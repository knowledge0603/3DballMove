using System.Windows;
using System.Windows.Media;
using Palmmedia.WpfGraph.Common;
using Palmmedia.WpfGraph.Core;
using Palmmedia.WpfGraph.UI.Interaction;
using Palmmedia.WpfGraph.UI.ViewModels;

namespace Palmmedia.WpfGraph.UI.Elements3D
{
    /// <summary>
    /// Represents an edge.
    /// </summary>
    /// <typeparam name="TNodeType">The type of the data attached to a <see cref="Palmmedia.WpfGraph.Core.Node&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
    /// <typeparam name="TEdgeType">The type of the data attached to an <see cref="Palmmedia.WpfGraph.Core.Edge&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
    internal abstract class EdgeUIElement<TNodeType, TEdgeType> : GraphUIElement
        where TNodeType : NodeData
        where TEdgeType : EdgeData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeUIElement&lt;TNodeType, TEdgeType&gt;"/> class.
        /// </summary>
        /// <param name="edge">The edge.</param>
        public EdgeUIElement(Edge<TNodeType, TEdgeType> edge)
            : base(edge.Data)
        {
            this.Edge = edge;

            edge.EdgeDirectionChanged += new System.EventHandler<System.EventArgs>((s, e) => this.InvalidateModel());
        }

        /// <summary>
        /// Gets the edge.
        /// </summary>
        protected Edge<TNodeType, TEdgeType> Edge { get; private set; }

        /// <summary>
        /// Invoked when an unhandled <see cref="E:System.Windows.UIElement3D.MouseLeftButtonDown"/> routed event is raised on this element. Implement this method to add class handling for this event.<br/>
        /// Selects the edge.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseButtonEventArgs"/> that contains the event data. The event data reports that the left mouse button was pressed.</param>
        protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            IocHelper.Resolve<IGraphProvider<TNodeType, TEdgeType>>().SelectedElement = this.Edge;
        }

        /// <summary>
        /// Invoked when an unhandled <see cref="E:System.Windows.UIElement3D.MouseRightButtonDown"/> routed event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.<br/>
        /// Marks the edge.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseButtonEventArgs"/> that contains the event data. The event data reports that the right mouse button was pressed.</param>
        protected override void OnMouseRightButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);

            this.Edge.Data.Marked = !this.Edge.Data.Marked;
        }

        /// <summary>
        /// Creates the brush.
        /// </summary>
        /// <returns>The brush.</returns>
        protected Brush CreateBrush()
        {
            var markedColor = this.Edge.Data.Marked ? Colors.Green : Colors.Red;

            var brush = new LinearGradientBrush();
            brush.StartPoint = new Point(0.5, 0);
            brush.EndPoint = new Point(0.5, 1);

            if (this.Edge.EdgeDirection != EdgeDirection.First2Second)
            {
                brush.GradientStops.Add(new GradientStop(markedColor, 0));
                brush.GradientStops.Add(new GradientStop(this.Color, 0.15));
            }
            else
            {
                brush.GradientStops.Add(new GradientStop(this.Color, 0));
            }

            if (this.Edge.EdgeDirection != EdgeDirection.Second2First)
            {
                brush.GradientStops.Add(new GradientStop(this.Color, 0.85));
                brush.GradientStops.Add(new GradientStop(markedColor, 1));
            }

            return brush;
        }
    }
}
