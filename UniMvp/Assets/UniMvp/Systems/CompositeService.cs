using System.Collections.Generic;
using UniMvp.Interfaces;
using UniMvp.IoC.Building;

namespace UniMvp.Systems
{
    public class CompositeService : ICompositeService
    {

        #region Fields and properties

        private readonly IComposite composite;

        public bool IsActive { get; private set; }
        public List<IService> Services { get; set; } = new List<IService>();
        public List<IComponent> Elements
        {
            get => composite.Elements;
            set => composite.Elements = value;
        }
        #endregion


        #region Constructors
        public CompositeService()
        {
            composite = Builder.CreateComposite( this );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"> Объект для передачи в качестве родителя <see cref="Interfaces.IComposite"/> всем дочерним компонентам <see cref="Interfaces.IComponent"/> ; </param>
        public CompositeService( IComposite parent )
        {
            composite = Builder.CreateComposite( parent is null ? this : parent );

        }
        #endregion


        #region IService implementation
        /// <summary>
        /// Активирует все службы <see cref="IService"/> в коллекции.
        /// </summary>
        public virtual void Activate()
        {
            if (!IsActive)
            {
                IsActive = true;

                for (var i = Services.Count - 1; i > -1; i--) Services[ i ].Activate();
            }
        }

        /// <summary>
        /// Деактивирует все службы <see cref="IService"/> в коллекции.
        /// </summary>
        public virtual void Deactivate()
        {
            if (IsActive)
            {
                IsActive = false;

                for (var i = Services.Count - 1; i > -1; i--)
                    Services[ i ].Deactivate();
            }
        }
        #endregion


        #region IComposite implementation
        /// <summary>
        /// <para> Регистрирует компонент в коллекции. </para>
        /// <para> Если <paramref name="component"/> реализует <see cref="IService"/> и данная служба в текущий момент <see cref="IsActive"/>, то <paramref name="component"/> будет активирован. </para>
        /// </summary>
        /// <param name="component"></param>
        public virtual IComponent Register( IComponent component )
        {
            composite.Register( component );

            if (component is IService service)
            {
                Services.Add( service );
                if (IsActive) service.Activate();
            }

            return component;
        }

        /// <summary>
        /// <para> Удалает <paramref name="component"/> из коллекции. </para>
        /// <para> Если <paramref name="component"/> реализует <see cref="IService"/> и при этом активен, то <paramref name="component"/> будет деактивирован. </para>
        /// </summary>
        public virtual void Remove( IComponent component )
        {
            if (component is IService service)
            {
                Services.Remove( service );
                if (service.IsActive) service.Deactivate();
            }
            composite.Remove( component );
        }

        /// <summary>
        /// <para> Активная служба будет деактивирована вместе со всеми дочерними службами. </para>
        /// <para> Удаляются все элементы из коллекции. </para>
        /// </summary>
        public virtual void Dispose()
        {
            if (IsActive) Deactivate();

            composite.Dispose();

#if DEBUG
            if (Services.Count > 0)
                throw new Errors.MvpServiceException( $"{nameof( IDisposable )} is not implemented properly." ); 
#endif

        }
        #endregion

    }
}
