using NLua;
using System.Reflection;
using VNet.System.Events;

namespace VNet.System.Plugin
{
    public class PluginLoader
    {
        private readonly string _cSharpPluginsFolder;
        private readonly string _luaPluginsFolder;


        public PluginLoader(string cSharpPluginsFolder, string luaPluginsFolder)
        {
            _cSharpPluginsFolder = cSharpPluginsFolder;
            _luaPluginsFolder = luaPluginsFolder;
        }

        public void RegisterPlugins(IEventAggregator eventAggregator)
        {
            foreach (var plugin in LoadAllPlugins())
            {
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

            foreach (var filename in Directory.GetFiles(_luaPluginsFolder, "*.lua"))
            {
                var luaScript = File.ReadAllText(filename);
                using Lua lua = new();
                lua.DoString(luaScript);
                result.Add(new LuaPluginWrapper(luaScript, lua));
            }

            return result;
        }
    }
}