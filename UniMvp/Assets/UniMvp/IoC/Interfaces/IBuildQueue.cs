namespace UniMvp.IoC.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBuildQueue
    {
        bool IsExecuting { get; }

        bool HasQueue();

        /// <summary>
        /// Добавить элемент в очередь для обработки.
        /// </summary>
        void Enqueue( object target );

        /// <summary>
        /// Выполнить операцию для всех элементов очереди.
        /// </summary>
        void Execute();

        void Clear();
    }
}
