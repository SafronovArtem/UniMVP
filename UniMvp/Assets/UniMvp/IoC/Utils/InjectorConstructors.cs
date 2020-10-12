using UniMvp.IoC.Containers;
using UniMvp.IoC.Factory;
using UniMvp.IoC.Interfaces;

namespace UniMvp.IoC
{
    /// <summary>
    /// Набор статических DI методов.
    /// </summary>
    public partial class Injector
    {
        protected Injector()
        {
            Clear();
        }

        public Injector( IDependencyFactory factory ) : this()
        {
            SetFactory( factory );
            SetBinder( new ValueBinder( factory.Values ) );
            SetControlBinder( new PresenterBinder( factory.Presenters ) );
            SetInjector( new DependencyInjector( Factory, BindControl, Bind ) );
        }

        public Injector( IValueBinder valueBinder, IPresenterBinder controlBinder ) : this()
        {
            SetBinder( valueBinder );
            SetControlBinder( controlBinder );
            SetFactory( new DependencyFactory( valueBinder.Container, controlBinder.Container ) );
            SetInjector( new DependencyInjector( Factory, BindControl, Bind ) );
        }

        public Injector( IDependencyInjector injector, IDependencyFactory factory, IValueBinder valueBinder, IPresenterBinder controlBinder ) : this()
        {
            SetInjector( injector );
            SetFactory( factory );
            SetBinder( valueBinder );
            SetControlBinder( controlBinder );
        }

        public static void Clear()
        {
            Injector.factory?.Clear();
            Injector.controlBinder?.Clear();
            Injector.valueBinder?.Clear();
            Injector.concreteInjector?.Clear();
        }

    }
}
