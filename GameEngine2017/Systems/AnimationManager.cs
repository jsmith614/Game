using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine2017.Systems
{
    // TODO: something here. Probably getting rid of this. Will do stuff in texture manager instead. 
    public class AnimationManager
    {
        private static AnimationManager _instance;
        public static AnimationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AnimationManager();
                }
                return _instance;
            }
        }
    }
}
