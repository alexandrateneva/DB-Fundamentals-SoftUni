namespace _6.Online_Radio_Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            var songs = new List<Song>();
            var num = int.Parse(Console.ReadLine());
            for (int i = 0; i < num; i++)
            {
                try
                {
                    var songInfo = Console.ReadLine().Split(';');
                    var duration = songInfo[2].Split(':').Select(int.Parse).ToArray();

                    var song = new Song(songInfo[0], songInfo[1], duration[0], duration[1]);
                    songs.Add(song);
                    Console.WriteLine("Song added.");
                }
                catch (InvalidSongException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Invalid song length.");
                }
            }

            var totalDuration = 0;
            foreach (var song in songs)
            {
                totalDuration += song.Minutes * 60 + song.Seconds;
            }
            var hours = totalDuration / 3600;
            totalDuration -= hours * 3600;
            var minutes = totalDuration / 60;
            totalDuration -= minutes * 60;
            var seconds = totalDuration;

            Console.WriteLine($"Songs added: {songs.Count}");
            Console.WriteLine($"Playlist length: {hours}h {minutes}m {seconds}s");
        }
    }
}
