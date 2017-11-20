using GameEngine2017.Constants;
using GameEngine2017.Interface;
using System.Collections.Generic;

namespace GameEngine2017.Objects
{
    public class GameEvent
    {
        public EventName Name { get; set; }
        public List<IGameObject> Subscribers { get; set; }
        
        public GameEvent(EventName name)
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
