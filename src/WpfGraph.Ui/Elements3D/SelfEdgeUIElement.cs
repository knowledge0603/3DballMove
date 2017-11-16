using System.Windows.Media.Media3D;
using Palmmedia.WpfGraph.Core;
using Palmmedia.WpfGraph.UI.Elements3D.Tesselate;
using Palmmedia.WpfGraph.UI.ViewModels;

namespace Palmmedia.WpfGraph.UI.Elements3D
{
    /// <summary>
    /// Represents an edge starting and ending at the same node.
    /// </summary>
    /// <typeparam name="TNodeType">The type of the data attached to a <see cref="Palmmedia.WpfGraph.Core.Node&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
    /// <typeparam name="TEdgeType">The type of the data attached to an <see cref="Palmmedia.WpfGraph.Core.Edge&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
    internal class SelfEdgeUIElement<TNodeType, TEdgeType> : EdgeUIElement<TNodeType, TEdgeType>
        where TNodeType : NodeData
        where TEdgeType : EdgeData
    {
        /// <summary>
        /// <see cref="MeshGeometry3D"/> used as prototype.
        /// </summary>
        private static readonly MeshGeometry3D torusPrototype = TorusTesselate.Create(30, 10, 0.3, NODERADIUS);

        /// <summary>
        /// Initializes a new instance of the <see cref="SelfEdgeUIElement&lt;TNodeType, TEdgeType&gt;"/> class.
        /// </summary>
        /// <param name="edge">The edge.</param>
        /// <param name="translateTransform">The <see cref="TranslateTransform3D"/> of the visual element representing the node of the edge.</param>
        public SelfEdgeUIElement(Edge<TNodeType, TEdgeType> edge, TranslateTransform3D translateTransform)
            : base(edge)
        {
            this.Transform = translateTransform;
        }

        /// <summary>
        /// Participates in rendering operations when overridden in a derived class.
        /// </summary>
        protected override void OnUpdateModel()
        {
            var model = new GeometryModel3D(
                torusPrototype,
                new DiffuseMaterial(this.CreateBrush()));

            this.Model = model;
        }
    }
}
