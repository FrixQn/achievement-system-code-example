using System;
using System.Collections.Generic;
using UnityEngine;

namespace DemoProject.UI
{
    public interface IAchievementsView
    {
        void UpdateAchievementProgress(string key, object currentProgress, object requiredProgress, float normalizedValue, string format = default);
    }

    public class AchievementsView : MonoBehaviour, IAchievementsView
    {
        [SerializeField] private AchievementCard[] _achievementCards = Array.Empty<AchievementCard>();
        private Dictionary<string, AchievementCard> _achievementCard = new();

        private void Awake()
        {
            InitializeCardsDictionary();
        }

        public void UpdateAchievementProgress(string key, object currentProgress, object requieredProgress, float normalizedValue, string format)
        {
            InitializeCardsDictionary();
            if (_achievementCard.TryGetValue(key, out AchievementCard card))
            {
                card.UpdateProgress(currentProgress, requieredProgress, normalizedValue, format);
            }
        }

        private void InitializeCardsDictionary()
        {
            if (_achievementCards.Length > _achievementCard.Count)
            {
                foreach (var card in _achievementCards)
                {
                    _achievementCard.TryAdd(card.Key, card);
                }
            }
        }
    }
}
