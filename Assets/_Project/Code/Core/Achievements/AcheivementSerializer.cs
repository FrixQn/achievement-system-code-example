using System;

namespace DemoProject.Core.Achievements
{
    public class AcheivementSerializer : IAchievementSerializer
    {
        public string Serialize(IAchievement achievement)
        {
            if (achievement is IAchievementSerializable serializable)
                return serializable.ToJSON();
            else
                throw new NotImplementedException($"{achievement.Key} doesn't implement interface {nameof(IAchievementSerializable)}");
        }

        public void Deserialize(IAchievement achievement, string json)
        {
            if (achievement is IAchievementSerializable serializable)
                serializable.LoadFromJSON(json);
            else
                throw new NotImplementedException($"{achievement.Key} doesn't implement interface {nameof(IAchievementSerializable)}");
        }
    }
}
