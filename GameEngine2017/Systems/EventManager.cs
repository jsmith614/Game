using System.Collections.Generic;
using System.Linq;

namespace GameEngine2017
{
    public class EventManager
    {
        private List<GameEvent> _events;
        private static EventManager _instance;
        public static EventManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EventManager();
                }
                return _instance;
            }
        }

        private EventManager()
        {
            _events = new List<GameEvent>();
        }
        
        public void Load(List<string> events)
        {
            ClearEvents();
            foreach (var eventName in events)
            AddEvent(eventName);

            //foreach (var eventName in Enum.GetValues(typeof(EventName)).Cast<EventName>())
            //{
            //    AddEvent(eventName);
            //}
        }

        public void Unload()
        {
            ClearEvents();
        }

        public void AddEvent(string eventName)
        {
            if (_events.Any(e => e.Name == eventName) == false)
            {
                _events.Add(new GameEvent(eventName));
            }
            else
            {
                MessageHandler.Instance.AddMessage("AddEvent: Event already exists.");
            }
        }

        public void Subscribe(string eventName, IGameObject gameObject)
        {
            var e = _events.SingleOrDefault(ev => ev.Name == eventName);
            if(e != null)
            {
                if(e.Subscribers.Contains(gameObject) == false)
                {
                    e.Subscribers.Add(gameObject);
                }
                else
                {
                    MessageHandler.Instance.AddMessage("Subscribe: Already subscribed.");
                }
            }
            else
            {
                MessageHandler.Instance.AddError("Subscribe: Event not found.");
            }
        }

        public void Unsubscribe(string eventName, IGameObject gameObject)
        {
            var e = _events.SingleOrDefault(ev => ev.Name == eventName);
            if (e != null)
            {
                if (e.Subscribers.Contains(gameObject))
                {
                    e.Subscribers.Remove(gameObject);
                }
                else
                {
                    MessageHandler.Instance.AddMessage("Unsubscribe: Not subscribed.");
                }
            }
            else
            {
                MessageHandler.Instance.AddError("Unsubscribe: Event not found.");
            }
        }

        public void UnsubscribeAll(IGameObject gameObject)
        {
            _events.ForEach(e => e.Subscribers.Remove(gameObject));
        }

        public void FireEvent(IGameObject source, string eventName)
        {
            var e = _events.SingleOrDefault(ev => ev.Name == eventName);
            if(e != null)
            {
                e.Fire(source);
            }
            else
            {
                MessageHandler.Instance.AddError("FireEvent: Event not found.");
            }
        }

        public void ClearEvents()
        {
            _events.Clear();
        }
    }

}
