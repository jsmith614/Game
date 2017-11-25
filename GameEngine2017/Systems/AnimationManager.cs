
namespace GameEngine2017
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
