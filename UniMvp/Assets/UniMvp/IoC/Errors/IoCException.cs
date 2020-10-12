using System;

namespace UniMvp.IoC.Errors
{
    public class IoCException : Exception
    {
        public IoCException()
        {
        }

        public IoCException( string message ) : base( message )
        {
        }

        public IoCException( string message, Exception innerException ) : base( message, innerException )
        {
        }

    }
}
