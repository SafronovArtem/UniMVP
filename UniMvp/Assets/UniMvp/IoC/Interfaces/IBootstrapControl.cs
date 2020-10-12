namespace UniMvp.IoC.Interfaces
{
    public interface IBootstrapControl

    {
        void Setup( IPresenterBinder binder );
    }
}
