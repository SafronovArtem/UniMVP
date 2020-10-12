using System.Collections.Generic;
using UniMvp.Interfaces;

namespace UniMvp.Systems
{
    public class Composite : IComposite, IDisposable
    {

        #region Fields and properties
        private readonly IComposite parent;

        public List<IComponent> Elements { get; set; } = new List<IComponent>();
        #endregion


        #region Constructors
        public Composite() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"> Объект для передачи в качестве родителя <see cref="Interfaces.IComposite"/> всем дочерним компонентам <see cref="Interfaces.IComponent"/> ; </param>
        public Composite( IComposite parent )
        {
            this.parent = parent;
        }
        #endregion


        #region IComposite implementation
        /// <summary>
        /// <para> Добавляет элемент в начало коллекции. </para>
        /// </summary>
        public virtual IComponent Register( IComponent component )
        {
            Elements.Insert( 0, component );
            component.SetParent( parent ?? this );
            return component;
        }

        /// <summary>
        /// Удалить элемент из коллекции.
        /// </summary>
        public virtual void Remove( IComponent component )
        {
            Elements.Remove( component );
        }

        /// <summary>
        /// Удалить все элементы коллекции.
        /// </summary>
        public virtual void Dispose()
        {
            while (Elements.Count > 0) Elements[ 0 ].Dispose();
        }
        #endregion
    }
}
