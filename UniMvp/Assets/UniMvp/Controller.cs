using UniMvp.Interfaces;
using UniMvp.Systems;

namespace UniMvp
{
    /// <summary>
    /// <para> Контроллер для решения задач, связанных со службой <see cref="Interfaces.IControl"/> </para>
    /// </summary>
    public abstract class Controller : CompositeComponent, IController
    {
        /// <summary>
        /// Служба в среде которой экземпляр данного класса обрабатывает изменения.
        /// </summary>
        protected IControl Control { get; private set; }

        IControl IController.Control
        {
            get => Control;
            set => Control = value;
        }


        /// <summary>
        /// Удалить компонент из родительской коллекции.
        /// <para> Удалить ссылку на <see cref="Control"/>. </para>
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            Control = null;
        }
    }
}
