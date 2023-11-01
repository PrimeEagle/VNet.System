namespace VNet.System.Services
{
    public class TempFile
    {
        public string FileName { get; init; }
        public Guid SetId { get; init; }
        public string SetDirectory { get; init; }



        public TempFile(string tempDirectory, Guid setId, string fileName)
        {
            FileName = fileName;
            SetId = setId;
            SetDirectory = Path.Combine(tempDirectory, setId.ToString());
        }
    }
}