using DemoProject.Core.Achievements;
using DemoProject.Models;
using DemoProject.UI;
using System;
using VContainer;
using VContainer.Unity;

namespace DemoProject.Presenters
{
    public class GameplayPresenter: IInitializable, IDisposable
    {
        private const string GAME_TIME_ACHIEVEMENT_KEY = "game_time";
        private const string DESTROY_ANY_OBJECTS_ACHIEVEMENT_KEY = "destroy_objects";
        private const string DESTROY_RED_OBJECTS_ACHIEVEMENT_KEY = "destroy_red_objects";
        private readonly IGameplayModel _gameplayModel;
        private readonly IAchievementsView _achievementsView;
        private readonly AchievementsSystem _achievementsSystem;

        [Inject]
        public GameplayPresenter(IGameplayModel gameplayModel, IAchievementsView view, AchievementsSystem achievementsSystem)
        {
            _gameplayModel = gameplayModel;
            _achievementsSystem = achievementsSystem;
            _achievementsView = view;

            _gameplayModel.StartGame();
            achievementsSystem.GetAchievement(GAME_TIME_ACHIEVEMENT_KEY).ProgressChanged += UpdateAchievementProgress;
            achievementsSystem.GetAchievement(DESTROY_ANY_OBJECTS_ACHIEVEMENT_KEY).ProgressChanged += UpdateAchievementProgress;
            achievementsSystem.GetAchievement(DESTROY_RED_OBJECTS_ACHIEVEMENT_KEY).ProgressChanged += UpdateAchievementProgress;
        }

        public void Initialize()
        {
            DisplayAllAchievements();
        }

        private void DisplayAllAchievements()
        {
            foreach(var achievement in _achievementsSystem.Achievements)
            {
                UpdateAchievementProgress(achievement);
            }
        }

        private void UpdateAchievementProgress(IAchievement achievement)
        {
            if (achievement.Key == GAME_TIME_ACHIEVEMENT_KEY)
            {
                int current = (achievement.Conditions[0] as ITypedCondition<int>).Current;
                int target = (achievement.Conditions[0] as ITypedCondition<int>).Target;
                _achievementsView.UpdateAchievementProgress(achievement.Key,
                    current, target, achievement.Conditions[0].Progress, FormatTwoTimes(current, target));
            }

            if (achievement.Key == DESTROY_ANY_OBJECTS_ACHIEVEMENT_KEY || achievement.Key == DESTROY_RED_OBJECTS_ACHIEVEMENT_KEY)
            {
                _achievementsView.UpdateAchievementProgress(achievement.Key,
                    (achievement.Conditions[0] as ITypedCondition<int>).Current,
                    (achievement.Conditions[0] as ITypedCondition<int>).Target,
                    achievement.Conditions[0].Progress);
            }
        }

        private string FormatTwoTimes(int sec1, int sec2, string pattern = "{0:mm\\:ss}/{1:mm\\:ss}")
        {
            return string.Format(pattern,
                TimeSpan.FromSeconds(sec1),
                TimeSpan.FromSeconds(sec2));
        }

        public void Dispose()
        {
            _achievementsSystem.GetAchievement(GAME_TIME_ACHIEVEMENT_KEY).ProgressChanged -= UpdateAchievementProgress;
            _achievementsSystem.GetAchievement(DESTROY_ANY_OBJECTS_ACHIEVEMENT_KEY).ProgressChanged -= UpdateAchievementProgress;
            _achievementsSystem.GetAchievement(DESTROY_RED_OBJECTS_ACHIEVEMENT_KEY).ProgressChanged -= UpdateAchievementProgress;
        }
    }
}
