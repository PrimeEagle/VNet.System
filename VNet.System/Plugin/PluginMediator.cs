using System.Reflection;

namespace VNet.System.Plugin
{
    public class PluginMediator : MarshalByRefObject
    {
        public IEnumerable<IPlugin> InitializeAndFetchPlugins(string assemblyPath)
        {
            var result = new List<IPlugin>();

            var assembly = Assembly.LoadFrom(assemblyPath);
            var pluginTypes = assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IPlugin)));

            foreach (var type in pluginTypes)
            {
                var pluginInstance = (IPlugin)Activator.CreateInstance(type);
                result.Add(pluginInstance);
            }

            return result;
        }
    }
}