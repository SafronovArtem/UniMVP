namespace UniMvp.Interfaces
{
    public interface IControl : IService, IDisposable
    {
        string Name { get; set; }

        IPresenter Presenter { get; set; }

    }
}
