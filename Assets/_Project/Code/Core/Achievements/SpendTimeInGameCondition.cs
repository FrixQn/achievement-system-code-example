using DemoProject.Core.Events;
using UnityEngine;

namespace DemoProject.Core.Achievements
{
    public class SpendTimeInGameCondition : AchievementCondition<int>
    {
        public SpendTimeInGameCondition(int seconds) : base(seconds) { }

        public override (bool isMet, bool isChanged) Evaluate(EventData eventData)
        {
            if (eventData.Name != "GameTime")
                return (false, false);

            int previous = Current;
            Current += (int)eventData.Parameters["Increment"];
            Progress = Mathf.InverseLerp(Default, Target, Current);
            return (Current >= Target, previous != Current);
        }
    }
}
