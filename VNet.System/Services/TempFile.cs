namespace VNet.System.Services
{
    public class TempFile
    {
        public string Filename { get; init; }
        public int SetId { get; init; }
        public string SetDirectory { get; init; }



        public TempFile(string tempDirectory, int setId, string filename)
        {
            Filename = filename;
            SetId = setId;
            SetDirectory = Path.Combine(tempDirectory, setId.ToString());
        }
    }
}