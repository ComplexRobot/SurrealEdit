using System;
using System.Collections.Generic;

namespace SurrealEdit.Nodes;

/// <summary>
/// <see langword="abstract"/> base class for <seealso cref="INode">Node interface</seealso>.
/// </summary>
public abstract class Node : INode {
	/// <inheritdoc/>
	public abstract Dictionary<string, INodeInput> Inputs { get; set; }
	/// <inheritdoc/>
	public abstract Dictionary<string, INodeIO> Outputs { get; set; }

	/// <inheritdoc/>
	public abstract void Process();

	/// <inheritdoc/>
	public virtual void Dispose() => GC.SuppressFinalize(this);
}
