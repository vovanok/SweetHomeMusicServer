using SweetHomeMusicServer.Data;

namespace SweetHomeMusicServer.Services;

public class MusicService
{
    public MusicCatalog GetCatalog(string directoryPath)
    {
        if (string.IsNullOrEmpty(directoryPath) || !Directory.Exists(directoryPath))
            return new MusicCatalog(Array.Empty<MusicBand>());
        
        string[] bandsDirectories = Directory.GetDirectories(directoryPath);

        MusicBand[] bands = bandsDirectories
            .Select(GetBandByDirectory)
            .ToArray();

        return new MusicCatalog(bands);
    }

    public MusicTrack? GetTrack(MusicCatalog musicCatalog, string bandName, string trackName)
    {
        MusicBand? targetBand = musicCatalog.Bands.FirstOrDefault(band => band.Name == bandName);
        return targetBand?.Tracks.FirstOrDefault(track => track.Name == trackName);
    }

    private MusicBand GetBandByDirectory(string bandDirectoryPath)
    {
        DirectoryInfo bandDirectory = new DirectoryInfo(bandDirectoryPath);
            
        string[] tracksFileNames = Directory.GetFiles(
            bandDirectory.FullName, "*", SearchOption.AllDirectories);
            
        MusicTrack[] tracks = tracksFileNames
            .Select(trackFilename => new MusicTrack(Path.GetFileName(trackFilename), trackFilename))
            .ToArray();

        return new MusicBand(bandDirectory.Name, tracks);
    }
}