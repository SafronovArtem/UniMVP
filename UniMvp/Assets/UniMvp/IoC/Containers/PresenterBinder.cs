using UniMvp.Interfaces;
using UniMvp.IoC.Building;
using UniMvp.IoC.Interfaces;

namespace UniMvp.IoC.Containers
{
    public class PresenterBinder : IPresenterBinder
    {
        public IDependencyContainer Container { get; }

        public PresenterBinder()
            : this( Builder.CreatePresenterContainer() )
        { }

        public PresenterBinder( IDependencyContainer container )
        {
            Container = container;
        }

        public IAliasMaker Alias => Container.Alias;

        public IPresenterBinder Set<TPresenter, TControl>()
            where TPresenter : IPresenter, new()
            where TControl : IControl
        {
            Container.Set( Alias.Nameof<TControl>(), () => System.Activator.CreateInstance( typeof( TPresenter ) ) );
            return this;
        }
        public IPresenterBinder Set<TPresenter, TControl>( params string[] controlNames )
            where TPresenter : IPresenter, new()
            where TControl : IControl
        {
            for (var i = 0; i < controlNames.Length; i++)
                Set<TPresenter, TControl>( controlNames[ i ] );

            return this;
        }
        public IPresenterBinder Set<TPresenter, TControl>( string controlName )
            where TPresenter : IPresenter, new()
            where TControl : IControl
        {
            Container.Set( Alias.Nameof<TControl>( controlName ), () => System.Activator.CreateInstance( typeof( TPresenter ) ) );
            return this;
        }


        public IPresenterBinder Remove<TControl>() where TControl : IControl
        {
            Container.Remove( Alias.Nameof<TControl>() );
            return this;
        }

        public IPresenterBinder Remove<TControl>( string controlName ) where TControl : IControl
        {
            Container.Remove( Alias.Nameof<TControl>( controlName ) );
            return this;
        }


        public void Clear()
        {
            Container.Clear();
        }

    }
}
