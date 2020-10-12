namespace UniMvp.IoC.Interfaces
{
    public interface IAliasMaker
    {
        string Nameof<T>();

        string Nameof<T>( string key );

        string Nameof( object value );

        string Nameof( object value, string key );

        string Nameof( System.Type type );

        string Nameof( System.Type type, string key );

        string Default();

        string NotNull( string value );

        string Ending( string key );

        string Ending();

        IAliasMaker Clone();
    }
}
