using UniMvp.Interfaces;
using UniMvp.Systems;

namespace UniMvp
{
    public abstract class Presenter : CompositePresenter, IPresenter
    {

        /// <summary>
        /// Служба в среде которой экземпляр данного класса обрабатывает изменения.
        /// </summary>
        protected IControl Control { get; private set; }
        
        IControl IPresenter.Control
        {
            get => Control;
            set => Control = value;
        }

        public abstract void Initialize();

        public override IComponent Register( IComponent component )
        {
            if (component is IController controller) controller.Control = Control;
            return base.Register( component );
        }
    }
}
