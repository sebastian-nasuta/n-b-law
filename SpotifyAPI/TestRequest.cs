using SpotifyAPI.Web;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyAPI
{
    public class TestRequest
    {
        private static SpotifyClient _spotifyClient;

        public static void Run()
        {
            var config = SpotifyClientConfig
                .CreateDefault()
                .WithAuthenticator(new ClientCredentialsAuthenticator("1b5ad373ad7449228cf8a7c26ae969af", "1563b6253ba04c55bd6e2092adee5fb2"));
            
            _spotifyClient = new SpotifyClient(config);

            var counts = new int[10];

            AddCounts(counts, "6RxsseYlyxrkJOOmAOLQTM").Wait(); // Rolling stone 500
            AddCounts(counts, "0xIhTsBJm7fpW2Q4cicDrX").Wait(); // Top Wszechczasów
            AddCounts(counts, "3RFC2ZoAqj2utRUNQxrTzb").Wait(); // Polski top Wszechczasów
            AddCounts(counts, "0WEvguGJN7UslUZN5Wpm8B").Wait(); // Don's Tunes
            AddCounts(counts, "4Op1LnZYmLtnrR8ffxHyIv").Wait(); // Best bass guitar riffs
            AddCounts(counts, "37i9dQZF1DWSf2RDTDayIx").Wait(); // Happy Beats
            AddCounts(counts, "37i9dQZF1DWWEJlAGA9gs0").Wait(); // Classical Essentials
            AddCounts(counts, "37i9dQZF1DWWn6teJIIcfG").Wait(); // Creative Focus
            AddCounts(counts, "37i9dQZF1DX0BcQWzuB7ZO").Wait(); // Dance Hits
            AddCounts(counts, "37i9dQZF1DX2TRYkJECvfC").Wait(); // Deep House Relax
            AddCounts(counts, "37i9dQZF1DWZeKCadgRdKQ").Wait(); // Deep Focus
            AddCounts(counts, "37i9dQZF1DWWQRwui0ExPn").Wait(); // Lo-Fi Beats
            AddCounts(counts, "37i9dQZF1DX4sWSpwq3LiO").Wait(); // Peaceful Piano
            AddCounts(counts, "37i9dQZF1DWV7EzJMK2FUI").Wait(); // Jazz in the background
            AddCounts(counts, "2YMRuaTDBC5X4jsfUBlD3i").Wait(); //Northern Echoes

            int number = 1;

            counts.Skip(1).ToList().ForEach(x => {
                //Console.WriteLine($"{number++} - {x}");
                Console.WriteLine(x);
            });
        }

        private static async Task AddCounts(int[] counts, string playlistId)
        {
            var page = await _spotifyClient.Playlists.Get(playlistId);

            var allTracks = await _spotifyClient.PaginateAll(page.Tracks);

            var numbers = allTracks.Select(x => (x.Track as FullTrack).Name.Length.ToString()).ToList();

            foreach (var number in numbers)
            {
                int firstDigit = int.Parse(number[0].ToString());
                counts[firstDigit]++;
            }
        }
    }
}
