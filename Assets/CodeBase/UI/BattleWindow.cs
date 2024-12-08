using CodeBase.Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class BattleWindow : MonoBehaviour
    {
        private const string Shoot = "К СТРЕЛЬБЕ";
        private const string Build = "НА СТРОЙКУ";

        [SerializeField] private Button _changeButton,_saveButton;
        [SerializeField] private TextMeshProUGUI _buttonText;
        [SerializeField] private WeaponControl _weaponControl;
        [SerializeField] private BuildView _buildView;

        private ProgressService _progress;
        private bool _isBuild;

        public void Construct(ProgressService progress)
        {
            _progress = progress;
        }

        private void Start()
        {
            _changeButton.onClick.AddListener(OnButtonClicked);
            _saveButton.onClick.AddListener(SaveClicked);
            SetBuild(true);
        }

        private void SaveClicked()
        {
            _progress.SaveProgress();
        }

        private void SetBuild(bool build)
        {
            _buttonText.text = build ? Shoot : Build;
            _buildView.Activate(build);
            _weaponControl.Activate(!build);
            _isBuild = build;
        }

        private void OnButtonClicked()
        {
            SetBuild(!_isBuild);
        }
    }
}