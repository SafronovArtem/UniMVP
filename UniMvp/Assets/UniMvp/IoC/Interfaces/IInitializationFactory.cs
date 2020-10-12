namespace UniMvp.IoC.Interfaces
{
    /// <summary>
    /// Фабрика инициализации объектов.
    /// <para> Служба разделяет инициализацию объектов на несколько этапов: </para>
    /// <para> <see cref="OnFind"/> - поиск и кэширование значений у всех объектов очереди помеченых атрибутами для DI фабрики ; </para>
    /// <para> <see cref="OnInject"/> - внедрение значений всем объектам очереди с атрибутами для DI фабрики ; </para>
    /// <para> <see cref="OnInitialize"/> - инициализация всех объектов очереди, реализующих <see cref="UniMvp.Interfaces.IInitializable"/> ; </para>
    /// </summary>
    public interface IInitializationFactory
    {
        /// <summary>
        /// Есть ли объекты в очереди для инициализации.
        /// </summary>
        bool HasQueue();

        /// <summary>
        /// Добавить объект в очередь для инициализации.
        /// </summary>
        IInitializationFactory Enqueue( object target );

        /// <summary>
        /// Запустить инициализацию всех объектов из очереди поэтапно.
        /// </summary>
        void Produce();

        /// <summary>
        /// Поиск и кэширование значений у всех объектов очереди помеченых атрибутами для DI фабрики.
        /// </summary>
        void OnFind();

        /// <summary>
        /// Внедрение значений всем объектам очереди с атрибутами для DI фабрики.
        /// </summary>
        void OnInject();

        /// <summary>
        /// Инициализация всех объектов очереди, реализующих <see cref="UniMvp.Interfaces.IInitializable"/>.
        /// </summary>
        void OnInitialize();

        void Clear();
    }
}
