namespace UniMvp.IoC
{
    /// <summary>
    /// <para> Создать новый экземпляр объекта, если поле не имеет значения (без использования DI контейнера). </para>
    /// <para> Созданый / полученый объект будет помещен в фабрику <see cref="UniMvp.IoC.Interfaces.IDependencyFactory"/> для настройки. </para>
    /// </summary>
    public class ProduceAttribute : System.Attribute
    {
        public readonly string key;
        public readonly bool saveValue;

        /// <summary>
        /// <para> Создать новый экземпляр объекта, если поле не имеет значения (без использования DI контейнера). </para>
        /// <para> Созданый / полученый объект будет помещен в фабрику <see cref="UniMvp.IoC.Interfaces.IDependencyFactory"/> для настройки. </para>
        /// </summary>
        public ProduceAttribute() { }


        /// <summary>
        /// <para> Создать новый экземпляр объекта, если поле не имеет значения (без использования DI контейнера). </para>
        /// <para> Созданый / полученый объект будет помещен в фабрику <see cref="UniMvp.IoC.Interfaces.IDependencyFactory"/> для настройки. </para>
        /// </summary>
        public ProduceAttribute( string key )
        {
            this.key = key;
        }

        /// <summary>
        /// <para> Создать новый экземпляр объекта, если поле не имеет значения (без использования DI контейнера). </para>
        /// <para> Объект будет кэширован в фабрике <see cref="UniMvp.IoC.Interfaces.IDependencyFactory"/> если <paramref name="saveValue"/> is true. </para>
        /// <para> Созданый / полученый объект будет помещен в фабрику <see cref="UniMvp.IoC.Interfaces.IDependencyFactory"/> для настройки. </para>
        /// </summary>
        public ProduceAttribute( bool saveValue )
        {
            this.saveValue = saveValue;
        }

        /// <summary>
        /// <para> Создать новый экземпляр объекта, если поле не имеет значения (без использования DI контейнера). </para>
        /// <para> Объект будет кэширован в фабрике <see cref="UniMvp.IoC.Interfaces.IDependencyFactory"/> если <paramref name="saveValue"/> is true. </para>
        /// <para> Созданый / полученый объект будет помещен в фабрику <see cref="UniMvp.IoC.Interfaces.IDependencyFactory"/> для настройки. </para>
        /// </summary>
        public ProduceAttribute( string key, bool saveValue )
        {
            this.key = key;
            this.saveValue = saveValue;
        }

    }
}
