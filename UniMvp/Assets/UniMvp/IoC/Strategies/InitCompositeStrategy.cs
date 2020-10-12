using UniMvp.Interfaces;
using UniMvp.IoC.Interfaces;

namespace UniMvp.IoC.Strategies
{
    public sealed class InitCompositeStrategy : IBuildStrategy
    {
        private readonly IDependencyFactory factory;

        public InitCompositeStrategy( IDependencyFactory factory )
        {
            this.factory = factory;
        }

        public void Execute( object target )
        {
            if (target is IInitializable initializable)
            {
                initializable.Initialize();
            }
            if (target is IComposite composite)
            {
                for (var i = 0; i < composite.Elements.Count; i++)
                    factory.Produce( composite.Elements[ i ] );
            }
        }
    }
}
