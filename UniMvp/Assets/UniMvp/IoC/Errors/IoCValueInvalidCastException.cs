using System;

namespace UniMvp.IoC.Errors
{
    public class IoCValueInvalidCastException : IoCException
    {
        public IoCValueInvalidCastException()
        {
        }

        public IoCValueInvalidCastException( string message ) : base( message )
        {
        }

        public IoCValueInvalidCastException( string message, Exception innerException ) : base( message, innerException )
        {
        }
    }
}
