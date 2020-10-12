using System;

namespace UniMvp.Errors
{
    public class MvpSystemException : Exception
    {
        public MvpSystemException()
        {
        }

        public MvpSystemException( string message ) : base( message )
        {
        }

        public MvpSystemException( string message, Exception innerException ) : base( message, innerException )
        {
        }

    }
}
