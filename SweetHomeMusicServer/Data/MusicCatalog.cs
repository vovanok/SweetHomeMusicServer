namespace SweetHomeMusicServer.Data
{
    [Serializable]
    public class MusicCatalog
    {
        public MusicBand[] Bands { get; private set; }
        
        public MusicCatalog(MusicBand[] bands)
        {
            Bands = bands;
        }
    }
}