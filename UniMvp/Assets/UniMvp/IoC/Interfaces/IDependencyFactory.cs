namespace UniMvp.IoC.Interfaces
{
    /// <summary>
    /// Фабрика по созданию объектов и настройке зависимостей.
    /// </summary>
    public interface IDependencyFactory
    {
        /// <summary>
        /// Dependency injection container.
        /// <para> Кэширует и поставляет значения для объектов. </para>
        /// </summary>
        IDependencyContainer Values { get; }

        /// <summary>
        /// Dependency injection container.
        /// <para> Кэширует правила и поставляет значения <see cref="UniMvp.Interfaces.IPresenter"/> для служб <see cref="UniMvp.Interfaces.IControl"/> </para>
        /// </summary>
        IDependencyContainer Presenters { get; }

        /// <summary>
        /// Добавить <paramref name="target"/> в очередь для настройки.
        /// </summary>
        IDependencyFactory Enqueue( object target );
        
        /// <summary>
        /// Очередь объектов для настройки.
        /// </summary>
        bool HasQueue();

        /// <summary>
        /// Запустить настройку всех объектов в очереди.
        /// </summary>
        void Produce();

        /// <summary>
        /// Запустить настройку конкретного объекта из очереди / вне очереди.
        /// </summary>
        void Produce( object target );

        void Clear();
    }
}
