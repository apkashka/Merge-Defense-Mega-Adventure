using CodeBase.Infrastructure.Services;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class ResourcesView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthText, _coinsText, _ammunitionText;
        [SerializeField] private Image _healthFill;

        private int _maxHealth;

        public void Construct(ProgressService progress)
        {
            SetMaxHealth(progress.Health.Value);
            progress.Health.Subscribe(val =>
            {
                _healthText.text = $"{val}/{_maxHealth}";
                _healthFill.fillAmount = (float)val / _maxHealth;
            }).AddTo(this);
            progress.Gold.Subscribe(val => _coinsText.text = val.ToString()).AddTo(this);
            progress.Clip.Subscribe( val => _ammunitionText.text = val.ToString()).AddTo(this);
        }

        public void SetMaxHealth(int value)
        {
            _maxHealth = value;
        }
    }
}