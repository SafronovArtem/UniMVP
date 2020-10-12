using System;

namespace UniMvp.Errors
{
    public class MvpControlException : Exception
    {
        public MvpControlException()
        {
        }

        public MvpControlException( string message ) : base( message )
        {
        }

        public MvpControlException( string message, Exception innerException ) : base( message, innerException )
        {
        }

    }
}
