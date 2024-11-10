using CodeBase.GamePlay;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class TowerBuild: MonoBehaviour
    {
        [SerializeField] private Button _towerButton, _saveButton;
        [SerializeField] private BuildField _buildField; //todo construct

        private void Awake()
        {
            _towerButton.onClick.AddListener(_buildField.CreateNewTower);
            _saveButton.onClick.AddListener(_buildField.SaveField);
        }
        
        public void Activate(bool build)
        {
            gameObject.SetActive(build);
        }
    }
}