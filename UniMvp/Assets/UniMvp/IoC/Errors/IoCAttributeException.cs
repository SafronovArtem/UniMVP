using System;

namespace UniMvp.IoC.Errors
{
    public class IoCAttributeException : IoCException
    {
        public IoCAttributeException()
        {
        }

        public IoCAttributeException( string message ) : base( message )
        {
        }

        public IoCAttributeException( string message, Exception innerException ) : base( message, innerException )
        {
        }
    }
}
