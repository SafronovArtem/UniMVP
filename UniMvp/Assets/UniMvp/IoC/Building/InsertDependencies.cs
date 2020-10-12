using UniMvp.IoC.Interfaces;
using UniMvp.IoC.Strategies;

namespace UniMvp.IoC.Building
{
    /// <summary>
    /// Набор статических классов и методов для упрощения создания DI фабрики.
    /// </summary>
    public partial class Builder
    {
        /// <summary>
        /// Набор статических методов, создающих очередь <see cref="Interfaces.IBuildQueue"/>
        /// для внедрения значений из DI контейнера объектам очереди.
        /// </summary>
        public static partial class InsertDependencies
        {
            /// <summary>
            /// Создать службу, которая поставляет значения из DI контейнера:
            /// <para> - полям класса с атрибутом <see cref="InjectToAttribute"/> ; </para>
            /// </summary>
            /// <param name="container"> Dependency injection container. </param>
            public static IBuildQueue IntoFields( IDependencyContainer container )
                => CreateBuildQueue( new SetFieldStrategy( container ) );

            /// <summary>
            /// Создать службу, которая поставляет значения из DI контейнера:
            /// <para> - свойствам класса с атрибутом <see cref="InjectToAttribute"/> ; </para>
            /// </summary>
            /// <param name="container"> Dependency injection container. </param>
            public static IBuildQueue IntoProperties( IDependencyContainer container )
                => CreateBuildQueue( new SetPropertyStrategy( container ) );

            /// <summary>
            /// Создать службу, которая поставляет значения из DI контейнера:
            /// <para> - полям и свойствам класса с атрибутом <see cref="InjectToAttribute"/> ; </para>
            /// </summary>
            /// <param name="container"> Dependency injection container. </param>
            public static IBuildQueue IntoFieldsAndProperties( IDependencyContainer container )
                => CreateBuildQueue(
                    new SetFieldStrategy( container ),
                    new SetPropertyStrategy( container ) );

            /// <summary>
            /// Создать службу, которая поставляет значения из DI контейнера:
            /// <para> - свойствам класса с атрибутом: <see cref="InjectToAttribute"/> ; </para>
            /// <para> - методам класса с атрибутом: <see cref="InjectToMethodAttribute"/> ;</para>
            /// </summary>
            /// <param name="container"> Dependency injection container. </param>
            public static IBuildQueue IntoPropertiesAndMethods( IDependencyContainer container )
                => CreateBuildQueue(
                    new SetPropertyStrategy( container ),
                    new SetMethodStrategy( container ) );

            /// <summary>
            /// Создать службу, которая поставляет значения из DI контейнера:
            /// <para> - полям и свойствам класса с атрибутом: <see cref="InjectToAttribute"/> ; </para>
            /// <para> - методам класса с атрибутом: <see cref="InjectToMethodAttribute"/> ;</para>
            /// </summary>
            /// <param name="container"> Dependency injection container. </param>
            public static IBuildQueue IntoFieldsPropertiesAndMethods( IDependencyContainer container )
                => CreateBuildQueue(
                    new SetFieldStrategy( container ),
                    new SetPropertyStrategy( container ),
                    new SetMethodStrategy( container ) );

        }
    }
}
