using System.Collections;
using UniMvp.Interfaces;
using UniMvp.IoC.Containers;
using UniMvp.IoC.Building;
using UniMvp.IoC.Interfaces;

namespace UniMvp.IoC.Factory
{
    public class DependencyFactory : IDependencyFactory
    {
        private readonly IInitializationFactory controls;
        private readonly IInitializationFactory services;
        private readonly IInitializationFactory presenters;
        private readonly IInitializationFactory components;
        private readonly IInitializationFactory common;

        private Queue queue;
        private bool isProducing;

        public IDependencyContainer Values { get; }

        public IDependencyContainer Presenters { get; }

        public DependencyFactory() : this( Builder.CreateValueContainer(), Builder.CreatePresenterContainer() )
        {
        }

        public DependencyFactory( IDependencyContainer valueContainer, IDependencyContainer controlsContainer )
        {
            Values = valueContainer;
            Presenters = controlsContainer;

            controls = Builder.InitializorFor.Controls( this, valueContainer, controlsContainer );
            services = Builder.InitializorFor.Services( this, valueContainer );
            presenters = Builder.InitializorFor.Presenter( this, valueContainer );
            components = Builder.InitializorFor.Components( valueContainer );
            common = Builder.InitializorFor.Common( valueContainer );
        }

        protected DependencyFactory(
            IDependencyContainer valueContainer,
            IDependencyContainer presenterContainer,

            IInitializationFactory controls,
            IInitializationFactory services,
            IInitializationFactory presenters,
            IInitializationFactory components,
            IInitializationFactory common )
        {
            Values = valueContainer;
            Presenters = presenterContainer;
            this.controls = controls;
            this.services = services;
            this.presenters = presenters;
            this.components = components;
            this.common = common;
        }

        private Queue GetUnsortedQueue() => queue ?? (queue = new Queue());

        private bool HasUnsortedQueue() => queue != null && queue.Count > 0;

        public bool HasQueue()
            => HasUnsortedQueue()
            || controls.HasQueue()
            || services.HasQueue()
            || presenters.HasQueue()
            || components.HasQueue()
            || common.HasQueue();

        public IDependencyFactory Enqueue( object target )
        {
            GetUnsortedQueue().Enqueue( target );
            return this;
        }

        public void Produce()
        {
            if (!isProducing)
            {
                isProducing = true;

                while (HasUnsortedQueue())
                    Produce( GetUnsortedQueue().Dequeue() );

                controls.Produce();
                services.Produce();
                presenters.Produce();
                components.Produce();
                common.Produce();

                isProducing = false;
            }
            if (HasQueue()) Produce();

        }

        public void Produce( object target )
        {
            if (target is IControl) controls.Enqueue( target );
            else if (target is IPresenter) presenters.Enqueue( target );
            else if (target is IService) services.Enqueue( target );
            else if (target is IComponent) components.Enqueue( target );
            else common.Enqueue( target );

            controls.OnFind();
            services.OnFind();
            common.OnFind();

            if (!isProducing) Produce();
        }

        public void Clear()
        {
            controls?.Clear();
            presenters?.Clear();
            services?.Clear();
            components?.Clear();
            common?.Clear();
        }
    }
}
