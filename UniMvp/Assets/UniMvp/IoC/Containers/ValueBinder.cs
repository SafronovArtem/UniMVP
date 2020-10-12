using System;
using UniMvp.IoC.Building;
using UniMvp.IoC.Interfaces;

namespace UniMvp.IoC.Containers
{
    public class ValueBinder : IValueBinder
    {
        public ValueBinder() : this( Builder.CreateValueContainer() )
        {
        }

        public ValueBinder( IDependencyContainer container )
        {
            Container = container;
        }

        public IAliasMaker Alias => Container.Alias;

        public IDependencyContainer Container { get; }

        public virtual void Clear()
        {
            Container.Clear();
        }

        #region Remove

        public IValueBinder Remove<T>()
        {
            Container.Remove( Alias.Nameof<T>() );
            return this;
        }

        public IValueBinder Remove<T>( string key )
        {
            Container.Remove( Alias.Nameof<T>( key ) );
            return this;
        }

        public IValueBinder Remove( Type type )
        {
            Container.Remove( Alias.Nameof( type ) );
            return this;
        }

        public IValueBinder Remove( Type type, string key )
        {
            Container.Remove( Alias.Nameof( type, key ) );
            return this;
        }
        public IValueBinder Remove( object value )
        {
            Container.Remove( Alias.Nameof( value ) );
            return this;
        }

        public IValueBinder Remove( object value, string key )
        {
            Container.Remove( Alias.Nameof( value, key ) );
            return this;
        }
        #endregion

        public IValueBinder Set<T>() where T : new()
        {
            Container.Set( Alias.Nameof<T>(), () => Activator.CreateInstance( typeof( T ) ) );
            return this;
        }

        public IValueBinder Set<T>( string key ) where T : new()
        {
            Container.Set( Alias.Nameof<T>( key ), () => Activator.CreateInstance( typeof( T ) ) );
            return this;
        }

        public IValueBinder Set<TValue, TBase>() where TValue : TBase, new()
        {
            Container.Set( Alias.Nameof<TBase>(), () => Activator.CreateInstance( typeof( TValue ) ) );
            return this;
        }

        public IValueBinder Set<TValue, TBase>( string key ) where TValue : TBase, new()
        {
            Container.Set( Alias.Nameof<TBase>( key ), () => Activator.CreateInstance( typeof( TValue ) ) );
            return this;
        }

        public IValueBinder Set<TBase>( object value )
        {
            Container.Set( Alias.Nameof<TBase>(), () => value );
            return this;
        }

        public IValueBinder Set<TBase>( object value, string key )
        {
            Container.Set( Alias.Nameof<TBase>( key ), () => value );
            return this;
        }

        public IValueBinder Set<T>( Func<object> strategy )
        {
            Container.Set( Alias.Nameof<T>(), strategy );
            return this;
        }

        public IValueBinder Set<T>( string key, Func<object> strategy )
        {
            Container.Set( Alias.Nameof<T>( key ), strategy );
            return this;
        }

        public IValueBinder Set( object value )
        {
            Container.Set( Alias.Nameof( value ), value );
            return this;
        }

        public IValueBinder Set( object value, string key )
        {
            Container.Set( Alias.Nameof( value, key ), value );
            return this;
        }


    }
}
