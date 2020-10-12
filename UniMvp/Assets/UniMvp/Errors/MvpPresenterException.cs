using System;

namespace UniMvp.Errors
{
    public class MvpPresenterException : Exception
    {
        public MvpPresenterException()
        {
        }

        public MvpPresenterException( string message ) : base( message )
        {
        }

        public MvpPresenterException( string message, Exception innerException ) : base( message, innerException )
        {
        }

    }
}
