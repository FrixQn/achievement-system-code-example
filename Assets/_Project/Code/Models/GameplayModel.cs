using DemoProject.Configs;
using DemoProject.Core.Achievements;
using DemoProject.Core.Events;
using DemoProject.Gameplay;
using VContainer;
using VContainer.Unity;

namespace DemoProject.Models
{
    public class GameplayModel : IGameplayModel, ITickable
    {
        private const string GAME_TIME_ACHIEVEMENT_KEY = "game_time";
        private const string DESTROY_ANY_OBJECTS_ACHIEVEMENT_KEY = "destroy_objects";
        private const string DESTROY_RED_OBJECTS_ACHIEVEMENT_KEY = "destroy_red_objects";
        private readonly AchievementsSystem _achievementsSystem = default;
        private readonly IEventsManager _eventsManager = default;
        private readonly GridSpawner _spawner = default;
        private readonly IGameplayConfig _gameplayConfig = default;
        private readonly EventData _gameTimeEvent;
        private readonly EventData _objectDestroyedEvent;
        private float _updateTime;
        private int _timeUpdateInterval = 1;
        private int _time;

        [Inject]
        public GameplayModel(AchievementsSystem achievementsSystem, IEventsManager eventsManager,
            GridSpawner spawner, IGameplayConfig config)
        {
            _eventsManager = eventsManager;
            _achievementsSystem = achievementsSystem;
            _gameplayConfig = config;
            _spawner = spawner;

            _gameTimeEvent = new("GameTime", new ()
            {
                { "Value", 0},
                { "Increment", 0},
            });

            _objectDestroyedEvent = new("ObjectDestoryed", new()
            {
                { "Type", string.Empty},
                { "Increment", 0},
            });

            _achievementsSystem.Add(new Achievement(GAME_TIME_ACHIEVEMENT_KEY, new SpendTimeInGameCondition(120)));
            _achievementsSystem.Add(new Achievement(DESTROY_ANY_OBJECTS_ACHIEVEMENT_KEY, new DestroySpecificObjectsCondition(10)));
            _achievementsSystem.Add(new Achievement(DESTROY_RED_OBJECTS_ACHIEVEMENT_KEY, new DestroySpecificObjectsCondition(10, nameof(ObjectType.Red))));
        }

        public void StartGame()
        {
            var blueCubes = _spawner.SpawnObjects(_gameplayConfig.Blue, 5, 2, 2, 2, UnityEngine.Vector2.up * 1.5f);
            var redCubes = _spawner.SpawnObjects(_gameplayConfig.Red, 5, 2, 2, 2, UnityEngine.Vector2.down * 2f);

            foreach (var cube in blueCubes)
            {
                cube.Clicked += OnCubeClicked;
            }

            foreach (var cube in redCubes)
            {
                cube.Clicked += OnCubeClicked;
            }
        }

        private void OnCubeClicked(Cube obj)
        {
            obj.Clicked -= OnCubeClicked;
            PublishObjectDestoryedEvent(obj.Type, 1);
            obj.Destroy();
        }

        public void Tick()
        {
            _updateTime += UnityEngine.Time.deltaTime;

            if (_updateTime >= _timeUpdateInterval)
            {
                _time += _timeUpdateInterval;
                PublishGametimeEvent(_time, _timeUpdateInterval);
                _updateTime = 0;
            }
        }

        private void PublishGametimeEvent(int value, int increment)
        {
            _gameTimeEvent.Parameters["Value"] = value;
            _gameTimeEvent.Parameters["Increment"] = increment;
            _eventsManager.Publish(_gameTimeEvent);
        }

        private void PublishObjectDestoryedEvent(ObjectType type, int increment)
        {
            _objectDestroyedEvent.Parameters["Type"] = type.ToString();
            _objectDestroyedEvent.Parameters["Increment"] = increment;
            _eventsManager.Publish(_objectDestroyedEvent);
        }

        
    }
}
