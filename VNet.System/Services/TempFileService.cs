using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

// ReSharper disable MemberCanBePrivate.Global


namespace VNet.System.Services
{
    public class TempFileService : ITempFileService
    {
        private readonly Dictionary<int, List<string>> _catalog;
        private readonly ILogger<TempFileService> _loggerService;
        public string TempDirectory { get; init; }




        public TempFileService(IOptions<TempFileServiceOptions> options, ILogger<TempFileService> loggerService)
        {
            _catalog = new Dictionary<int, List<string>>();
            _loggerService = loggerService;
            TempDirectory = options.Value.TempDirectory;

            if (!Directory.Exists(TempDirectory))
            {
                Directory.CreateDirectory(TempDirectory);
            }
        }

        private int GetNextSetId()
        {
            var id = 1;
            while (_catalog.ContainsKey(id))
            {
                id++;
            }

            return id;
        }

        public TempFile RegisterFile(string fileName)
        {
            var setId = GetNextSetId();

            if (!_catalog.ContainsKey(setId))
            {
                _catalog.Add(setId, new List<string>());
            }

            var tempFilename = GetTempFilename(setId, fileName);
            _catalog[setId].Add(tempFilename);

            return new TempFile(TempDirectory, setId, tempFilename);
        }

        public TempFile RegisterFile(int setId, string fileName)
        {
            if (_catalog.TryGetValue(setId, out var list))
            {
                var tempFilename = GetTempFilename(setId, fileName);
                list.Add(tempFilename);

                return new TempFile(TempDirectory, setId, tempFilename);
            }
            else
            {
                var ex = new ArgumentException("The specified ID does not exist in the catalog.", nameof(setId));
                _loggerService.LogError(ex, "The specified ID does not exist in the catalog.");
                throw ex;
            }
        }

        private string GetTempFilename(int setId, string fileName)
        {
            var tempDir = Path.Combine(TempDirectory, setId.ToString());
            if (!Directory.Exists(tempDir)) Directory.CreateDirectory(tempDir);

            return Path.Combine(tempDir, fileName);
        }

        public void CleanSetId(int setId)
        {
            CleanSetIdAsync(setId).GetAwaiter().GetResult();
        }

        public Task CleanSetIdAsync(int setId)
        {
            return Task.Run(() =>
            {
                if (_catalog.TryGetValue(setId, out _))
                {
                    Directory.Delete(Path.Combine(TempDirectory, setId.ToString()), true);
                    _catalog.Remove(setId);
                }
                else
                {
                    var ex = new ArgumentException("The specified ID does not exist in the catalog.", nameof(setId));
                    _loggerService.LogError(ex, "The specified ID does not exist in the catalog.");
                    throw ex;
                }
            });
        }

        public void CleanAll()
        {
            CleanAllAsync().GetAwaiter().GetResult();
        }

        public async Task CleanAllAsync()
        {
            var tasks = _catalog.Keys.Select(CleanSetIdAsync).ToList();
            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
        
        public void RemoveTempDirectory()
        {
            RemoveTempDirectoryAsync().GetAwaiter().GetResult();
        }

        public Task RemoveTempDirectoryAsync()
        {
            return Task.Run(() =>
            {
                if (Directory.Exists(TempDirectory))
                    Directory.Delete(TempDirectory, true);
            });
        }
    }
}