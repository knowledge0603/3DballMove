using System.Collections.Generic;
using System.Linq;
using Palmmedia.WpfGraph.UI.Algorithms;

namespace Palmmedia.WpfGraph.UI.ViewModels.Menu
{
    /// <summary>
    /// MenuItem containing <see cref="IAlgorithmMenuItemViewModel">IAlgorithmMenuItemViewModels</see> as child items.
    /// </summary>
    internal class CategoryMenuItemViewModel : MenuItemViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryMenuItemViewModel"/> class.
        /// </summary>
        /// <param name="header">The header.</param>
        /// <param name="algorithms">The <see cref="IGraphAlgorithm">algorithms</see>.</param>
        public CategoryMenuItemViewModel(string header, IEnumerable<IGraphAlgorithm> algorithms)
            : base(null)
        {
            this.Header = header;

            foreach (var algorithm in algorithms)
            {
                this.ChildMenuItems.Add(new IAlgorithmMenuItemViewModel(this, algorithm));
            }
        }
    }
}
