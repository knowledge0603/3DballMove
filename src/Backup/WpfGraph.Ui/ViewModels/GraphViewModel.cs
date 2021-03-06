﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Media3D;
using Palmmedia.WpfGraph.Core;
using Palmmedia.WpfGraph.UI.Elements3D;
using Palmmedia.WpfGraph.UI.Interaction;

namespace Palmmedia.WpfGraph.UI.ViewModels
{
    /// <summary>
    /// The viewmodel of a graph.
    /// </summary>
    /// <typeparam name="TNodeType">The type of the data attached to a <see cref="Palmmedia.WpfGraph.Core.Node&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
    /// <typeparam name="TEdgeType">The type of the data attached to an <see cref="Palmmedia.WpfGraph.Core.Edge&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
    internal class GraphViewModel<TNodeType, TEdgeType> : ViewModelBase, IGraphProvider<TNodeType, TEdgeType>
        where TNodeType : NodeData
        where TEdgeType : EdgeData
    {
        /// <summary>
        /// Logger instance.
        /// </summary>
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(GraphViewModel<TNodeType, TEdgeType>));

        /// <summary>
        /// The camera position.
        /// </summary>
        private Point3D cameraPosition = new Point3D(0, 0, 40);        

        /// <summary>
        /// The graph.
        /// </summary>
        private IGraph<TNodeType, TEdgeType> graph;

        /// <summary>
        /// The currently selected node or edge.
        /// </summary>
        private GraphElement<TNodeType, TEdgeType> selectedElement;

        /// <summary>
        /// Dictionaray containing the <see cref="UIElement3D"/> per edge.
        /// </summary>
        private Dictionary<Edge<TNodeType, TEdgeType>, UIElement3D> edge2VisualDictionary;

        /// <summary>
        /// Dictionaray containing the <see cref="UIElement3D"/> per node.
        /// </summary>
        private Dictionary<Node<TNodeType, TEdgeType>, NodeUIElement<TNodeType, TEdgeType>> node2VisualDictionary;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphViewModel&lt;TNodeType, TEdgeType&gt;"/> class.
        /// </summary>
        /// <param name="graph">The graph.</param>
        public GraphViewModel(IGraph<TNodeType, TEdgeType> graph)
        {
            if (graph == null)
            {
                throw new ArgumentNullException("graph");
            }

            this.node2VisualDictionary = new Dictionary<Node<TNodeType, TEdgeType>, NodeUIElement<TNodeType, TEdgeType>>();
            this.edge2VisualDictionary = new Dictionary<Edge<TNodeType, TEdgeType>, UIElement3D>();
            this.Graph = graph;
        }

        /// <summary>
        /// Gets or sets the container displaying the graph elements.
        /// </summary>
        /// <value></value>
        public ContainerUIElement3D Container { get; set; }

        /// <summary>
        /// Gets or sets the zoom of the camera.
        /// </summary>
        public double Zoom
        {
            get
            {
                return this.CameraPosition.Z;
            }

            set
            {
                var cameraPosition = this.CameraPosition;
                cameraPosition.Z = value;
                this.CameraPosition = cameraPosition;
                this.OnPropertyChanged("Zoom");
            }
        }

        /// <summary>
        /// Gets the camera position.
        /// </summary>
        public Point3D CameraPosition
        {
            get
            {
                return this.cameraPosition;
            }

            private set
            {
                this.cameraPosition = value;
                this.OnPropertyChanged("CameraPosition");
            }
        }

        /// <summary>
        /// Gets or sets the graph.
        /// </summary>
        public IGraph<TNodeType, TEdgeType> Graph
        {
            get
            {
                return this.graph;
            }

            set
            {
                if (this.graph != null)
                {
                    this.graph.EdgeAdded -= this.Graph_EdgeAdded;
                    this.graph.NodeAdded -= this.Graph_NodeAdded;
                    this.graph.EdgeRemoved -= this.Graph_EdgeRemoved;
                    this.graph.NodeRemoved -= this.Graph_NodeRemoved;
                }

                this.SelectedElement = null;

                this.graph = value;

                var container = this.Container;
                if (container != null)
                {
                    this.Container.Children.Clear();
                }

                this.edge2VisualDictionary.Clear();
                this.node2VisualDictionary.Clear();

                if (value != null)
                {
                    foreach (var node in this.graph.Nodes)
                    {
                        var visual = node.CreateVisual();
                        this.node2VisualDictionary.Add(node, visual);
                        this.Container.Children.Add(visual);
                    }

                    foreach (var edge in this.graph.Edges)
                    {
                        var visual = edge.CreateVisual(this.node2VisualDictionary[edge.FirstNode].TranslateTransform, this.node2VisualDictionary[edge.SecondNode].TranslateTransform);
                        this.edge2VisualDictionary.Add(edge, visual);
                        this.Container.Children.Add(visual);
                    }

                    this.graph.EdgeAdded += new EventHandler<EdgeEventArgs<TNodeType, TEdgeType>>(this.Graph_EdgeAdded);
                    this.graph.NodeAdded += new EventHandler<NodeEventArgs<TNodeType, TEdgeType>>(this.Graph_NodeAdded);
                    this.graph.EdgeRemoved += new EventHandler<EdgeEventArgs<TNodeType, TEdgeType>>(this.Graph_EdgeRemoved);
                    this.graph.NodeRemoved += new EventHandler<NodeEventArgs<TNodeType, TEdgeType>>(this.Graph_NodeRemoved);
                }

                this.OnPropertyChanged("Graph");
            }
        }

