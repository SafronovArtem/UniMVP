using UniMvp.Interfaces;

namespace UniMvp
{
    public abstract class Control : IControl
    {
        public bool IsActive { get; private set; }

        public string Name { get; private set; }

        protected IPresenter Presenter { get; private set; }

        IPresenter IControl.Presenter { get => Presenter; set => Presenter = value; }

        string IControl.Name { get => Name; set => Name = value; }

        protected Control( string name )
        {
            Name = name;
        }

        public virtual void Activate()
        {
            if (!IsActive)
            {
                IsActive = true;
                Presenter.Activate();
            }
        }

        public virtual void Deactivate()
        {
            if (IsActive)
            {
                IsActive = false;
                Presenter.Deactivate();
            }
        }

        public virtual void Dispose()
        {
            Presenter.Dispose();
        }


    }
}
