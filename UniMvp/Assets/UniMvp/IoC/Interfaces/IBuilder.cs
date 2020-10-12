using UniMvp.Interfaces;

namespace UniMvp.IoC.Interfaces
{
    public interface IBuilder
    {

        ICompositeService CreateCompositeService();
        ICompositeService CreateCompositeService( IComposite parent );
        ICompositeService CreateCompositeServiceForPreseter( IComposite parent );

        IComposite CreateComposite();
        IComposite CreateComposite( IComposite parent );



        IDependencyContainer CreateValueContainer();
        IDependencyContainer CreatePresenterContainer();


        IPresenterBinder CreatePresenterBinder();
        IPresenterBinder CreatePresenterBinder( IDependencyContainer container );

        IValueBinder CreateValueBinder();
        IValueBinder CreateValueBinder( IDependencyContainer container );

        IDependencyFactory CreateFactory();
        IDependencyFactory CreateFactory( out IValueBinder valueBinder, out IPresenterBinder presenterBinder );
        IDependencyFactory CreateFactory( out IDependencyContainer valuesContainer, out IDependencyContainer presentersContainer );
        IDependencyFactory CreateFactory( IDependencyContainer valuesContainer, IDependencyContainer presentersContainer );

        IDependencyInjector CreateDependencyInjector();

        IInitializationFactory CreateInitializationFactory( IBuildQueue find, IBuildQueue inject, IBuildQueue initialize );
        IBuildQueue CreateBuildQueue( params IBuildStrategy[] strategies );

    }
}
