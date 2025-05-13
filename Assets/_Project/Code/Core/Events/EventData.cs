using System.Collections.Generic;

namespace DemoProject.Core.Events
{
    public class EventData
    {
        public readonly string Name;
        public readonly Dictionary<string, object> Parameters = new ();

        public EventData(string name, Dictionary<string, object> parameters)
        {
            Name = name;
            Parameters = parameters;
        }
    }
}
