namespace DemoProject.Core.Achievements
{
    public interface IAchievementsSystem
    {
        void Add(IAchievement achievement);
        void AddRange(params IAchievement[] achievements);
        public IAchievement GetAchievement(string key);
        public T GetAchievement<T>(string key) where T : IAchievement;
    }
}
