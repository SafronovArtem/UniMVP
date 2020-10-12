using UniMvp.Interfaces;

namespace UniMvp.IoC.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPresenterBinder

    {
        IDependencyContainer Container { get; }

        IPresenterBinder Set<TPresenter, TControl>()
            where TPresenter : IPresenter, new()
            where TControl : IControl;

        IPresenterBinder Set<TPresenter, TControl>( string controlName )
            where TPresenter : IPresenter, new()
            where TControl : IControl;

        IPresenterBinder Set<TPresenter, TControl>( params string[] controlNames )
            where TPresenter : IPresenter, new()
            where TControl : IControl;

        IPresenterBinder Remove<TControl>() where TControl : IControl;

        IPresenterBinder Remove<TControl>( string controlName ) where TControl : IControl;

        void Clear();
    }
}
