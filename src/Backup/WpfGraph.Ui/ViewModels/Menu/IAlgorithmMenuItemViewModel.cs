using System;
using System.Globalization;
using Palmmedia.WpfGraph.Common;
using Palmmedia.WpfGraph.UI.Algorithms;
using Palmmedia.WpfGraph.UI.Interaction;

namespace Palmmedia.WpfGraph.UI.ViewModels.Menu
{
    /// <summary>
    /// MenuItem showing an <see cref="IGraphAlgorithm"/> for execution.
    /// </summary>
    internal class IAlgorithmMenuItemViewModel : MenuItemViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IAlgorithmMenuItemViewModel"/> class.
        /// </summary>
        /// <param name="parentViewModel">The parent view model.</param>
        /// <param name="algorithm">The <see cref="IGraphAlgorithm"/>.</param>
        public IAlgorithmMenuItemViewModel(MenuItemViewModel parentViewModel, IGraphAlgorithm algorithm)
            : base(parentViewModel)
        {
            this.Header = algorithm.Name;
            this.Command = new RelayCommand(param => Execute(algorithm));
        }

        /// <summary>
        /// Executes the given <see cref="IGraphAlgorithm"/>.
        /// </summary>
        /// <param name="algorithm">The <see cref="IGraphAlgorithm"/> to execute.</param>
        private static void Execute(IGraphAlgorithm algorithm)
        {
            var graph = IocHelper.Resolve<IGraphProvider<NodeData, EdgeData>>().Graph;
            try
            {
                algorithm.Execute(graph);
            }
            catch (InvalidOperationException ex)
            {
                var messageHandler = IocHelper.Resolve<IMessageHandler>();
                messageHandler.ShowError(string.Format(CultureInfo.CurrentCulture, Palmmedia.WpfGraph.UI.Properties.Resources.InvalidGraph, ex.Message));
            }            
        }
    }
}
