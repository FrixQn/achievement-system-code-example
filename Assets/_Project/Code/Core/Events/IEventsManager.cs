using System;

namespace DemoProject.Core.Events
{
    public interface IEventsManager
    {
        public void Publish(EventData data);
        public void Subscribe(Action<EventData> action);
        public void Unsubscribe(Action<EventData> action);
    }

}
