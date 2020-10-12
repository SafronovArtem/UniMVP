using System;
using UniMvp.IoC.Interfaces;

namespace UniMvp.IoC.Containers
{
    public sealed class AliasMaker : IAliasMaker
    {
        private readonly string name;
        private readonly string ending;
        private readonly string delimiter;

        public AliasMaker() : this( "default", ":" ) { }

        public AliasMaker( string name ) : this( name, ":" ) { }

        public AliasMaker( string name, string delimiter )
        {
            this.name = name;
            this.delimiter = delimiter;
            ending = delimiter + name;

        }
        public string Default() => name;

        public string NotNull( string value )
        {
            return string.IsNullOrEmpty( value ) ? name : value;
        }

        public string Ending() => ending;

        public string Ending( string key )
        {
            return string.IsNullOrEmpty( key ) ? ending : (delimiter + key);
        }

        public string Nameof<T>()
        {
            return typeof( T ).FullName + Ending();
        }

        public string Nameof<T>( string key )
        {
            return typeof( T ).FullName + Ending( key );
        }

        public string Nameof( object value )
        {
            return value.GetType().FullName + Ending();
        }

        public string Nameof( object value, string key )
        {
            return value.GetType().FullName + Ending( key );
        }

        public string Nameof( Type type )
        {
            return type.FullName + Ending();
        }

        public string Nameof( Type type, string key )
        {
            return type.FullName + Ending( key );
        }

        public IAliasMaker Clone()
        {
            return new AliasMaker( name, delimiter );
        }

    }
}
