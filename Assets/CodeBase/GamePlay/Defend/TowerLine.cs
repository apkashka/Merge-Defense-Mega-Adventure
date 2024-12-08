using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.GamePlay
{
    public class TowerLine : MonoBehaviour
    {
        [SerializeField] private List<TowerSpot> _spots;
        
        public List<TowerSpot> Spots => _spots;
    }
}
