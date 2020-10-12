using System.Collections.Generic;
using UniMvp.Interfaces;
using UniMvp.IoC.Building;

namespace UniMvp.Systems
{
    public class CompositeSystem : ICompositeSystem
    {
        #region Fields and properties 
        private readonly ICompositeService compositeService;
        private bool prevented = true;

        public List<IExecutable> Executables { get; set; } = new List<IExecutable>();
        public List<IService> Services { get => compositeService.Services; set => compositeService.Services = value; }
        public List<IComponent> Elements { get => compositeService.Elements; set => compositeService.Elements = value; }

        public bool IsActive { get; private set; }
        #endregion


        #region Constructors
        public CompositeSystem()
        {
            compositeService = Builder.CreateCompositeService( this );
        }

        public CompositeSystem( IComposite parent )
        {
            compositeService = Builder.CreateCompositeService( parent );
        }

        public CompositeSystem( ICompositeService compositeService )
        {
            this.compositeService = compositeService;
        }
        #endregion


        /// <summary>
        /// <para> Вызывает метод <see cref="IExecutable.Execute"/> у всех компонентов, реализующих <see cref="IExecutable"/> </para>
        /// <para> Сгенерирует <see cref="Errors.MvpSystemException"/> если вызвать метод до активации системы. </para>
        /// </summary>
        public virtual void Execute()
        {
            if (IsActive)
            {
                prevented = false;

                for (var i = Executables.Count - 1; i > -1; i--)
                {
                    Executables[ i ].Execute();
                    if (prevented) break;
                }

                prevented = true;
            }
            else
            {
                throw new Errors.MvpSystemException( $"System must be activated befor calling {nameof( Execute )} method." );
            }
        }

        public virtual void Prevent()
        {
            prevented = true;
        }

        /// <summary>
        /// <para> Регистрирует компонент в системе. </para>
        /// <para> Если <paramref name="component"/> реализует <see cref="Interfaces.IExecutable"/>, то его метод <see cref="Interfaces.IExecutable.Execute"/> будет выполняться в порядке добавления в коллекцию. </para>
        /// </summary>
        public virtual IComponent Register( IComponent component )
        {
            if (component is IExecutable e) Executables.Insert( 0, e );
            return compositeService.Register( component );
        }

        /// <summary>
        /// <para> Удалает <paramref name="component"/> из всех коллекций. </para>
        /// </summary>
        public virtual void Remove( IComponent component )
        {
            if (component is IExecutable e) Executables.Remove( e );
            compositeService.Remove( component );
        }

        public virtual void Activate()
        {
            if (!IsActive)
            {
                IsActive = true;
                compositeService.Activate();
            }
        }

        public virtual void Deactivate()
        {
            if (IsActive)
            {
                IsActive = false;
                if (!prevented) Prevent();
                compositeService.Deactivate();
            }
        }

        public virtual void Dispose()
        {
            compositeService.Dispose();

            if (Executables.Count > 0)
                throw new Errors.MvpSystemException( $"Pattern {nameof( IDisposable )} is not implemented properly." );
        }

    }
}
