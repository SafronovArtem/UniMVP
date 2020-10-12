using System;
using UniMvp.Interfaces;
using UniMvp.IoC.Containers;
using UniMvp.IoC.Factory;
using UniMvp.IoC.Interfaces;
using UniMvp.IoC.Strategies;
using UniMvp.Systems;

namespace UniMvp.IoC.Building
{
    public partial class Builder : IBuilder
    {
        IBuildQueue IBuilder.CreateBuildQueue( params IBuildStrategy[] strategies )
        {
            return new BuildQueue( strategies );
        }


        #region Composition

        ICompositeService IBuilder.CreateCompositeService()
            => new CompositeService();

        ICompositeService IBuilder.CreateCompositeService( IComposite parent )
            => new CompositeService( parent );

        ICompositeService IBuilder.CreateCompositeServiceForPreseter( IComposite parent )
            => (this as IBuilder).CreateCompositeService( parent );

        IComposite IBuilder.CreateComposite()
            => new Composite();

        IComposite IBuilder.CreateComposite( IComposite parent )
            => new Composite( parent );
        #endregion

        #region Dependency containers
        IDependencyContainer IBuilder.CreateValueContainer()
            => new DependencyContainer();
        IDependencyContainer IBuilder.CreatePresenterContainer()
            => (this as IBuilder).CreateValueContainer();

        IPresenterBinder IBuilder.CreatePresenterBinder()
            => new PresenterBinder();
        IPresenterBinder IBuilder.CreatePresenterBinder( IDependencyContainer container )
            => new PresenterBinder( container );

        IValueBinder IBuilder.CreateValueBinder()
            => new ValueBinder();

        IValueBinder IBuilder.CreateValueBinder( IDependencyContainer container )
            => new ValueBinder( container );
        #endregion


        #region Dependency factory

        IDependencyInjector IBuilder.CreateDependencyInjector()
            => new DependencyInjector();

        IInitializationFactory IBuilder.CreateInitializationFactory( IBuildQueue find, IBuildQueue inject, IBuildQueue initialize )
            => new InitializationFactory( find, inject, initialize );

        IDependencyFactory IBuilder.CreateFactory()
            => (this as IBuilder).CreateFactory(
               (this as IBuilder).CreateValueContainer(),
               (this as IBuilder).CreateValueContainer() );

        IDependencyFactory IBuilder.CreateFactory( out IValueBinder valueBinder, out IPresenterBinder presenterBinder )
        {
            var factory = (this as IBuilder).CreateFactory(out IDependencyContainer values, out IDependencyContainer presenters);
            valueBinder = (this as IBuilder).CreateValueBinder( values );
            presenterBinder = (this as IBuilder).CreatePresenterBinder( presenters );
            return factory;
        }

        IDependencyFactory IBuilder.CreateFactory( out IDependencyContainer valuesContainer, out IDependencyContainer presentersContainer )
        {
            valuesContainer = (this as IBuilder).CreateValueContainer();
            presentersContainer = (this as IBuilder).CreatePresenterContainer();

            return (this as IBuilder).CreateFactory( valuesContainer, presentersContainer );
        }

        IDependencyFactory IBuilder.CreateFactory( IDependencyContainer valuesContainer, IDependencyContainer presentersContainer )
        {
            return new DependencyFactory( valuesContainer, presentersContainer );
        }
        #endregion
    }
}
