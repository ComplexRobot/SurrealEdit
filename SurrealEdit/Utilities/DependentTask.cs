using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurrealEdit.Utilities;

/// <summary>
/// Helper class for generating <see cref="Task">Tasks</see> which wait for a collection of Tasks to complete before
/// completing an action.
/// </summary>
public static class DependentTask {
	/// <summary>
	/// Creates a <see cref="Task">Task</see> that runs an awaitable action after first waiting for a collection of
	/// Tasks to complete.
	/// </summary>
	/// <param name="dependedTasks">The collection of Tasks to wait for before completing the action.</param>
	/// <param name="action">The awaitable action to complete.</param>
	/// <returns>
	/// A Task that <see cref="TaskStatus.RanToCompletion">completes</see> after the <paramref name="dependedTasks"/>
	/// and the <paramref name="action"/> completes. Or a <see cref="TaskStatus.Faulted">Faulted</see> state if an
	/// exception occurred.
	/// </returns>
	public static Task CompleteAfter(IEnumerable<Task> dependedTasks, Func<Task> action) {
		var taskCompletionSource = new TaskCompletionSource();

		Task.Run(async () => {
			try {
				await Task.WhenAll(dependedTasks);
				await action();
				taskCompletionSource.SetResult();
			} catch (Exception exception) {
				taskCompletionSource.SetException(exception);
			}
		});

		return taskCompletionSource.Task;
	}
}
