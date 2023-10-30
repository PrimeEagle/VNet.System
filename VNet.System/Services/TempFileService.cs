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

        public TempFile RegisterFile(string filename)
        {
            var setId = GetNextSetId();

            if (!_catalog.ContainsKey(setId))
            {
                _catalog.Add(setId, new List<string>());
            }

            var tempFilename = GetTempFilename(setId, filename);
            _catalog[setId].Add(tempFilename);

            return new TempFile(TempDirectory, setId, tempFilename);
        }

        public TempFile RegisterFile(int setId, string filename)
        {
            if (_catalog.TryGetValue(setId, out var list))
            {
                var tempFilename = GetTempFilename(setId, filename);
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

        private string GetTempFilename(int setId, string filename)
        {
            var tempDir = Path.Combine(TempDirectory, setId.ToString());
            if(!Directory.Exists(tempDir)) Directory.CreateDirectory(tempDir);

            return Path.Combine(tempDir, filename);
        }

        public void CleanSetId(int setId)
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
        }

        public void CleanAll()
        {
            foreach (var key in _catalog.Keys)
            {
                CleanSetId(key);
            }
        }

        public void RemoveTempDirectory()
        {
            if (Directory.Exists(TempDirectory))
                Directory.Delete(TempDirectory, true);
        }
    }
}