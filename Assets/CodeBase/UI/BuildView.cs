using CodeBase.GamePlay;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class BuildView : MonoBehaviour
    {
        [SerializeField] private Button _towerButton;
        [SerializeField] private BuildField _buildField; //todo construct

        private ProgressService _progress;
        private readonly int _price = 10; //todo

        public void Construct(ProgressService progressService)
        {
            _progress = progressService;
        }
        private void Awake()
        {
            _towerButton.onClick.AddListener(BuildTower);
        }



        private void BuildTower()
        {
            if (_progress.Gold.Value < _price)
            {
                Debug.LogError("can't build");
                return;
            }

            _progress.Gold.Value -= _price;
            _buildField.CreateNewTower();
        }

        public void Activate(bool build)
        {
            gameObject.SetActive(build);
        }
    }
}