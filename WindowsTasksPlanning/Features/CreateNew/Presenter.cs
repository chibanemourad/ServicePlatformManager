using System;
using System.ComponentModel;
using System.Threading;
using WindowsTasksPlanning.Events;
using WindowsTasksPlanning.Infrastructure;
using WindowsTasksPlanning.Infrastructure.Impl;
using WindowsTasksPlanning.Model;

namespace WindowsTasksPlanning.Features.CreateNew
{

	public class Presenter : AbstractPresenter<Model, View>
	{
		private readonly BackgroundWorker _saveBackgroundWorker;

		public Presenter()
		{
			_saveBackgroundWorker = new BackgroundWorker();
			_saveBackgroundWorker.DoWork += (sender, args) => PerformActualSave();
			_saveBackgroundWorker.RunWorkerCompleted += (sender, args) => CompleteSave();
			Model = new Model
			{
				Action = DataBindingFactory.Create<ServicePlatform>(),
				AllowEditing = new Observable<bool>(true)
			};
		}

		public void OnCancel()
		{
			View.Close();
		}

		public Fact CanSave
		{
			get { return new Fact(Model.AllowEditing, () => Model.AllowEditing); }
		}

		public Fact CanCancel
		{
			get
			{
				// we do not allow canceling when a save is in progress
				return new Fact(Model.AllowEditing, () => Model.AllowEditing);
			}
		}

		public void OnSave()
		{
			Model.AllowEditing.Value = false;
			_saveBackgroundWorker.RunWorkerAsync();
		}

		private void CompleteSave()
		{
			Model.AllowEditing.Value = true;
			EventPublisher.Publish(new ActionUpdated
			{
				Id = Model.Action.Id
			}, this);

			View.Close();
		}

		private void PerformActualSave()
		{
			Thread.Sleep(5000);// simulating a long save here
			using (var tx = Session.BeginTransaction())
			{
				Model.Action.CreationDate = DateTime.Now;

				Session.Save(Model.Action);
				tx.Commit();
			}
		}

		public override void Dispose()
		{
			_saveBackgroundWorker.Dispose();
			base.Dispose();
		}
	}
}