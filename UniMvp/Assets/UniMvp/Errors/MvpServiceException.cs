using System;

namespace UniMvp.Errors
{
    public class MvpServiceException : Exception
    {
        public MvpServiceException()
        {
        }

        public MvpServiceException( string message ) : base( message )
        {
        }

        public MvpServiceException( string message, Exception innerException ) : base( message, innerException )
        {
        }

    }
}
