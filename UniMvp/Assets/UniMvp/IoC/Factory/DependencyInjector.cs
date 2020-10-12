using System;
using UniMvp.Interfaces;
using UniMvp.IoC.Building;
using UniMvp.IoC.Interfaces;

namespace UniMvp.IoC.Factory
{
    public sealed class DependencyInjector : IDependencyInjector
    {
        private readonly IBuildQueue find;
        private readonly IBuildQueue insert;


        public DependencyInjector()
        {
            Factory = Builder.CreateFactory( out IValueBinder valueBinder, out IPresenterBinder presenterBinder );
            Presenter = presenterBinder;
            Value = valueBinder;

            find = Builder.FindDependencies.InFieldsPropertiesAndMethods( Value.Container );
            insert = Builder.InsertDependencies.IntoFieldsPropertiesAndMethods( Value.Container );
        }

        public DependencyInjector( IDependencyFactory factory, IPresenterBinder controlBinder, IValueBinder valueBinder )
        {
            Factory = factory;
            Value = valueBinder;
            Presenter = controlBinder;

            find = Builder.FindDependencies.InFieldsPropertiesAndMethods( Value.Container );
            insert = Builder.InsertDependencies.IntoFieldsPropertiesAndMethods( Value.Container );
        }

        public DependencyInjector( IDependencyFactory factory, IPresenterBinder controlBinder, IValueBinder valueBinder, IBuildQueue find, IBuildQueue insert )
        {
            Factory = factory;
            Value = valueBinder;
            Presenter = controlBinder;
            this.find = find;
            this.insert = insert;
        }

        public IValueBinder Value { get; }

        public IPresenterBinder Presenter { get; }

        public IDependencyFactory Factory { get; }

        public void Clear()
        {
            Value.Clear();
            Presenter.Clear();
            find.Clear();
            insert.Clear();
        }

        public IDependencyInjector From( object source )
        {
            find.Enqueue( source );

            if (!find.IsExecuting)
                find.Execute();

            return this;
        }

        public IDependencyInjector From<T>() where T : new()
        {
            From( Activator.CreateInstance( typeof( T ) ) );
            return this;
        }

        public IDependencyInjector Setup<T>() where T : IBootstrapControl, new()
        {
            (Activator.CreateInstance( typeof( T ) ) as IBootstrapControl).Setup( Presenter );
            return this;
        }

        public IDependencyInjector SetupValues<T>() where T : IBootstrapValue, new()
        {
            (Activator.CreateInstance( typeof( T ) ) as IBootstrapValue).Setup( Value );
            return this;
        }

        public IDependencyInjector To( object target )
        {
            insert.Enqueue( target );

            if (!insert.IsExecuting)
                insert.Execute();

            return this;
        }

        public IDependencyInjector Produce( object value )
        {
            Factory.Enqueue( value );
            Factory.Produce();
            return this;
        }

        public IDependencyInjector Produce( params object[] value )
        {
            for (var i = 0; i < value.Length; i++)
                Factory.Enqueue( value[ i ] );

            Factory.Produce();
            return this;
        }

        public IDependencyInjector Enqueue( object value )
        {
            Factory.Enqueue( value );
            return this;
        }

        public IDependencyInjector Produce()
        {
            Factory.Produce();
            return this;
        }

        public IDependencyInjector Save( object value )
        {
            Value.Set( value );
            return this;
        }

        public IDependencyInjector Save( object value, string key )
        {
            Value.Set( value, key );
            return this;
        }

        public IDependencyInjector Save( IControl control )
        {
            Value.Set( control, control.Name );
            return this;
        }
    }
}
