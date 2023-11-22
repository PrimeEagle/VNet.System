namespace VNet.System.Plugin
{
    public interface IPluginApi
    {
        public string ApiVersion { get; init; }
        public string[] CompatibleApiVersions { get; init; }
    }
}