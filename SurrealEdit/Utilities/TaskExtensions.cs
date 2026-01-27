using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurrealEdit.Utilities;

/// <summary>
/// Extension class for <see cref="Task">Tasks</see>.
/// </summary>
public static class TaskExtensions {
	extension(Task) {
		/// <summary>
		/// Creates a <see cref="Task">Task</see> that runs an action after first waiting for a collection of Tasks to
		/// complete.
		/// </summary>
		/// <remarks>
		/// The function returns immediately after queueing the task.<br/>
		/// It can be used to store the returned Task object and reference it in other contexts before the action
		/// starts.
		/// </remarks>
		/// <param name="blockingTasks">The collection of Tasks to wait for before completing the action.</param>
		/// <param name="action">The awaitable action to complete.</param>
		/// <returns>
		/// A Task that <see cref="TaskStatus.RanToCompletion">completes</see> after the
		/// <paramref name="blockingTasks"/> and then the <paramref name="action"/> completes.
		/// </returns>
		public static Task RunAfter(IEnumerable<Task> blockingTasks, Func<Task> action) {
			var taskCompletionSource = new TaskCompletionSource();

			Task.Run(async () => {
				try {
					await Task.WhenAll(blockingTasks);
					await action();
					taskCompletionSource.SetResult();
				} catch (Exception exception) {
					taskCompletionSource.SetException(exception);
				}
			});

			return taskCompletionSource.Task;
		}
	}
}
