using System;
using System.Windows;
using NHibernate;

namespace WindowsTasksPlanning.Infrastructure
{
    public interface IPresenter
    {
        /// <summary>
        /// Get the View
        /// </summary>
        DependencyObject View { get; }
        /// <summary>
        /// Set the session factory 
        /// </summary>
        void SetSessionFactory(ISessionFactory sessionFactory);

        void Show();

        object Result { get; }

        event Action Disposed;

        void ShowDialog();

    }
}
