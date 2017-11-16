using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Palmmedia.WpfGraph.Common
{
    /// <summary>
    /// Helper class for dependency injection.
    /// Provides methods to resolve object instances.
    /// Wrappes the Unity Container. 
    /// Other containers can be easily used by changing the implementation of this class.
    /// </summary>
    public static class IocHelper
    {
        /// <summary>
        /// Instance of the <see cref="IUnityContainer"/>.
        /// </summary>
        private static IUnityContainer container = new UnityContainer();

        /// <summary>
        /// Initializes static members of the <see cref="IocHelper"/> class.
        /// </summary>
        static IocHelper()
        {
            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            section.Containers["defaultContainer"].Configure(container);
        }

        /// <summary>
        /// Resolves instance of the given interface.
        /// </summary>
        /// <typeparam name="T">The interface to resolve.</typeparam>
        /// <returns>The instance of the given interface.</returns>
        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        /// <summary>
        /// Resolves instance of the given type.
        /// </summary>
        /// <param name="type">The type to resolve.</param>
        /// <returns>The instance of the given type.</returns>
        public static object Resolve(System.Type type)
        {
            return container.Resolve(type);
        }
    }
}
