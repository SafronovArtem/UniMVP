using System;

namespace UniMvp.IoC.Errors
{
    public class IoCValueNotFoundException : IoCException
    {
        public IoCValueNotFoundException()
        {
        }

        public IoCValueNotFoundException( string message ) : base( message )
        {
        }

        public IoCValueNotFoundException( string message, Exception innerException ) : base( message, innerException )
        {
        }
    }
}
