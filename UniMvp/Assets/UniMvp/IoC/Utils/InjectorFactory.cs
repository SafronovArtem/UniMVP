using UniMvp.IoC.Factory;
using UniMvp.IoC.Interfaces;

namespace UniMvp.IoC
{
    public partial class Injector
    {
        private static IDependencyFactory factory;

        protected IDependencyFactory SetFactory( IDependencyFactory value )
            => Injector.factory = value;

        public static IDependencyFactory Factory
            => factory ?? (factory = new DependencyFactory( Bind.Container, BindControl.Container ));

    }
}
