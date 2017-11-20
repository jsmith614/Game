using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameEngine2017.Constants
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
        
        public float DefaultMessageLifeTime = 2000.00f;
        public float Gravity = 25.0f;

        public void Load()
        {

        }

        public void Unload()
        {

        }
    }
}
