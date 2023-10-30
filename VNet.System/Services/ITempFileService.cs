namespace VNet.System.Services;

public interface ITempFileService
{
    public TempFile RegisterFile(string filename);
    public TempFile RegisterFile(int setId, string filename);
    public void CleanSetId(int setId);
    public void RemoveTempDirectory();
    public string TempDirectory { get; init; }
}