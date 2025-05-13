namespace DemoProject
{
    public interface IConditionDataSerialization
    {
        public string Serialize();
        public void Deserialize(string data);
    }
}
