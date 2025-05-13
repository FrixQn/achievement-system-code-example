using DemoProject.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

namespace DemoProject.Core.Achievements
{
    public class AchievementsSystem : IAchievementsSystem, IDisposable
    {
        private readonly IEventsManager _eventsManager;
        private readonly IAchievementSerializer _serializer;
        private Dictionary<string, IAchievement> _achievement = new();

        public IAchievement[] Achievements => _achievement.Values.ToArray();

        [Inject]
        public AchievementsSystem(IAchievementSerializer serializer, IEventsManager eventsManager)
        {
            Application.focusChanged += OnFocusChanged;
            _serializer = serializer;
            _eventsManager = eventsManager;

            _eventsManager.Subscribe(OnEventPublished);
        }

        private void OnEventPublished(EventData data)
        {
            foreach(var achievement in _achievement.Values)
            {
                achievement.UpdateConditions(data);
            }
        }

        private void OnFocusChanged(bool focus)
        {
            if (!focus)
                SaveProgress();
        }

        public void Add(IAchievement achievement)
        {
            if (_achievement.TryAdd(achievement.Key, achievement))
            {
                if (PlayerPrefs.HasKey(achievement.Key))
                {
                    _serializer.Deserialize(achievement, PlayerPrefs.GetString(achievement.Key));
                }
            }
        }

        public void AddRange(params IAchievement[] achievements)
        {
            foreach (var achievement in achievements)
            {
                Add(achievement);
            }
        }

        public IAchievement GetAchievement(string key)
        {
            if (!_achievement.TryGetValue(key, out IAchievement result))
                throw new ArgumentException($"Achievement with key: {key} not found");

            return result;
        }

        public T GetAchievement<T>(string key) where T : IAchievement
        {
            if (!_achievement.TryGetValue(key, out IAchievement result))
                throw new ArgumentException($"Achievement with key: {key} not found");

            return (T)result;
        }

        private void SaveProgress()
        {
            foreach (var achievement in _achievement)
            {
                PlayerPrefs.SetString(achievement.Key, _serializer.Serialize(achievement.Value));
            }
            PlayerPrefs.Save();
        }

        public void Dispose()
        {
            Application.focusChanged -= OnFocusChanged;
            _eventsManager.Unsubscribe(OnEventPublished);
        }
    }
}
