using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Configs
{
    [CreateAssetMenu(fileName = "Core", menuName = "Scriptable/Configs/CoreConfig")]
    public class CoreConfig : ScriptableObject
    {
        public LevelData[] levels;
    }
}
