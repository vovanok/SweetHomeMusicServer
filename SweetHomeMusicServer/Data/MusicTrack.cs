namespace SweetHomeMusicServer.Data
{
    [Serializable]
    public class MusicTrack
    {
        public string Name { get; private set; }
        public string FilePath { get; private set; }
        
        public MusicTrack(string name, string filePath)
        {
            Name = name;
            FilePath = filePath;
        }
    }
}