using System;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using Palmmedia.WpfGraph.Common;
using Palmmedia.WpfGraph.Core;
using Palmmedia.WpfGraph.UI.Interaction;
using Palmmedia.WpfGraph.UI.IO;

namespace Palmmedia.WpfGraph.UI.ViewModels
{
    /// <summary>
    /// The viewmodel for the main window.
    /// </summary>
    internal class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// Logger instance.
        /// </summary>
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(MainWindowViewModel));

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="graph">Graph to show on startup.</param>
        public MainWindowViewModel(ContainerUIElement3D container, IGraph<NodeData, EdgeData> graph)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.GraphViewModel = IocHelper.Resolve<IGraphProvider<NodeData, EdgeData>>();
            this.GraphViewModel.Container = container;

            if (graph != null)
            {
                this.GraphViewModel.Graph = graph;
            }

            this.FileSelector = IocHelper.Resolve<IFileSelector>();
            this.MessageHandler = IocHelper.Resolve<IMessageHandler>();

            this.LoadGraphCommand = new RelayCommand(param => this.LoadGraph());
            this.SaveGraphCommand = new RelayCommand(param => this.SaveGraph());
            this.ExitCommand = new RelayCommand(param => WpfGraph.Current.Shutdown());
        }

        /// <summary>
        /// Gets or sets the file selector.
        /// </summary>
        public IFileSelector FileSelector { get; set; }

        /// <summary>
        /// Gets or sets the message handler.
        /// </summary>
        public IMessageHandler MessageHandler { get; set; }

        /// <summary>
        /// Gets the load graph command.
        /// </summary>
        public ICommand LoadGraphCommand { get; private set; }

        /// <summary>
        /// Gets the save graph command.
        /// </summary>
        public ICommand SaveGraphCommand { get; private set; }

        /// <summary>
        /// Gets the exit command.
        /// </summary>
        public ICommand ExitCommand { get; private set; }

        /// <summary>
        /// Gets the graph view model.
        /// </summary>
        public IGraphProvider<NodeData, EdgeData> GraphViewModel { get; private set; }

        /// <summary>
        /// Loads a graph from a file.
        /// </summary>
        private void LoadGraph()
        {
            string path = this.FileSelector.GetFileNameForOpening();

            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    var graph = GraphSerializer.ReadFromXmlFile(path);
                    this.GraphViewModel.Graph = graph;
                }
                catch (GraphSerializationException ex)
                {
                    logger.Error("Loading graph form path '" + path + "' failed.", ex);
                    this.MessageHandler.ShowError(ex.Message);
                }
            }
        }

        /// <summary>
        /// Saves a graph to a file.
        /// </summary>
        private void SaveGraph()
        {
            string path = this.FileSelector.GetFileNameForSaving();

            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    var graph = this.GraphViewModel.Graph;
                    GraphSerializer.SaveAsXmlFile(graph, path);
                }
                catch (GraphSerializationException ex)
                {
                    logger.Error("Saving graph to path '" + path + "' failed.", ex);
                    this.MessageHandler.ShowError(ex.Message);
                }
            }
        }
    }
}
