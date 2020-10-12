using System.Collections.Generic;

namespace UniMvp.Interfaces
{
    public interface ICompositeService : IComposite, IService
    {
        List<IService> Services { get; set; }
    }
}
