using UniMvp.IoC.Containers;
using UniMvp.IoC.Interfaces;

namespace UniMvp.IoC
{
    public partial class Injector
    {
        private static IPresenterBinder controlBinder;

        protected void SetControlBinder( IPresenterBinder value )
            => Injector.controlBinder = value;

        /// <summary>
        /// Настройка значений презентеров <see cref="UniMvp.Interfaces.IPresenter"/> для служб <see cref="UniMvp.Interfaces.IControl"/>
        /// </summary>
        public static IPresenterBinder BindControl
            => controlBinder ?? (controlBinder = new PresenterBinder());

    }

}
