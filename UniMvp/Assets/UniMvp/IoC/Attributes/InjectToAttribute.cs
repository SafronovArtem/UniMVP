using System;

namespace UniMvp
{
    public class InjectToAttribute : Attribute
    {
        public readonly string key;

        public InjectToAttribute() { }
        public InjectToAttribute( string key )
        {
            this.key = key;
        }


    }

}
