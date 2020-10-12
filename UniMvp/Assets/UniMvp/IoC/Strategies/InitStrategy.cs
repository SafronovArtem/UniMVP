using UniMvp.Interfaces;
using UniMvp.IoC.Interfaces;

namespace UniMvp.IoC.Strategies
{
    public sealed class InitStrategy : IBuildStrategy
    {
        public void Execute( object target )
        {
            if (target is IInitializable initializable)
            {
                initializable.Initialize();
            }
        }
    }
}
