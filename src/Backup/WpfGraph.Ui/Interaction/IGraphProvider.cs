using System.Windows.Media.Media3D;
using Palmmedia.WpfGraph.Core;
using Palmmedia.WpfGraph.UI.ViewModels;

namespace Palmmedia.WpfGraph.UI.Interaction
{
    /// <summary>
    /// Interface to enable access to currently displayed graph.
    /// </summary>
    /// <typeparam name="TNodeType">The type of the data attached to a <see cref="Palmmedia.WpfGraph.Core.Node&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
    /// <typeparam name="TEdgeType">The type of the data attached to an <see cref="Palmmedia.WpfGraph.Core.Edge&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
    public interface IGraphProvider<TNodeType, TEdgeType>
    {
        /// <summary>
        /// Gets or sets the graph.
        /// </summary>
        IGraph<TNodeType, TEdgeType> Graph { get; set; }

        /// <summary>
        /// Gets or sets the currently selected node or edge.
        /// </summary>
        GraphElement<TNodeType, TEdgeType> SelectedElement { get; set; }

        /// <summary>
        /// Gets or sets the container displaying the graph elements.
        /// </summary>
        ContainerUIElement3D Container { get; set; }
    }
}
