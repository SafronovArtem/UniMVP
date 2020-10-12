using System.Collections.Generic;

namespace UniMvp.Interfaces
{
    public interface IComposite : IDisposable
    {
        List<IComponent> Elements { get; set; }

        IComponent Register( IComponent component );

        void Remove( IComponent component );
    }
}
