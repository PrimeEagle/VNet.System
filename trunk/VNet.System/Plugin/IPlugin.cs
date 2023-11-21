namespace VNet.System.Plugin
{
    public interface IPlugin
    {
        string ApiVersion { get; }
        string Name { get; }
        string Author { get; }
        string Version { get; }
        DateTime ReleaseDate { get; }
        List<string> InterestedEvents { get; }
        void HandleEvent(string eventName, global::System.EventArgs args);
        void Initialize(IPluginApi api);
    }
}