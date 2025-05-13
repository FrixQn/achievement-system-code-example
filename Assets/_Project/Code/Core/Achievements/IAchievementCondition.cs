using DemoProject.Core.Events;

namespace DemoProject.Core.Achievements
{
    public interface IAchievementCondition
    {
        public float Progress { get; }
        public (bool isMet, bool isChanged) Evaluate(EventData eventData);
        public void Reset();
    }
}
