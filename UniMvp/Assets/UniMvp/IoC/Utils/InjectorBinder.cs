using UniMvp.IoC.Containers;
using UniMvp.IoC.Interfaces;

namespace UniMvp.IoC
{
    public partial class Injector
    {
        private static IValueBinder valueBinder;

        protected IValueBinder SetBinder( IValueBinder value )
            => Injector.valueBinder = value;

        public static IValueBinder Bind
            => valueBinder ?? (valueBinder = new ValueBinder());

    }
}
