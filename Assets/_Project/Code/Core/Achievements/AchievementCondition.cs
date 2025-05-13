using DemoProject.Core.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DemoProject.Core.Achievements
{
    public abstract class AchievementCondition : IAchievementCondition, IConditionDataSerialization
    {
        public abstract float Progress { get; protected set; }

        public abstract (bool isMet, bool isChanged) Evaluate(EventData eventData);
        public abstract void Reset();
        public virtual string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void Deserialize(string data)
        {
            var obj = JObject.Parse(data);
            Progress = obj.GetValue(nameof(Progress)).Value<float>();
            OnDeserialize(obj);
        }

        public virtual void OnDeserialize(JObject obj) { }
    }
}
