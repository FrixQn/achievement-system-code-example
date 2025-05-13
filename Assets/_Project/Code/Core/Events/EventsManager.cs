using System;

namespace DemoProject.Core.Events
{
    public class EventsManager : IEventsManager
    {
        private event Action<EventData> _publish;

        public void Publish(EventData data)
        {
            _publish?.Invoke(data);
        }

        public void Subscribe(Action<EventData> action)
        {
            if (action == null) return;

            _publish += action;
        }

        public void Unsubscribe(Action<EventData> action)
        {
            if (action == null) return;

            _publish -= action;
        }
    }
}
