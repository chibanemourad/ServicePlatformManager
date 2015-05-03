using System;
using System.Windows;
using WindowsTasksPlanning.Events;
using WindowsTasksPlanning.Features.Merge;
using WindowsTasksPlanning.Infrastructure;
using WindowsTasksPlanning.Infrastructure.Impl;
using WindowsTasksPlanning.Model;
using NHibernate;

namespace WindowsTasksPlanning.Features.Edit
{
	public class Presenter : AbstractPresenter<Model, View>
	{
		public Presenter()
		{
			EventPublisher.Register<ActionUpdated>(RefreshAction);
		}

		private void RefreshAction(ActionUpdated actionUpdated)
		{
			if (actionUpdated.Id != Model.Action.Id)
				return;
			Session.Refresh(Model.Action);
		}

		public void Initialize(long id)
		{
			ServicePlatform action;
			using (var tx = Session.BeginTransaction())
			{
				action = Session.Get<ServicePlatform>(id);
				tx.Commit();
			}

			if (action == null)
				throw new InvalidOperationException("Action " + id + " does not exists");

			Model = new Model
			{
				Action = action
			};
		}

		public void OnCancel()
		{
			View.Close();
		}

		public void OnSave()
		{
			bool successfulSave;
			try
			{
				using (var tx = Session.BeginTransaction())
				{
					// this isn't strictly necessary, NHibernate will 
					// automatically do it for us, but it make things
					// more explicit
					Session.Update(Model.Action);

					tx.Commit();
				}
				successfulSave = true;
			}
			catch (StaleObjectStateException)
			{
				var mergeResult = Presenters.ShowDialog<MergeResult?>("Merge", Model.Action);
				successfulSave = mergeResult != null;

				ReplaceSessionAfterError();
			}

			// we call ActionUpdated anyway, either we updated the value ourselves
			// or we encountered a concurrency conflict, in which case we _still_
			// want other parts of the application to update themselves with the values
			// from the db
			EventPublisher.Publish(new ActionUpdated
			{
				Id = Model.Action.Id
			}, this);

			if (successfulSave)
				View.Close();
		}

		protected override void ReplaceEntitiesLoadedByFaultedSession()
		{
			Initialize(Model.Action.Id);
		}
	}
}