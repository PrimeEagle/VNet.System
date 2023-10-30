namespace VNet.System.Services;

public interface ITempFileService
{
    TempFile RegisterFile(string filename);
    TempFile RegisterFile(int setId, string filename);
    void CleanSetId(int setId);
    Task CleanSetIdAsync(int setId);
    void CleanAll();
    Task CleanAllAsync();
    void RemoveTempDirectory();
    Task RemoveTempDirectoryAsync();
    string TempDirectory { get; init; }
}