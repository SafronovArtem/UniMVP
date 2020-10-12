namespace UniMvp.Interfaces
{
    /// <summary>
    /// <para> Контроллер для решения задач, связанных со службой <see cref="Interfaces.IControl"/> </para>
    /// </summary>
    public interface IController : IComponent
    {
        IControl Control { get; set; }

    }

}
