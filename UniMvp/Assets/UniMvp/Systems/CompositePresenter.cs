using System.Collections.Generic;
using UniMvp.Interfaces;
using UniMvp.IoC.Building;

namespace UniMvp.Systems
{
    public abstract class CompositePresenter : ICompositeService
    {
        private readonly ICompositeService composite;

        protected CompositePresenter()
        {
            composite = Builder.CreateCompositeServiceForPreseter( this );
        }

        public bool IsActive => composite.IsActive;

        public List<IComponent> Elements
        {
            get => composite.Elements;
            set => composite.Elements = value;
        }
        public List<IService> Services
        {
            get => composite.Services;
            set => composite.Services = value;
        }


        #region Presenter implementation

        protected T Add<T>() where T : IController, IComponent, new()
        {
            return Add( (T)System.Activator.CreateInstance( typeof( T ) ) );
        }

        protected T Add<T>( params object[] args ) where T : IController, IComponent, new()
        {
            return Add( (T)System.Activator.CreateInstance( typeof( T ), args ) );
        }

        protected T Add<T>( T controller ) where T : IController, IComponent
        {
            Register( controller );
            return controller;
        }

        #endregion


        #region IComposite && IService implementation
        public virtual void Activate()
            => composite.Activate();

        public virtual void Deactivate()
            => composite.Deactivate();

        public virtual IComponent Register( IComponent component )
            => composite.Register( component );

        public virtual void Remove( IComponent component )
            => composite.Remove( component );

        public virtual void Dispose()
        {
            if (IsActive) Deactivate();
            composite.Dispose();
        }
        #endregion

    }
}
