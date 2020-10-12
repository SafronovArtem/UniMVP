using System.Collections;
using UniMvp.IoC.Interfaces;

namespace UniMvp.IoC.Strategies
{
    public sealed class BuildQueue : IBuildQueue
    {
        private Queue queue;
        private readonly IBuildStrategy[] strategies;

        public BuildQueue( params IBuildStrategy[] strategies )
        {
            this.strategies = strategies ?? new IBuildStrategy[] { };
        }

        public bool IsExecuting { get; private set; }

        private void Execute( object target )
        {
            for (var i = 0; i < strategies.Length; i++)
                strategies[ i ].Execute( target );
        }

        public void Enqueue( object injectee )
        {
            GetQueue().Enqueue( injectee );
        }

        public void Execute()
        {
            if (!IsExecuting && HasQueue())
            {
                IsExecuting = true;

                while (queue.Count > 0)
                    Execute( queue.Dequeue() );

                IsExecuting = false;
            }
        }

        private Queue GetQueue() => queue ?? (queue = new Queue());

        public bool HasQueue() => (queue != null && queue.Count > 0);

        public void Clear()
        {
            queue?.Clear();
        }
    }
}
