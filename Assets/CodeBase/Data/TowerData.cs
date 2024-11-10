using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Data
{
    [CreateAssetMenu(menuName = "Scriptable/Tower", fileName = "TowerData")]
    public class TowerData : ScriptableObject
    {
        public int id;
        public int damage;
        public int powerUp;
        
        public Leveling[] leveling;
        public float reloadTime = 2;

        public int GetDamage(int level)
        {
            return damage + level * powerUp;
        }
    }
    
    [System.Serializable]
    public class Leveling
    {
        public string prefab;
    }

    public enum TowerStyle
    {
        None = 0,
        Shooting = 1,
        Smashing = 2
    }

}