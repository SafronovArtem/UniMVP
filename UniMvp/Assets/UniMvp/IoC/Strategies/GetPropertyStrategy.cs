using System.Reflection;
using UniMvp.IoC.Errors;
using UniMvp.IoC.Interfaces;

namespace UniMvp.IoC.Strategies
{
    /// <summary>
    /// Добавление в DI контейнер значений всех свойств с атрибутом <see cref="SaveValueAttribute"/> ;
    /// 
    /// </summary>
    public sealed class GetPropertyStrategy : IBuildStrategy
    {
        private readonly IDependencyContainer container;
        private static readonly System.Type attributeType = typeof( SaveValueAttribute );

        #region Constructors

        /// <summary>
        /// Добавление в DI контейнер значений всех свойств с атрибутом <see cref="SaveValueAttribute"/> ;
        /// </summary>
        /// <param name="container"> Dependency injection container. </param>
        public GetPropertyStrategy( IDependencyContainer container )
        {
            this.container = container;
        }
        #endregion

        public void Execute( object target )
        {
            var properties = target.GetType().GetProperties( BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic );

            for (var i = 0; i < properties.Length; i++)
            {
                var propertyInfo = properties[ i ];
                var attributes = System.Attribute.GetCustomAttributes(propertyInfo, attributeType);

                for (var j = 0; j < attributes.Length; j++)
                {
                    if (!(attributes[ j ] is SaveValueAttribute attribute)) continue;

                    var value = propertyInfo.GetValue( target, null );

                    if (value == null)
                        throw new IoCException( $"Can't add null value to the dependency injection collection. Property: {target.GetType().FullName}.{propertyInfo.Name}; " );

                    var parameterAlias = string.Empty;
                    if (attribute.useTypeFromParameter)
                    {
                        parameterAlias = container.Alias.Nameof( propertyInfo.PropertyType, attribute.key );
                        container.Set( parameterAlias, value );
                    }
                    if (attribute.useTypeFromValue)
                    {
                        var valueAlias = container.Alias.Nameof(value, attribute.key);

                        if (parameterAlias != valueAlias)
                            container.Set( valueAlias, value );
                    }
                    if (!attribute.useTypeFromParameter && !attribute.useTypeFromValue)
                    {
                        throw new IoCAttributeException( $"Invalid attribute parameters. Can't ignore both type names from value and from parameter at the same time, otherwise the value will not be added to the dipendency injection collection. Check property attribute: {target.GetType().FullName}.{propertyInfo.Name}; " );
                    }


                }

            }
        }
    }
}
