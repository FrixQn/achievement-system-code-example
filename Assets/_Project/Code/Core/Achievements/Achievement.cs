using DemoProject.Core.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace DemoProject.Core.Achievements
{
    public class Achievement : IAchievement
    {
        private List<IAchievementCondition> _conditions = new();

        public string Key { get; private set; }
        public bool IsCompleted { get; private set; }
        public IReadOnlyList<IAchievementCondition> Conditions => _conditions;

        public event Action<IAchievement> ProgressChanged;
        public event Action<IAchievement> Completed;

        public Achievement(string key, IAchievementCondition condition)
        {
            Key = key;
            _conditions = new () { condition};
        }

        public Achievement(string key, IAchievementCondition[] conditions)
        {
            Key = key;
            _conditions = new (conditions);
        }

        public void UpdateConditions(EventData eventData)
        {
            if (IsCompleted) return;

            bool allCompleted = true;
            bool anyChanged = false;
            foreach (var condition in _conditions)
            {
                var (isMet, isChanged) = condition.Evaluate(eventData);
                allCompleted &= isMet;
                anyChanged |= isChanged;
            }

            if (anyChanged)
                ProgressChanged?.Invoke(this);

            IsCompleted = allCompleted;

            if (IsCompleted)
                Completed?.Invoke(this);

        }

        public void LoadFromJSON(string json)
        {
            JObject jobj = JObject.Parse(json);
            var condition = jobj.GetValue(nameof(Conditions));
            int index = 0;
            bool allCompleted = true;
            foreach (var jsonCondition in condition)
            {
                if (index < _conditions.Count)
                {
                    (_conditions[index] as IConditionDataSerialization).Deserialize(jsonCondition.ToString());
                    allCompleted &= _conditions[index].Progress == 1f;
                }
                index++;
            }

            ProgressChanged?.Invoke(this);
            if (allCompleted)
                Completed?.Invoke(this);

            IsCompleted = allCompleted;
        }

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
