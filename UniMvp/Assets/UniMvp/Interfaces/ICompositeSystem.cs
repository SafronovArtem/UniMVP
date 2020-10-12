using System.Collections.Generic;

namespace UniMvp.Interfaces
{
    public interface ICompositeSystem : ICompositeService, IExecutable
    {
        List<IExecutable> Executables { get; set; }

        void Prevent();
    }
}
