using NLua;
using System.Reflection;
using VNet.System.Events;

namespace VNet.System.Plugin
{
    public class PluginLoader
    {
        private readonly string _cSharpPluginsFolder;
        private readonly string _luaPluginsFolder;
        private readonly string _apiVersion;
        private readonly string[] _compatibleApiVersions;
        private readonly IPluginApi _api;

        public PluginLoader(string cSharpPluginsFolder, string luaPluginsFolder, IPluginApi api)
        {
            _cSharpPluginsFolder = cSharpPluginsFolder;
            _luaPluginsFolder = luaPluginsFolder;
            _api = api;
            _apiVersion = api.ApiVersion;
            _compatibleApiVersions = api.CompatibleApiVersions;
        }

        public void RegisterPlugins(IEventAggregator eventAggregator)
        {
            foreach (var plugin in LoadAllPlugins())
            {
                if (!IsApiVersionCompatible(plugin.ApiVersion))
                {
                    continue;
                }

                plugin.Initialize(_api);

                foreach (var eventName in plugin.InterestedEvents)
                {
                    var eventType = Type.GetType(eventName);
                    if (eventType == null) continue;

                    Action<object> handler = args => plugin.HandleEvent(eventName, (EventArgs)args);
                    var subscribeMethod = typeof(IEventAggregator).GetMethod("Subscribe").MakeGenericMethod(eventType);
                    subscribeMethod.Invoke(eventAggregator, new object[] { handler });
                }
            }
        }

        private List<IPlugin> LoadAllPlugins()
        {
            return LoadCSharpPlugins().Concat(LoadLuaPlugin()).ToList();
        }

        private IEnumerable<IPlugin> LoadCSharpPlugins()
        {
            var result = new List<IPlugin>();

            if (string.IsNullOrEmpty(_cSharpPluginsFolder)) return result;

            result = (from file in Directory.GetFiles(_cSharpPluginsFolder, "*.dll")
                select Assembly.LoadFile(file)
                into assembly
                from type in assembly.GetTypes()
                where type.GetInterfaces().Contains(typeof(IPlugin))
                select Activator.CreateInstance(type) as IPlugin).ToList();

            return result;
        }

        private IEnumerable<IPlugin> LoadLuaPlugin()
        {
            var result = new List<IPlugin>();

            if (string.IsNullOrEmpty(_luaPluginsFolder)) return result;

            foreach (var fileName in Directory.GetFiles(_luaPluginsFolder, "*.lua"))
            {
                var luaScript = File.ReadAllText(fileName);
                using Lua lua = new();
                lua.DoString(luaScript);
                result.Add(new LuaPluginWrapper(luaScript, lua));
            }

            return result;
        }

        private bool IsApiVersionCompatible(string pluginApiVersion)
        {
            return pluginApiVersion == _apiVersion || _compatibleApiVersions.Contains(pluginApiVersion);
        }
    }
}