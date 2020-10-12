using System.Collections.Generic;

namespace UniMvp.Interfaces
{
    public interface IPresenter : IService, IInitializable, IDisposable
    {
        IControl Control { get; set; }

        List<IComponent> Elements { get; }
    }
}
