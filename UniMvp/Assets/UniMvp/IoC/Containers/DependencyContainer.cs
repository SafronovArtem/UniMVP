using System;
using System.Collections.Generic;
using UniMvp.IoC.Errors;
using UniMvp.IoC.Interfaces;

namespace UniMvp.IoC.Containers
{
    public class DependencyContainer : IDependencyContainer
    {
        private readonly Dictionary<string, Func<object>> cache = new Dictionary<string, Func<object>>();

        public DependencyContainer() : this( new AliasMaker() ) { }
        public DependencyContainer( IAliasMaker alias )
        {
            Alias = alias;
        }

        public IAliasMaker Alias { get; }

        public virtual void Clear()
        {
            cache.Clear();
        }

        public object Get( string alias )
        {
            try
            {
                return cache[ alias ].Invoke();
            }
            catch (KeyNotFoundException)
            {
                throw new IoCValueNotFoundException( $"Injection container doesn't have any value by alias: {alias};" );
            }
        }

        public T Get<T>( string alias )
        {
            object value = null;
            try
            {
                value = Get( alias );
                return (T)value;
            }
            catch (InvalidCastException)
            {
                var valueType = value is null ? "null" : value.GetType().FullName;
                throw new IoCValueInvalidCastException( $"Value doesn't mutch required type. Value: {valueType}, Type: {typeof( T ).FullName}, Flias: {alias}." );
            }
        }

        public virtual void Set( string alias, object value )
        {
            Set( alias, () => value );
        }

        public void Set( string alias, Func<object> strategy )
        {
            try
            {
                cache.Add( alias, strategy );
            }
            catch (Exception)
            {
                throw new IoCValueDuplicateException( $"Can't save dependency injection strategy by alias which has been already defined: {alias}. " );
            }
        }

        public void Remove( string alias )
        {
            cache.Remove( alias );
        }

        public bool Contains( string alias )
        {
            return cache.ContainsKey( alias );
        }

    }
}
