using System;
using System.Windows;
using NHibernate;

namespace WindowsTasksPlanning.Infrastructure
{
    public abstract class AbstractPresenter<TModel, TView> : IDisposable, IPresenter
        where TView : Window, new()
    {
        private TModel _model;
        private ISessionFactory _sessionFactory;
        private ISession _session;
        private IStatelessSession _statelessSession;

        protected AbstractPresenter()
        {
            View = new TView();
            View.Closed += (sender, args) => Dispose();
        }

        DependencyObject IPresenter.View { get { return View; } }

        public object Result { get; protected set; }

        protected TView View { get; set; }

        protected ISessionFactory SessionFactory
        {
            get { return _sessionFactory; }
        }

        protected ISession Session
        {
            get { return _session ?? (_session = _sessionFactory.OpenSession()); }
        }

        protected IStatelessSession StatelessSession
        {
            get { return _statelessSession ?? (_statelessSession = _sessionFactory.OpenStatelessSession()); }
        }

        protected TModel Model
        {
            get { return _model; }
            set
            {
                _model = value;
                View.DataContext = _model;
            }
        }

        protected void ReplaceSessionAfterError()
        {
            if (_session != null)
            {
                _session.Dispose();
                _session = _sessionFactory.OpenSession();
                ReplaceEntitiesLoadedByFaultedSession();
            }
            if (_statelessSession == null) return;
            _statelessSession.Dispose();
            _statelessSession = _sessionFactory.OpenStatelessSession();
        }

        protected virtual void ReplaceEntitiesLoadedByFaultedSession()
        {
            throw new InvalidOperationException(
                @"ReplaceSessionAfterError was called, but the presenter does not override ReplaceEntitiesLoadedByFaultedSession!
                You must override ReplaceEntitiesLoadedByFaultedSession to call ReplaceSessionAfterError.");
        }
        
        public void SetSessionFactory(ISessionFactory theSessionFactory)
        {
            _sessionFactory = theSessionFactory;
        }

        public void Show()
        {
            View.Show();
        }

        public void ShowDialog()
        {
            View.ShowDialog();
        }

        public event Action Disposed = delegate { };

        public virtual void Dispose()
        {
            if (_session != null)
            {
                _session.Dispose();
            }
            if (_statelessSession != null)
            {
                _statelessSession.Dispose();
            }
            View.Close();
            Disposed();
        }
    }
}
