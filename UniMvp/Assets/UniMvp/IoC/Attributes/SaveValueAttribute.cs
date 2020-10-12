using System;

namespace UniMvp
{
    public class SaveValueAttribute : Attribute
    {
        public readonly string key;
        public readonly bool useTypeFromValue;
        public readonly bool useTypeFromParameter;

        /// <summary>
        /// Кэшировать значение используя тип свойства.
        /// </summary>
        public SaveValueAttribute() : this( string.Empty, true, false ) { }

        /// <summary>
        /// Кэшировать значение с иникальным ключем <paramref name="key"/>, используя тип свойства.
        /// </summary>
        /// <param name="key">Уникальный ключ доступа к значению в коллекции DI ;</param>
        public SaveValueAttribute( string key ) : this( key, true, false ) { }

        /// <summary>
        /// Кэшировать значение с иникальным ключем <paramref name="key"/>, используя либо тип свойства, либо тип возвращаемого значения, либо оба ;
        /// </summary>
        /// <param name="key">Уникальный ключ доступа к значению в коллекции DI ;</param>
        /// <param name="getTypeFromParameter">Кэшировать значение по декларированому типу свойства, используя базовый класс или интерфейс ;</param>
        /// <param name="getTypeFromValue">Дополнителько кэшировать значение по типу самого возвращаемого значения, если оно отлично от типа свойства ;</param>
        public SaveValueAttribute( string key, bool getTypeFromParameter, bool getTypeFromValue )
        {
            this.key = key;
            this.useTypeFromValue = getTypeFromValue;
            this.useTypeFromParameter = getTypeFromParameter;
        }

    }
}
