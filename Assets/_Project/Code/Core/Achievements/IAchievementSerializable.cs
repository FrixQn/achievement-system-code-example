namespace DemoProject.Core.Achievements
{
    public interface IAchievementSerializable
    {
        public void LoadFromJSON(string json);
        public string ToJSON();
    }
}
