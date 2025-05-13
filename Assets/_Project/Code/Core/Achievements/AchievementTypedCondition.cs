using Newtonsoft.Json.Linq;

namespace DemoProject.Core.Achievements
{
    public abstract class AchievementCondition<T> : AchievementCondition, ITypedCondition<T>
    {
        public T Default => default;
        public T Current { get; protected set; }
        public T Target { get; protected set; }
        public override float Progress { get; protected set; }

        public AchievementCondition(T value)
        {
            Current = Default;
            Target = value;
        }

        public override void Reset()
        {
            Current = Default;
        }

        public override void OnDeserialize(JObject obj)
        {
            Current = obj.GetValue(nameof(Current)).Value<T>();
        }
    }
}
