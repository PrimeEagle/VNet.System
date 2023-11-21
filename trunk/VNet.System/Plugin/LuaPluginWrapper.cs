using NLua;
using VNet.System.Events;

namespace VNet.System.Plugin
{
    public class LuaPluginWrapper : IPlugin
    {
        private readonly Lua _luaEnvironment;

        public LuaPluginWrapper(string luaScript, Lua luaEnvironment)
        {
            _luaEnvironment = luaEnvironment;
            _luaEnvironment.DoString(luaScript);

            Name = _luaEnvironment["Name"] as string ?? string.Empty;
            Author = _luaEnvironment["Author"] as string ?? string.Empty;
            Version = _luaEnvironment["Version"] as string ?? string.Empty;

            if (_luaEnvironment["ReleaseDate"] is double timestamp)
            {
                ReleaseDate = DateTime.FromOADate(timestamp);
            }

            var luaInterestedEvents = _luaEnvironment["InterestedEvents"] as LuaTable;
            InterestedEvents = ConvertLuaTableToList(luaInterestedEvents);
        }

        public string ApiVersion { get; }
        public string Name { get; }
        public string Author { get; }
        public string Version { get; }
        public DateTime ReleaseDate { get; }
        public List<string> InterestedEvents { get; }

        public void HandleEvent(string eventName, global::System.EventArgs args)
        {
            if (_luaEnvironment.GetFunction("HandleEvent") is LuaFunction function)
            {
                function.Call(eventName, args);
            }
        }

        public void Initialize(IPluginApi api)
        {
            if (_luaEnvironment.GetFunction("Initialize") is LuaFunction function)
            {
                function.Call(api);
            }
        }

        private List<string> ConvertLuaTableToList(LuaTable luaTable)
        {
            var list = new List<string>();

            if (luaTable == null) return list;
            foreach (var value in luaTable.Values)
            {
                if (value is string strValue)
                {
                    list.Add(strValue);
                }
            }

            return list;
        }
    }
}