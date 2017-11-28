namespace GameEngine2017
{
    // TODO: still unsure about making this a singleton to read in config values. 
    // Also wondering if enums like texture name, font name, map, etc shouldn't be lists read in from files also. Idk. Figure this out before moving forward.
    public class Config
    {
        private Config() { }
        private static Config _instance;
        public static Config Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Config();
                }
                return _instance;
            }
        }
        
        public float DefaultMessageLifeTime = 2.00f;
        public float Gravity = 25.0f;
        public string MapDirectory = @"Maps\";

        public void Load()
        {

        }

        public void Unload()
        {

        }
    }
}
