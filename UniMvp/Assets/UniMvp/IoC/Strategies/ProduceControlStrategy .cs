using System;
using System.Reflection;
using UniMvp.Errors;
using UniMvp.Interfaces;
using UniMvp.IoC.Errors;
using UniMvp.IoC.Interfaces;

namespace UniMvp.IoC.Strategies
{
    /// <summary>
    /// <para> Обрабатывает объекты, реализующие <see cref="UniMvp.Interfaces.IControl"/> ; </para>
    /// <para> Создание и добавление в очередь на инициализацию в <see cref="Interfaces.IDependencyFactory"/> значений всех свойств с атрибутом <see cref="ProduceAttribute"/> ; </para>
    /// <para> Инжектит службе <see cref="UniMvp.Interfaces.IPresenter"/> и добавляет его в фабрику для инициализации ; </para>
    /// </summary>
    public sealed class ProduceControlStrategy : IBuildStrategy
    {
        private static readonly Type attributeType = typeof( ProduceAttribute );

        private readonly IDependencyFactory builder;
        private readonly IDependencyContainer valueContainer;
        private readonly IDependencyContainer controlsContainer;

        /// <summary>
        /// Создание и добавление в очередь на инициализацию в <see cref="Interfaces.IDependencyFactory"/> значений всех свойств с атрибутом <see cref="ProduceAttribute"/> ;
        /// <para> Инжектит службе <see cref="UniMvp.Interfaces.IPresenter"/> и добавляет его в фабрику для инициализации ; </para>
        /// </summary>
        /// <param name="builder"> Фабрика по инициализации объектов. </param>
        /// <param name="valueContainer"> DI контейнер для кэширования и поставки значений. </param>
        /// <param name="controlsContainer"> DI контейнер со настройками презентеров для служб. </param>
        public ProduceControlStrategy( IDependencyFactory builder, IDependencyContainer valueContainer, IDependencyContainer controlsContainer )
        {
            this.builder = builder;
            this.valueContainer = valueContainer;
            this.controlsContainer = controlsContainer;
        }

        public void Execute( object target )
        {
            if (!(target is IControl control)) return;

            var type = target.GetType();
            InjectPresenter( control, type );

            var properties = type.GetProperties( BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic );

            for (var i = 0; i < properties.Length; i++)
            {
                var propertyInfo = properties[ i ];
                var attributes = Attribute.GetCustomAttributes(propertyInfo, attributeType);

                for (var j = 0; j < attributes.Length; j++)
                {
                    if (!(attributes[ j ] is ProduceAttribute attribute)) continue;

                    var value = propertyInfo.GetValue( target, null );
                    var created = value != null;
                    var propertyType = propertyInfo.PropertyType;

                    #region Instantiate value
                    try
                    {
                        if (!created)
                            value = Activator.CreateInstance( propertyType );
                    }
                    catch (Exception exception)
                    {
                        throw new IoCException( $"Couldn't create value for property: '{type.FullName}.{propertyInfo.Name}'; Properties type must be a class not an abstraction or interface and must have method constructor withour paramenters. Inner exception: {exception.Message}" );
                    }
                    #endregion

                    #region Set value to property
                    try
                    {
                        if (!created)
                            propertyInfo.SetValue( target, value, null );
                    }
                    catch (ArgumentException argumentException)
                    {
                        throw new IoCException( $"Couldn't inject value to property. {argumentException.Message}; Property is missing setter. Inner exception: {target.GetType().FullName}.{propertyInfo.Name} ; " );
                    }
                    #endregion

                    #region Setup value and add to DI container
                    if (value is IControl)
                        (value as IControl).Name = valueContainer.Alias.NotNull( attribute.key );

                    if (attribute.saveValue)
                        valueContainer.Set( valueContainer.Alias.Nameof( propertyType, attribute.key ), value );
                    #endregion

                    builder.Produce( value );

                }

            }
        }

        private void InjectPresenter( IControl control, Type controlType )
        {

            if (string.IsNullOrEmpty( control.Name ))
                control.Name = controlsContainer.Alias.NotNull( control.Name );

            if (control.Presenter is null)
            {
                try
                {
                    var alias = controlsContainer.Alias.Nameof( controlType, control.Name );
                    control.Presenter = (IPresenter)controlsContainer.Get( alias );
                }
                catch (IoCValueNotFoundException notFound)
                {
                    throw new MvpPresenterException( $"Presenter was not added to the DI collection for specific control. {notFound.Message}" );
                }
                catch (InvalidCastException)
                {
                    throw new MvpPresenterException( $"Can't add class to the control instance as presenter which doesn't implement '{nameof( IPresenter )}' interface. Control alias: '{controlsContainer.Alias.Nameof( controlType, control.Name )}'; " );
                }
                catch (ArgumentException argumentException)
                {
                    throw new IoCException( $"Couldn't inject presenter. Control class '{controlType.FullName}' doesn't implement setter for presenter. Inner exception: {argumentException.Message}; " );
                }

            }

            if (control.Presenter.Control is null)
                control.Presenter.Control = control;

            builder.Produce( control.Presenter );
        }
    }
}
