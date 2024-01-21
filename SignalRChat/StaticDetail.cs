﻿namespace SignalRChat
{
    public static class StaticDetail
    {
        static StaticDetail()
        {
            DealthyHallowRace = new Dictionary<string, int>
            {
                { Cloak, 0 },
                { Stone, 0 },
                { Wand, 0 }
            };
        }

        public const string Wand = "wand";
        public const string Stone = "stone";
        public const string Cloak = "cloak";

        public static Dictionary<string, int> DealthyHallowRace;
    }
}
