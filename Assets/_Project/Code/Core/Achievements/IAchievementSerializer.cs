namespace DemoProject.Core.Achievements
{
    public interface IAchievementSerializer
    {
        string Serialize(IAchievement achievement);
        void Deserialize(IAchievement achievement, string json);
    }
}
