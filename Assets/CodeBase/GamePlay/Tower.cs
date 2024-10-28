using CodeBase.Data;
using UnityEngine;

namespace CodeBase.GamePlay
{
    public class Tower : MonoBehaviour
    {
        private TowerData _data;
        
        public void Init(TowerData data)
        {
            _data = data;
        }
    }
}