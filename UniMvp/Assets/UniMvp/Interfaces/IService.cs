namespace UniMvp.Interfaces
{
    public interface IService 
    {
        bool IsActive { get; }

        void Activate();

        void Deactivate();
    }
}
