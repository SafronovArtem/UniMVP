namespace UniMvp.Interfaces
{
    public interface IComponent : IDisposable
    {
        void SetParent( IComposite parent );
    }
}
