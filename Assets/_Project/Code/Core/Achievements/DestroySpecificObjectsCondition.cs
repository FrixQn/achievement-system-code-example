using DemoProject.Core.Events;
using UnityEngine;

namespace DemoProject.Core.Achievements
{
    public class DestroySpecificObjectsCondition : AchievementCondition<int>
    {
        private readonly string _objectType;
        public override float Progress { get; protected set; }

        public DestroySpecificObjectsCondition(int count, string type = null) : base(count)
        {
            _objectType = type;
        }

        public override (bool isMet, bool isChanged) Evaluate(EventData eventData)
        {
            if (eventData.Name != "ObjectDestoryed")
                return (false, false);

            if ((string)eventData.Parameters["Type"] != _objectType && _objectType != null)
                return (false, false);

            int previous = Current;
            Current += (int)eventData.Parameters["Increment"];
            Progress = Mathf.InverseLerp(Default, Target, Current);
            return (Current >= Target, previous != Current);
        }
    }
}
