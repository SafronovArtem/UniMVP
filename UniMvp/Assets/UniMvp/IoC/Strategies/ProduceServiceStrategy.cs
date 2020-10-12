using System;
using System.Reflection;
using UniMvp.Interfaces;
using UniMvp.IoC.Errors;
using UniMvp.IoC.Interfaces;

namespace UniMvp.IoC.Strategies
{
    /// <summary>
    /// <para> Создание и добавление в очередь на инициализацию в <see cref="Interfaces.IDependencyFactory"/> значений всех свойств с атрибутом <see cref="ProduceAttribute"/> ; </para>
    /// <para> Для служб <see cref="UniMvp.Interfaces.IService"/> </para>
    /// </summary>
    public sealed class ProduceServiceStrategy : IBuildStrategy
    {
        private static readonly Type attributeType = typeof( ProduceAttribute );

        private readonly IDependencyFactory builder;
        private readonly IDependencyContainer valueContainer;

        /// <summary>
        /// Создание и добавление в очередь на инициализацию в <see cref="Interfaces.IDependencyFactory"/> значений всех свойств с атрибутом <see cref="ProduceAttribute"/> ;
        /// <para> Для служб <see cref="UniMvp.Interfaces.IService"/> </para>
        /// </summary>
        /// <param name="builder"> Фабрика по инициализации объектов. </param>
        /// <param name="valueContainer"> DI контейнер для кэширования и поставки значений. </param>
        public ProduceServiceStrategy( IDependencyFactory builder, IDependencyContainer valueContainer )
        {
            this.builder = builder;
            this.valueContainer = valueContainer;
        }

        public void Execute( object target )
        {
            if (!(target is IService)) return;

            var type = target.GetType();
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

                    if (attribute.saveValue)
                        valueContainer.Set( valueContainer.Alias.Nameof( propertyType, attribute.key ), value );

                    builder.Produce( value );

                }

            }
        }

    }
}
