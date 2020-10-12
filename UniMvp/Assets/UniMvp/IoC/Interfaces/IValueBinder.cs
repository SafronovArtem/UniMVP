using System;

namespace UniMvp.IoC.Interfaces
{
    public interface IValueBinder 
    {
        
        IDependencyContainer Container { get; }


        IValueBinder Set<T>() where T : new();
        IValueBinder Set<T>( string key ) where T : new();

        IValueBinder Set<TValue, TBase>() where TValue : TBase, new();
        IValueBinder Set<TValue, TBase>( string key ) where TValue : TBase, new();

        IValueBinder Set<TBase>( object value );
        IValueBinder Set<TBase>( object value, string key );

        IValueBinder Set<T>( Func<object> strategy );
        IValueBinder Set<T>( string key, Func<object> strategy );

        IValueBinder Set( object value );
        IValueBinder Set( object value, string key );


        IValueBinder Remove<T>();
        IValueBinder Remove<T>( string key );

        IValueBinder Remove( Type type );
        IValueBinder Remove( Type type, string key );

        IValueBinder Remove( object value );
        IValueBinder Remove( object value, string key );

        void Clear();
    }
}
