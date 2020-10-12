using UniMvp.IoC.Interfaces;

namespace UniMvp.IoC.Factory
{
    public sealed class InitializationFactory : IInitializationFactory
    {
        private readonly IBuildQueue find;
        private readonly IBuildQueue inject;
        private readonly IBuildQueue initialize;
        private bool isProducing;

        public InitializationFactory(
            IBuildQueue find,
            IBuildQueue inject,
            IBuildQueue initialize )
        {
            this.find = find;
            this.inject = inject;
            this.initialize = initialize;
        }

        public void Clear()
        {
            find?.Clear();
            inject?.Clear();
            initialize?.Clear();
        }

        public IInitializationFactory Enqueue( object target )
        {
            find?.Enqueue( target );
            inject?.Enqueue( target );
            initialize?.Enqueue( target );
            return this;
        }

        public bool HasQueue()
            => find != null && find.HasQueue()
            || inject != null && inject.HasQueue()
            || initialize != null && initialize.HasQueue();

        public void OnFind()
        {
            if (find != null && !find.IsExecuting)
            {
                find.Execute();
            }
        }

        public void OnInitialize()
        {
            if (initialize != null && !initialize.IsExecuting)
            {
                initialize.Execute();
            }
        }

        public void OnInject()
        {
            if (inject != null && !inject.IsExecuting)
            {
                inject.Execute();
            }
        }

        public void Produce()
        {
            if (!isProducing)
            {
                isProducing = true;

                while (HasQueue())
                {
                    OnFind();
                    OnInject();
                    OnInitialize();
                }

                isProducing = false;
            }
        }

        public void Produce( object target )
        {
            Enqueue( target );

            Produce();
        }
    }
}
