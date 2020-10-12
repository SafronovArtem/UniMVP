using UniMvp.Interfaces;
using UniMvp.IoC.Interfaces;

namespace UniMvp.IoC.Building
{
    public partial class Builder
    {
        private static IBuilder instance;

        public Builder()
            => instance = this;

        public Builder( IBuilder builder )
            => instance = builder;

        protected static IBuilder Instance
            => instance ?? new Builder();

        public static IComposite CreateComposite()
            => Instance.CreateComposite();

        public static IComposite CreateComposite( IComposite parent )
            => Instance.CreateComposite( parent );

        public static ICompositeService CreateCompositeService()
            => Instance.CreateCompositeService();

        public static ICompositeService CreateCompositeService( IComposite parent )
            => Instance.CreateCompositeService( parent );

        public static ICompositeService CreateCompositeServiceForPreseter( IComposite parent )
            => Instance.CreateCompositeServiceForPreseter( parent );

        public static IBuildQueue CreateBuildQueue( params IBuildStrategy[] strategies )
            => Instance.CreateBuildQueue( strategies );

        public static IDependencyInjector CreateDependencyInjector()
            => Instance.CreateDependencyInjector();

        public static IDependencyFactory CreateFactory()
            => Instance.CreateFactory();

        public static IDependencyFactory CreateFactory( out IValueBinder valueBinder, out IPresenterBinder presenterBinder )
            => Instance.CreateFactory( out valueBinder, out presenterBinder );

        public static IDependencyFactory CreateFactory( out IDependencyContainer valuesContainer, out IDependencyContainer presentersContainer )
            => Instance.CreateFactory( out valuesContainer, out presentersContainer );

        public static IDependencyFactory CreateFactory( IDependencyContainer valuesContainer, IDependencyContainer presentersContainer )
            => Instance.CreateFactory( valuesContainer, presentersContainer );

        public static IInitializationFactory CreateInitializationFactory( IBuildQueue find, IBuildQueue inject, IBuildQueue initialize )
            => Instance.CreateInitializationFactory( find, inject, initialize );

        public static IPresenterBinder CreatePresenterBinder()
            => Instance.CreatePresenterBinder();

        public static IPresenterBinder CreatePresenterBinder( IDependencyContainer container )
            => Instance.CreatePresenterBinder( container );

        public static IDependencyContainer CreatePresenterContainer()
            => Instance.CreatePresenterContainer();

        public static IValueBinder CreateValueBinder()
            => Instance.CreateValueBinder();

        public static IValueBinder CreateValueBinder( IDependencyContainer container )
            => Instance.CreateValueBinder( container );

        public static IDependencyContainer CreateValueContainer()
            => Instance.CreateValueContainer();

    }
}
