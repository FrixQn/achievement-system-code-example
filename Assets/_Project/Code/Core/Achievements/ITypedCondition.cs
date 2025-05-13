namespace DemoProject.Core.Achievements
{
    public interface ITypedCondition<T> : IAchievementCondition
    {
        public T Default { get; }
        public T Current { get; }
        public T Target { get; }
    }
}
