using CodeBase.Systems;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    internal class WeaponControl : MonoBehaviour
    {
        [SerializeField] private WeaponSystem _weaponSystem;
        [SerializeField] private Slider _slider;

        private void Awake()
        {
            _slider.onValueChanged.AddListener(OnSliderMove);
        }

        private void OnSliderMove(float val)
        {
            _weaponSystem.MoveWeapon(val);
        }

        public void Activate(bool active)
        {
            gameObject.SetActive(active);
            _weaponSystem.Active = active;
        }
    }
}