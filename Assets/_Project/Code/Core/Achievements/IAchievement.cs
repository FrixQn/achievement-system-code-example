using DemoProject.Core.Events;
using System;
using System.Collections.Generic;

namespace DemoProject.Core.Achievements
{
    public interface IAchievement : IAchievementSerializable
    {
        public string Key { get; }
        public bool IsCompleted { get; }
        IReadOnlyList<IAchievementCondition> Conditions { get; }
        public void UpdateConditions(EventData data);

        public event Action<IAchievement> ProgressChanged;
        public event Action<IAchievement> Completed;
    }
}
