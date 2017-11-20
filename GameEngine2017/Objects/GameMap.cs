using GameEngine2017.Constants;

namespace GameEngine2017.Objects
{
    public class GameMap
    {
        public MapName Map { get; set; }

        public GameMap(MapName map)
        {
            Map = map;
        }
    }
}
