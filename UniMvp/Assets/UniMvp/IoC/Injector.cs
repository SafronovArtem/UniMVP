using UniMvp.Interfaces;
using UniMvp.IoC.Factory;
using UniMvp.IoC.Interfaces;

namespace UniMvp.IoC
{
    public partial class Injector
    {

        #region Private members
        private static IDependencyInjector concreteInjector;

        protected void SetInjector( IDependencyInjector value )
            => Injector.concreteInjector = value;

        private static IDependencyInjector ConcreteInjector
            => concreteInjector ?? (concreteInjector = new DependencyInjector( Factory, BindControl, Bind ));

        #endregion


        #region Fast injection
        public static IDependencyInjector From( object source )
            => ConcreteInjector.From( source );

        public static IDependencyInjector From<T>() where T : new()
            => ConcreteInjector.From<T>();

        public static IDependencyInjector To( object target )
            => ConcreteInjector.To( target );
        #endregion


        #region Bootstrap
        public static IDependencyInjector Setup<T>() where T : IBootstrapControl, new()
            => ConcreteInjector.Setup<T>();

        public static IDependencyInjector SetupValues<T>() where T : IBootstrapValue, new()
            => ConcreteInjector.SetupValues<T>();
        #endregion


        #region Produce controls
        public static IDependencyInjector Enqueue( object value )
            => ConcreteInjector.Enqueue( value );

        public IDependencyInjector Produce()
            => ConcreteInjector.Produce();

        public static IDependencyInjector Produce( object value )
            => ConcreteInjector.Produce( value );

        public static IDependencyInjector Produce( params object[] values )
            => ConcreteInjector.Produce( values );
        #endregion


        #region Save values
        public static IDependencyInjector Save( object value )
            => ConcreteInjector.Save( value );

        public static IDependencyInjector Save( object value, string key )
            => ConcreteInjector.Save( value, key );

        public static IDependencyInjector Save( IControl control )
            => ConcreteInjector.Save( control );
        #endregion

    }
}
