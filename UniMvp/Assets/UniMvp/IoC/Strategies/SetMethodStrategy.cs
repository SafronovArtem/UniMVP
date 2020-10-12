using System.Reflection;
using UniMvp.IoC.Errors;
using UniMvp.IoC.Interfaces;

namespace UniMvp.IoC.Strategies
{
    /// <summary>
    /// Внедрение значений из DI контейнера всем <b> методам </b> класса с атрибутом: <see cref="InjectToMethodAttribute"/>.
    /// </summary>
    public sealed class SetMethodStrategy : IBuildStrategy
    {
        private readonly IDependencyContainer container;
        private static readonly System.Type attributeType = typeof( InjectToMethodAttribute );

        #region Constructors

        /// <summary>
        /// Внедрение значений из DI контейнера всем <b> методам </b> класса с атрибутом: <see cref="InjectToMethodAttribute"/>.
        /// </summary>
        /// <param name="container"> Dependency injection container </param>
        public SetMethodStrategy( IDependencyContainer container )
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
                    if (!(attributes[ j ] is InjectToMethodAttribute attribute)) continue;

                    var parameterInfo = methodInfo.GetParameters();
                    if (parameterInfo != null && parameterInfo.Length > 0)
                    {
                        var keys = attribute.keys;
                        var parameterAlias = new string[ parameterInfo.Length ];

                        for (var k = 0; k < parameterInfo.Length; k++)
                            parameterAlias[ k ] = container.Alias.Nameof( parameterInfo[ k ].ParameterType, keys[ k ] );

                        var parameters = new object[ parameterAlias.Length ];
                        try
                        {
                            for (var l = 0; l < parameterAlias.Length; l++)
                                parameters[ l ] = container.Get( parameterAlias[ l ] );
                        }
                        catch (IoCValueNotFoundException notFound)
                        {
                            throw new IoCValueNotFoundException( $"Couldn't inject values to method. {notFound.Message} Check method attributes at {target.GetType().FullName}.{methodInfo.Name};" );
                        }
                        methodInfo.Invoke( target, parameters );

                    }
                    else
                    {
#if DEBUG
                        throw new IoCException( $"Method with attribute '{nameof( InjectToMethodAttribute )}' must have at least one parameter. Invalid DI method: '{target.GetType().FullName}.{methodInfo.Name}'; " );
#endif
                        // throw exception
                    }


                }

            }
        }

    }
}
