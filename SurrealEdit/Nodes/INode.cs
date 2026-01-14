using System;
using System.Collections.Generic;

namespace SurrealEdit.Nodes;

/// <summary>
/// A single-responsibility node for processing data.
/// </summary>
public interface INode : IDescriptor, IDisposable {
	/// <summary>
	/// The named input data of the node.
	/// </summary>
	Dictionary<string, INodeInput> Inputs { get; set; }
	/// <summary>
	/// The named output data of the node.
	/// </summary>
	Dictionary<string, INodeIO> Outputs { get; set; }

	/// <summary>
	/// Process the inputs and set the outputs.<br/>
	/// Do whatever this node is responsible for.
	/// </summary>
	void Process();
}
