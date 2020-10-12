using UniMvp.Interfaces;

namespace UniMvp.IoC.Interfaces
{
    public interface IDependencyInjector
    {
        IValueBinder Value { get; }
        IPresenterBinder Presenter { get; }
        IDependencyFactory Factory { get; }


        IDependencyInjector Setup<T>() where T : IBootstrapControl, new();
        IDependencyInjector SetupValues<T>() where T : IBootstrapValue, new();


        IDependencyInjector From<T>() where T : new();
        IDependencyInjector From( object source );
        IDependencyInjector To( object target );


        IDependencyInjector Enqueue( object value );
        IDependencyInjector Produce();
        IDependencyInjector Produce( object value );
        IDependencyInjector Produce( params object[] value );


        IDependencyInjector Save( object value );
        IDependencyInjector Save( object value, string key );
        IDependencyInjector Save( IControl control );


        void Clear();
    }
}
