using System.Reflection;
using UniMvp.IoC.Errors;
using UniMvp.IoC.Interfaces;

namespace UniMvp.IoC.Strategies
{
    /// <summary>
    /// Добавляет в DI контейнер методы поставщики значений с атрибутом <see cref="SaveMethodAttribute"/>.
    /// </summary>
    public sealed class GetMethodStrategy : IBuildStrategy
    {
        private readonly IDependencyContainer container;
        private static readonly System.Type attributeType = typeof( SaveMethodAttribute );

        #region Constructors

        /// <summary>
        /// Добавляет в DI контейнер методы поставщики значений с атрибутом <see cref="SaveMethodAttribute"/>.
        /// </summary>
        /// <param name="container"> Dependency injection container. </param>
        public GetMethodStrategy( IDependencyContainer container )
        {
            this.container = container;
        }
        #endregion

        public void Execute( object target )
        {
            var methods = target.GetType().GetMethods( BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic );

            for (var i = 0; i < methods.Length; i++)
            {
                var methodInfo = methods[ i ];
                var attributes = System.Attribute.GetCustomAttributes(methodInfo, attributeType);

                for (var j = 0; j < attributes.Length; j++)
                {
                    if (!(attributes[ j ] is SaveMethodAttribute attribute)) continue;

                    var alias = container.Alias.Nameof( methodInfo.ReturnType, attribute.Key );

                    var parameterInfo = methodInfo.GetParameters();
                    if (parameterInfo is null || parameterInfo.Length == 0)
                    {
                        // вызов метода без передачи параметров

                        container.Set( alias, () => methodInfo.Invoke( target, null ) );
                    }
                    else
                    {
                        // вызов метода с передачей параметров
                        // значения параметров берутся из DI контейнера

                        var parameterAlias = new string[ parameterInfo.Length ];

                        for (var k = 0; k < parameterInfo.Length; k++)
                            parameterAlias[ k ] = container.Alias.Nameof( parameterInfo[ k ].ParameterType, attribute.GetParameterKey( k ) );

                        container.Set( alias, () =>
                        {
                            var parameters = new object[ parameterAlias.Length ];
                            try
                            {
                                for (var l = 0; l < parameterAlias.Length; l++)
                                    parameters[ l ] = container.Get( parameterAlias[ l ] );
                            }
                            catch (IoCValueNotFoundException notFound)
                            {
                                throw new IoCValueNotFoundException( $"Couldn't inject required values to method. {notFound.Message} Check method attributes at {target.GetType().FullName}.{methodInfo.Name};" );
                            }
                            return methodInfo.Invoke( target, parameters );
                        } );
                    }


                }

            }
        }

    }
}
