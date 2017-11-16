using System.Windows;
using System.Windows.Media.Media3D;
using Palmmedia.WpfGraph.Core;
using Palmmedia.WpfGraph.UI.ViewModels;

namespace Palmmedia.WpfGraph.UI.Elements3D
{
    /// <summary>
    /// Factory class to instantiate <see cref="GraphUIElement">GraphUIElements</see>.
    /// </summary>
    internal static class VisualFactory
    {
        /// <summary>
        /// Creates the visual representation for an edge.
        /// </summary>
        /// <typeparam name="TNodeType">The type of the data attached to a <see cref="Palmmedia.WpfGraph.Core.Node&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
        /// <typeparam name="TEdgeType">The type of the data attached to an <see cref="Palmmedia.WpfGraph.Core.Edge&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
        /// <param name="edge">The edge.</param>
        /// <param name="translateTransform1">The <see cref="TranslateTransform3D"/> of the visual element representing the first node of the edge.</param>
        /// <param name="translateTransform2">The <see cref="TranslateTransform3D"/> of the visual element representing the second node of the edge.</param>
        /// <returns>The visual representation for an edge.</returns>
        public static UIElement3D CreateVisual<TNodeType, TEdgeType>(this Edge<TNodeType, TEdgeType> edge, TranslateTransform3D translateTransform1, TranslateTransform3D translateTransform2)
            where TNodeType : NodeData
            where TEdgeType : EdgeData
        {
            if (edge.Data == null)
            {
                edge.Data = (TEdgeType)new EdgeData();
            }
            
            if (edge.FirstNode == edge.SecondNode)
            {
                return new SelfEdgeUIElement<TNodeType, TEdgeType>(edge, translateTransform1);
            }
            else
            {
                return new RegularEdgeUIElement<TNodeType, TEdgeType>(edge, translateTransform1, translateTransform2);
            }
        }

        /// <summary>
        /// Creates the visual representation for a node.
        /// </summary>
        /// <typeparam name="TNodeType">The type of the data attached to a <see cref="Palmmedia.WpfGraph.Core.Node&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
        /// <typeparam name="TEdgeType">The type of the data attached to an <see cref="Palmmedia.WpfGraph.Core.Edge&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
        /// <param name="node">The node.</param>
        /// <returns>The visual representation for a node.</returns>
        public static NodeUIElement<TNodeType, TEdgeType> CreateVisual<TNodeType, TEdgeType>(this Node<TNodeType, TEdgeType> node)
            where TNodeType : NodeData
            where TEdgeType : EdgeData
        {
            if (node.Data == null)
            {
                node.Data = (TNodeType)new NodeData();
            }

            return new NodeUIElement<TNodeType, TEdgeType>(node);
        }
    }
}
