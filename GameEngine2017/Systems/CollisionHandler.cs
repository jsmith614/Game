namespace GameEngine2017
{
    public class CollisionHandler
    {
        private static CollisionHandler _instance;
        public static CollisionHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CollisionHandler();
                }
                return _instance;
            }
        }

        private CollisionHandler()
        {

        }

        public void Load()
        {

        }

        public void Unload()
        {

        }
    }
}
