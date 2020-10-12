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
        /// Набор статических медотодов, создающих различные реализации очереди <see cref="Interfaces.IBuildQueue"/>
        /// для поиска поставщиков значений для DI контейнера.
        /// </summary>
        public static partial class FindDependencies
        {
            /// <summary>
            /// Создать службу, которая будет добавлять значения в DI контейнер :
            /// <para> - из полей с атрибутом <see cref="SaveValueAttribute"/> ;</para>
            /// </summary>
            /// <param name="container"> Dependency injection container. </param>
            public static IBuildQueue InFields( IDependencyContainer container )
                => CreateBuildQueue( new GetFieldStrategy( container ) );

            /// <summary>
            /// Создать службу, которая будет добавлять значения в DI контейнер :
            /// <para> - из свойств с атрибутом <see cref="SaveValueAttribute"/> ;</para>
            /// </summary>
            /// <param name="container"> Dependency injection container. </param>
            public static IBuildQueue InProperties( IDependencyContainer container )
                => CreateBuildQueue( new GetPropertyStrategy( container ) );

            /// <summary>
            /// Создать службу, которая будет добавлять значения в DI контейнер :
            /// <para> - из полей и свойств с атрибутом <see cref="SaveValueAttribute"/> ;</para>
            /// </summary>
            /// <param name="container"> Dependency injection container. </param>
            public static IBuildQueue InFieldsAndProperties( IDependencyContainer container )
                => CreateBuildQueue(
                    new GetFieldStrategy( container ),
                    new GetPropertyStrategy( container ) );

            /// <summary>
            /// Создать службу, которая будет добавлять значения в DI контейнер :
            /// <para> - из полей и свойств с атрибутом <see cref="SaveValueAttribute"/> ;</para>
            /// <para> - из методов с атрибутом <see cref="SaveMethodAttribute"/> ;</para>
            /// </summary>
            /// <param name="container"> Dependency injection container. </param>
            public static IBuildQueue InFieldsPropertiesAndMethods( IDependencyContainer container )
                => CreateBuildQueue(
                    new GetFieldStrategy( container ),
                    new GetPropertyStrategy( container ),
                    new GetMethodStrategy( container ) );

            /// <summary>
            /// Создать службу, которая будет добавлять значения в DI контейнер :
            /// <para> - из свойств с атрибутом <see cref="SaveValueAttribute"/> ;</para>
            /// <para> - из методов с атрибутом <see cref="SaveMethodAttribute"/> ;</para>
            /// </summary>
            /// <param name="container"> Dependency injection container. </param>
            public static IBuildQueue InPropertiesAndMethods( IDependencyContainer container )
                => CreateBuildQueue(
                    new GetPropertyStrategy( container ),
                    new GetMethodStrategy( container ) );


            /// <summary>
            /// Создать службу по настройке объектов <see cref="UniMvp.Interfaces.IControl"/>.
            /// 
            /// <para> [ 1 ] Служба будет добавлять значения в DI контейнер: </para>
            /// <para> - из полей и свойств с атрибутом <see cref="SaveValueAttribute"/> ;</para>
            /// <para> - из методов с атрибутом <see cref="SaveMethodAttribute"/> ;</para>
            /// 
            /// <para> [ 2 ] Служба будет обрабатывать все значения свойств с атрибутом <see cref="ProduceAttribute"/>:</para>
            /// <para> - и внедрять <see cref="UniMvp.Interfaces.IPresenter"/> для всех служб <see cref="UniMvp.Interfaces.IControl"/> ; </para>
            /// <para> - добавлять в очередь на инициализацию в <paramref name="factory"/> всех созданых объектов; </para>
            /// 
            /// </summary>
            /// <param name="factory"> Фабрика по созданию объектов и настройке зависимостей. </param>
            /// <param name="valueContainer"> DI контейнер для кэширования и поставки значений. </param>
            /// <param name="controlsContainer"> DI контейнер со настройками презентеров для служб. </param>
            public static IBuildQueue InControls( IDependencyFactory factory, IDependencyContainer valueContainer, IDependencyContainer controlsContainer )
                => CreateBuildQueue(
                    new GetFieldStrategy( valueContainer ),
                    new GetPropertyStrategy( valueContainer ),
                    new GetMethodStrategy( valueContainer ),
                    new ProduceControlStrategy( factory, valueContainer, controlsContainer ) );

            /// <summary>
            /// Создать службу по настройке объектов <see cref="UniMvp.Interfaces.IService"/>.
            /// 
            /// <para> [ 1 ] Служба будет добавлять значения в DI контейнер: </para>
            /// <para> - из полей и свойств с атрибутом <see cref="SaveValueAttribute"/> ;</para>
            /// <para> - из методов с атрибутом <see cref="SaveMethodAttribute"/> ;</para>
            /// 
            /// <para> [ 2 ] Служба будет обрабатывать все значения свойств с атрибутом <see cref="ProduceAttribute"/>:</para>
            /// <para> - и создавать объекты если свойство не имеет значение ; </para>
            /// <para> - добавлять в очередь на инициализацию в <paramref name="factory"/> всех созданых объектов; </para>
            /// 
            /// </summary>
            /// <param name="factory"> Фабрика по созданию объектов и настройке зависимостей. </param>
            /// <param name="valueContainer"> DI контейнер для кэширования и поставки значений. </param>
            public static IBuildQueue InServices( IDependencyFactory factory, IDependencyContainer valueContainer )
                => CreateBuildQueue(
                    new GetFieldStrategy( valueContainer ),
                    new GetPropertyStrategy( valueContainer ),
                    new GetMethodStrategy( valueContainer ),
                    new ProduceServiceStrategy( factory, valueContainer ) );

        }
    }


}
