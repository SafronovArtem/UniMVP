using System;

namespace UniMvp
{
    public class InjectToMethodAttribute : Attribute
    {
        public readonly string[] keys;

        /// <summary>
        /// Внедрить в метод значения из контейнера DI ;
        /// <para> Все принимаемые значения, будут взяты из DI контейнера по названию их типа + <paramref name="keys"/>[ i ] и переданы при вызове метода ; </para>
        /// </summary>
        /// <param name="keys"><paramref name="keys"/>[i] - ключ доступа к значению в коллекции DI ; </param>
        public InjectToMethodAttribute( params string[] keys )
        {
            this.keys = keys;
        }
    }
}
