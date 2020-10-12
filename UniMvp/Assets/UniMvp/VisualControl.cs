using UniMvp.Interfaces;
using UnityEngine;

namespace UniMvp
{
    public abstract class VisualControl : MonoBehaviour, IControl
    {
        [SerializeField] private string controlName;

        public IPresenter Presenter { get; set; }

        public bool IsActive { get; private set; }
        public string ControlName { get => controlName; private set => controlName = value; }
        string IControl.Name { get => ControlName; set => ControlName = value; }

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