        /// <summary>
        /// Gets or sets the currently selected node or edge.
        /// </summary>
        /// <value></value>
        public GraphElement<TNodeType, TEdgeType> SelectedElement
        {
            get
            {
                return this.selectedElement;
            }

            set
            {
                logger.Debug("Selected new element: " + value);
                this.selectedElement = value;
                this.OnPropertyChanged("SelectedElement");
                this.OnPropertyChanged("SelectedElements");
            }
        }

        /// <summary>
        /// Gets the selected elements.<br/>
        /// Used as a workaround together with <see cref="Palmmedia.WpfGraph.UI.Resources.GraphElementTemplateSelector"/>, since generics are not supported in XAML.
        /// </summary>
        public IEnumerable<GraphElement<TNodeType, TEdgeType>> SelectedElements
        {
            get
            {
                if (this.selectedElement == null)
                {
                    return null;
                }
                else
                {
                    return new GraphElement<TNodeType, TEdgeType>[] { this.selectedElement };
                }
            }
        }

        /// <summary>
        /// Handles the EdgeAdded event of the Graph control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Palmmedia.WpfGraph.Core.EdgeEventArgs&lt;TNodeType, TEdgeType&gt;"/> instance containing the event data.</param>
        private void Graph_EdgeAdded(object sender, EdgeEventArgs<TNodeType, TEdgeType> e)
        {
            logger.Debug("Added edge: " + e.Edge);

            var visual = e.Edge.CreateVisual(this.node2VisualDictionary[e.Edge.FirstNode].TranslateTransform, this.node2VisualDictionary[e.Edge.SecondNode].TranslateTransform);
            this.edge2VisualDictionary.Add(e.Edge, visual);
            this.Container.Children.Add(visual);
        }

        /// <summary>
        /// Handles the NodeAdded event of the Graph control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Palmmedia.WpfGraph.Core.NodeEventArgs&lt;TNodeType, TEdgeType&gt;"/> instance containing the event data.</param>
        private void Graph_NodeAdded(object sender, NodeEventArgs<TNodeType, TEdgeType> e)
        {
            logger.Debug("Added node: " + e.Node);

            var visual = e.Node.CreateVisual();
            this.node2VisualDictionary.Add(e.Node, visual);
            this.Container.Children.Add(visual);
        }

        /// <summary>
        /// Handles the EdgeRemoved event of the Graph control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Palmmedia.WpfGraph.Core.EdgeEventArgs&lt;TNodeType, TEdgeType&gt;"/> instance containing the event data.</param>
        private void Graph_EdgeRemoved(object sender, EdgeEventArgs<TNodeType, TEdgeType> e)
        {
            logger.Debug("Removed edge: " + e.Edge);

            var visual = this.edge2VisualDictionary[e.Edge];
            this.Container.Children.Remove(visual);
            this.edge2VisualDictionary.Remove(e.Edge);

            if (e.Edge == this.SelectedElement)
            {
                this.SelectedElement = null;
            }
        }

        /// <summary>
        /// Handles the NodeRemoved event of the Graph control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Palmmedia.WpfGraph.Core.NodeEventArgs&lt;TNodeType, TEdgeType&gt;"/> instance containing the event data.</param>
        private void Graph_NodeRemoved(object sender, NodeEventArgs<TNodeType, TEdgeType> e)
        {
            logger.Debug("Removed node: " + e.Node);
            
            var visual = this.node2VisualDictionary[e.Node];
            this.Container.Children.Remove(visual);
            this.node2VisualDictionary.Remove(e.Node);

            if (e.Node == this.SelectedElement)
            {
                this.SelectedElement = null;
            }
        }
    }
}