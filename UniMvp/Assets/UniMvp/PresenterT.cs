using UniMvp.Interfaces;
using UniMvp.Systems;

namespace UniMvp
{
    public abstract class Presenter<T> : CompositePresenter, IPresenter where T : IControl
    {

        /// <summary>
        /// Служба в среде которой экземпляр данного класса обрабатывает изменения.
        /// </summary>
        public T Control { get; private set; }

        IControl IPresenter.Control
        {
            get => Control;
            set
            {
                try
                {
                    Control = (T)value;
                }
                catch (System.InvalidCastException)
                {
                    throw new Errors.MvpPresenterException( $"Couldn't inject control value to the presenter. Control must be of type: {typeof( T ).FullName}, Injected type: {value.GetType().FullName}, Presenter: {GetType().FullName}." );
                }
            }
        }

        public abstract void Initialize();
        public override IComponent Register( IComponent component )
        {
            if (component is IController controller) controller.Control = Control;
            return base.Register( component );
        }

    }
}
