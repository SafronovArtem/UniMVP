using System;

namespace UniMvp
{
    public class SaveMethodAttribute : Attribute
    {
        /// <summary>
        /// <para> Под индексом 0 - ключ возвращаемого значения ; </para>
        /// <para> Под индексами > 0 - ключи параметров ; </para>
        /// </summary>
        public readonly string[] keys;


        /// <summary>
        /// Кэшировать метод в коллекции DI ;
        /// <para> Доступ к методу осуществляется по имени возвращаемого типа + <paramref name="key"/>[ 0 ] </para>
        /// <para> Все принимаемые значения, будут взяты из DI контейнера по названию их типа + <paramref name="keys"/>[ n+1 ] и переданы при вызове метода ; </para>
        /// </summary>
        /// <param name="keys"><paramref name="keys"/>[0] - ключ доступа к методу в коллекции DI ; <paramref name="keys"/>[n+1] - ключи доступа к значению принимаемого параметра ;</param>
        public SaveMethodAttribute( params string[] keys )
        {
            this.keys = keys;
        }

        public string Key => GetParameterKey( -1 );

        public int CountParameters => keys.Length - 1;

        public string GetParameterKey( int index )
        {
            return (keys != null && index < CountParameters) ? keys[ index + 1 ] : string.Empty;
        }
    }
}
