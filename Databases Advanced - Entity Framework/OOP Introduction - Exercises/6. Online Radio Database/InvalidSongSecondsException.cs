﻿namespace _6.Online_Radio_Database
{
    public class InvalidSongSecondsException : InvalidSongLengthException
    {
        public override string Message => "Song seconds should be between 0 and 59.";
    }
}
