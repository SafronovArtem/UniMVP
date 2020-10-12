using System;

namespace UniMvp.Errors
{
    public class MvpControllerException : Exception
    {
        public MvpControllerException()
        {
        }

        public MvpControllerException( string message ) : base( message )
        {
        }

        public MvpControllerException( string message, Exception innerException ) : base( message, innerException )
        {
        }

    }
}
