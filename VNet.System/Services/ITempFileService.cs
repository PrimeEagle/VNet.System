namespace VNet.System.Services;

public interface ITempFileService
{
    TempFile RegisterFile(string fileName);
    TempFile RegisterFile(Guid setId, string fileName);
    void CleanSetId(Guid setId);
    Task CleanSetIdAsync(Guid setId);
    void CleanAll();
    Task CleanAllAsync();
    void RemoveTempDirectory();
    Task RemoveTempDirectoryAsync();
    string TempDirectory { get; init; }
}