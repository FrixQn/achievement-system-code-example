using DemoProject.Gameplay;
using UnityEngine;

namespace DemoProject.Configs
{
    
    [CreateAssetMenu(fileName = "GameplayConfig", menuName = "Project/Configs")]
    public class GameplayConfig : ScriptableObject, IGameplayConfig
    {
        [field: SerializeField] public Cube Blue { get; private set; }
        [field: SerializeField] public Cube Red { get; private set; }
    }
}
