using UnityEngine;

namespace CodeBase.Data
{
    [CreateAssetMenu(menuName = "Scriptable/Level", fileName = "Level")]
    public class LevelData : ScriptableObject
    {
        public WaveData[] waves;
        public int delay; //todo to array waves length
    }
}