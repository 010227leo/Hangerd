﻿using System;

namespace Hangerd.Uow
{
	public abstract class UnitOfWorkBase : Disposable, IUnitOfWork
	{
		public event EventHandler Completed;

		public string Id { get; private set; }

		protected UnitOfWorkBase()
		{
			Id = Guid.NewGuid().ToString("N");
		}

		public void Commit()
		{
			CommitUow();

			OnCompleted();
		}

		protected virtual void OnCompleted()
		{
			var completed = Completed;

			if (completed == null)
				return;

			completed(this, EventArgs.Empty);
		}

		protected abstract void CommitUow();
	}
}
