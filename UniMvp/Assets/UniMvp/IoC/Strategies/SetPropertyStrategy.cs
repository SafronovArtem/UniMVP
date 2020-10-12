using System.Reflection;
using UniMvp.IoC.Errors;
using UniMvp.IoC.Interfaces;

namespace UniMvp.IoC.Strategies
{
    /// <summary>
    /// Внедрение значений из DI контейнера всем <b> свойствам </b> класса с атрибутом <see cref="InjectToAttribute"/>.
    /// </summary>
    public class SetPropertyStrategy : IBuildStrategy
    {
        private readonly IDependencyContainer container;
        private static readonly System.Type attributeType = typeof( InjectToAttribute );

        /// <summary>
        /// Внедрение значений из DI контейнера всем <b> свойствам </b> класса с атрибутом <see cref="InjectToAttribute"/>.
        /// </summary>
        /// <param name="container"> Dependency injection container. </param>
        public SetPropertyStrategy( IDependencyContainer container )
        {
            this.container = container;
        }

        public void Execute( object target )
        {
            var properties = target.GetType().GetProperties( BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic );

            for (var i = 0; i < properties.Length; i++)
            {
                var propertyInfo = properties[ i ];
                var attributes = System.Attribute.GetCustomAttributes( propertyInfo, attributeType );

                for (var j = 0; j < attributes.Length; j++)
                {
                    if (!(attributes[ j ] is InjectToAttribute attribute)) continue;

                    try
                    {
                        var value = container.Get( container.Alias.Nameof(propertyInfo.PropertyType, attribute.key) );
                        propertyInfo.SetValue( target, value, null );
                    }
                    catch (IoCValueNotFoundException valueNotFound)
                    {
                        throw new IoCException( $"Couldn't inject value to property: {target.GetType().FullName}.{propertyInfo.Name}; ", valueNotFound );
                    }
                    catch (System.ArgumentException argumentException)
                    {
                        throw new IoCException( $"Can't inject value to property. {argumentException.Message}; Property is missing setter. Inner exception: {target.GetType().FullName}.{propertyInfo.Name} ; " );
                    }
                }


            }
        }
    }
}
