using System;

namespace UniMvp.IoC.Interfaces
{
    public interface IDependencyContainer 
    {
        IAliasMaker Alias { get; }

        /// <summary>
        /// Set value.
        /// </summary>
        void Set( string alias, object value );

        /// <summary>
        /// Set strategy.
        /// </summary>
        void Set( string alias, Func<object> strategy );

        /// <summary>
        /// Get value.
        /// <para> Throw <see cref="Errors.IoCValueNotFoundException"/> if value was not added to the injection container by <paramref name="alias"/> </para>
        /// </summary>
        object Get( string alias );

        /// <summary>
        /// Get value.
        /// </summary>
        T Get<T>( string alias );

        void Remove( string alias );

        bool Contains( string alias );

        void Clear();

    }
}
