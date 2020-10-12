using UniMvp.IoC.Factory;
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
        /// Создает компоненты которые реализуют этапы инициализации различных типов служб в DI фабрике.
        /// </summary>
        public static partial class InitializorFor
        {
            /// <summary>
            /// Создает службу для инициализации объектов: <see cref="UniMvp.Interfaces.IControl"/>.
            /// </summary>
            /// <param name="factory">Фабрика по созданию объектов и настройке зависимостей.</param>
            /// <param name="valueContainer"> DI контейнер для кэширования и поставки значений. </param>
            /// <param name="controlsContainer"> DI контейнер со настройками презентеров для служб. </param>
            /// <returns></returns>
            public static IInitializationFactory Controls( IDependencyFactory factory, IDependencyContainer valueContainer, IDependencyContainer controlsContainer )
            {
                var find = FindDependencies.InControls( factory, valueContainer, controlsContainer );
                var inject = InsertDependencies.IntoFieldsPropertiesAndMethods( valueContainer );
                var init = CreateBuildQueue( new InitCompositeStrategy( factory ) );

                return CreateInitializationFactory( find, inject, init );
            }

            /// <summary>
            /// Создает службу для инициализации объектов: <see cref="UniMvp.Interfaces.IService"/>.
            /// </summary>
            /// <param name="factory">Фабрика по созданию объектов и настройке зависимостей.</param>
            /// <param name="valueContainer"> DI контейнер для кэширования и поставки значений. </param>
            /// <returns></returns>
            public static IInitializationFactory Services( IDependencyFactory factory, IDependencyContainer valueContainer )
            {
                var find = FindDependencies.InServices( factory, valueContainer );
                var inject = InsertDependencies.IntoFieldsPropertiesAndMethods( valueContainer );
                var init = CreateBuildQueue( new InitCompositeStrategy( factory ) );

                return CreateInitializationFactory( find, inject, init );
            }

            /// <summary>
            /// Создает службу для инициализации объектов: <see cref="UniMvp.Interfaces.IPresenter"/>.
            /// </summary>
            /// <param name="factory">Фабрика по созданию объектов и настройке зависимостей.</param>
            /// <param name="valueContainer"> DI контейнер для кэширования и поставки значений. </param>
            public static IInitializationFactory Presenter( IDependencyFactory factory, IDependencyContainer valueContainer )
            {
                var inject = InsertDependencies.IntoFieldsPropertiesAndMethods( valueContainer );
                var init = CreateBuildQueue( new InitCompositeStrategy( factory ) );

                return CreateInitializationFactory( null, inject, init );
            }

            /// <summary>
            /// Создает службу для инициализации объектов: <see cref="UniMvp.Interfaces.IComponent"/>.
            /// </summary>
            public static IInitializationFactory Components( IDependencyContainer valueContainer )
            {
                var inject = InsertDependencies.IntoFieldsPropertiesAndMethods( valueContainer );
                var init = CreateBuildQueue( new InitStrategy() );

                return CreateInitializationFactory( null, inject, init );
            }

            /// <summary>
            /// Создает службу для инициализации общих объектов.
            /// </summary>
            public static IInitializationFactory Common( IDependencyContainer valueContainer )
            {
                var find = FindDependencies.InFieldsPropertiesAndMethods( valueContainer );
                var inject = InsertDependencies.IntoFieldsPropertiesAndMethods( valueContainer );
                var init = CreateBuildQueue( new InitStrategy() );

                return CreateInitializationFactory( find, inject, init );
            }


        }
    }
}
