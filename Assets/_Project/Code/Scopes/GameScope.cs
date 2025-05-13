using DemoProject.Configs;
using DemoProject.Core.Achievements;
using DemoProject.Core.Events;
using DemoProject.Gameplay;
using DemoProject.Models;
using DemoProject.Presenters;
using DemoProject.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DemoProject.Scopes
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private AchievementsView _view;
        [SerializeField] private GridSpawner _spawner;
        [SerializeField] private GameplayConfig _gameplayConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<EventsManager>(Lifetime.Scoped).As<IEventsManager>();
            
            builder.RegisterEntryPoint<GameplayModel>(Lifetime.Scoped).As<IGameplayModel>();
            builder.RegisterInstance(_spawner);
            builder.RegisterInstance(_gameplayConfig).As<IGameplayConfig>();

            builder.Register<AcheivementSerializer>(Lifetime.Scoped).As<IAchievementSerializer>();
            builder.Register<AchievementsSystem>(Lifetime.Scoped);

            builder.RegisterInstance(_view).As<IAchievementsView>();

            builder.RegisterEntryPoint<GameplayPresenter>(Lifetime.Scoped);
        }
    }
}
