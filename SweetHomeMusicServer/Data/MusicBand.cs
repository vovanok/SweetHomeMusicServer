namespace SweetHomeMusicServer.Data
{
    [Serializable]
    public class MusicBand
    {
        public string Name { get; private set; }
        public MusicTrack[] Tracks { get; private set; }

        public MusicBand(string name, MusicTrack[] tracks)
        {
            Name = name;
            Tracks = tracks;
        }
    }
}