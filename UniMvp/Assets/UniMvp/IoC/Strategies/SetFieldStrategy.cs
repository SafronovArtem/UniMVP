using System.Reflection;
using UniMvp.IoC.Errors;
using UniMvp.IoC.Interfaces;

namespace UniMvp.IoC.Strategies
{
    /// <summary>
    /// Внедрение значений из DI контейнера всем <b> полям </b> класса с атрибутом <see cref="InjectToAttribute"/>.
    /// </summary>
    public class SetFieldStrategy : IBuildStrategy
    {
        private readonly IDependencyContainer container;
        private static readonly System.Type attributeType = typeof( InjectToAttribute );

        /// <summary>
        /// Внедрение значений из DI контейнера всем <b> полям </b> класса с атрибутом <see cref="InjectToAttribute"/>.
        /// </summary>
        /// <param name="container"> Dependency injection container. </param>
        public SetFieldStrategy( IDependencyContainer container )
        {
            this.container = container;
        }

        public void Execute( object target )
        {
            var fields = target.GetType().GetFields( BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic );

            for (var i = 0; i < fields.Length; i++)
            {
                var fieldInfo = fields[ i ];
                var attributes = System.Attribute.GetCustomAttributes( fieldInfo, attributeType );

                for (var j = 0; j < attributes.Length; j++)
                {
                    if (!(attributes[ j ] is InjectToAttribute attribute)) continue;

                    try
                    {
                        var value = container.Get( container.Alias.Nameof(fieldInfo.FieldType, attribute.key) );
                        fieldInfo.SetValue( target, value );
                    }
                    catch (IoCValueNotFoundException valueNotFound)
                    {
                        throw new IoCException( $"Couldn't inject value to field: {target.GetType().FullName}.{fieldInfo.Name}; ", valueNotFound );
                    }
                }


            }
        }
    }
}
