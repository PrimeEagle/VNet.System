namespace VNet.System.Services;

public class TempFileServiceOptions
{
    public string TempDirectory { get; set; }

    public TempFileServiceOptions(string tempDirectory)
    {
        TempDirectory = tempDirectory;
    }
}