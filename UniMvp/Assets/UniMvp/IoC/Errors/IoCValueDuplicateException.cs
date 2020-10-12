using System;

namespace UniMvp.IoC.Errors
{
    public class IoCValueDuplicateException : IoCException
    {
        public IoCValueDuplicateException()
        {
        }

        public IoCValueDuplicateException( string message ) : base( message )
        {
        }

        public IoCValueDuplicateException( string message, Exception innerException ) : base( message, innerException )
        {
        }
    }
}
