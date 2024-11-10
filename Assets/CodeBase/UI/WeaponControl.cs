using CodeBase.Systems;
using UnityEngine;

namespace CodeBase.UI
{
    internal class WeaponControl : MonoBehaviour
    {
        [SerializeField] private WeaponSystem _weaponSystem;
        public void Activate(bool active)
        {
            gameObject.SetActive(active);
            _weaponSystem.Active = false;
        }
    }
}