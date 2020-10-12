using UniMvp.Interfaces;
using UniMvp.Systems;

namespace UniMvp
{
    /// <summary>
    /// <para> Контроллер для решения задач, связанных со службой ( <typeparamref name="T"/> ) <see cref="Interfaces.IControl"/>. </para>
    /// <para> Генерирует исключение <see cref="Errors.MvpControllerException"/> в случае передачи контроллера службе <see cref="Interfaces.IControl"/> которая не является типом <typeparamref name="T"/>. </para>
    /// </summary>
    /// <typeparam name="T">Служба в среде которой экземпляр <see cref="Controller{T}"/> обрабатывает изменения.</typeparam>
    public abstract class Controller<T> : CompositeComponent, IController where T : IControl
    {
        /// <summary>
        /// Служба в среде которой экземпляр данного класса обрабатывает изменения.
        /// </summary>
        protected T Control { get; private set; }

        IControl IController.Control
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
                    throw new Errors.MvpControllerException( $"Couldn't inject control value to the controller. Control must be of type: {typeof( T ).FullName}, Injected type: {value.GetType().FullName}, Controller: {GetType().FullName}." );
                }
            }
        }

        /// <summary>
        /// Удалить компонент из родительской коллекции.
        /// <para> Удалить ссылку на <see cref="Control"/>. </para>
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            Control = default;
        }

    }
}
