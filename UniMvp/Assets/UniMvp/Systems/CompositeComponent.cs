using UniMvp.Interfaces;

namespace UniMvp.Systems
{
    public abstract class CompositeComponent : IComponent
    {
        /// <summary>
        /// <para> Родительский объект. </para>
        /// <para> Содержит экземпляр этого класса внутри своей коллекции компонентов. </para>
        /// <para> <see cref="Dispose"/> удаляет этот компонент из родительской коллекции. </para>
        /// </summary>
        protected IComposite Parent { get; private set; }

        /// <summary>
        /// Удалить компонент из родительской коллекции.
        /// </summary>
        public virtual void Dispose()
        {
            Parent.Remove( this );
            Parent = null;
        }

        void IComponent.SetParent( IComposite parent )
        {
            Parent = parent;
        }
    }
}
