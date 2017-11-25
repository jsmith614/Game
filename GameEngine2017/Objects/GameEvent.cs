using System.Collections.Generic;

namespace GameEngine2017
{
    public class GameEvent
    {
        public string Name { get; set; }
        public List<IGameObject> Subscribers { get; set; }
        
        public GameEvent(string name)
        {
            Name = name;
            Subscribers = new List<IGameObject>();
        }

        public void Fire(IGameObject source)
        {
            Subscribers.ForEach(s => s.HandleEvent(Name, source));
        }
    }
}
