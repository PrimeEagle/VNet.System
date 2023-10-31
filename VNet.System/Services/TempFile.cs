namespace VNet.System.Services
{
    public class TempFile
    {
        public string FileName { get; init; }
        public int SetId { get; init; }
        public string SetDirectory { get; init; }



        public TempFile(string tempDirectory, int setId, string fileName)
        {
            FileName = fileName;
            SetId = setId;
            SetDirectory = Path.Combine(tempDirectory, setId.ToString());
        }
    }
}