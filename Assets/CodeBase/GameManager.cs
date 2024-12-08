using CodeBase.Configs;
using CodeBase.Data;
using CodeBase.GamePlay;
using CodeBase.Infrastructure.Services;
using CodeBase.Systems;
using CodeBase.UI;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeBase
{
    public class GameManager : MonoBehaviour
    {
        [Header("Configs")] 
        [SerializeField] private CoreConfig _core;

        [Header("UI")] //todo get UI out
        [SerializeField] private GameObject _gameplayPart;
        [SerializeField] private BattleWindow _battleWindow;
        [SerializeField] private ResourcesView _resourcesView;
        [SerializeField] private BuildView _buildView;
        
        [SerializeField] private GameObject _startMenu, _endLevel;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Button _startLevel, _nextLevel;
        
        [Header("Systems")]
        [SerializeField] private MonsterSystem _monsterSystem;
        [SerializeField] private WeaponSystem _weaponSystem;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private TowerSystem _towerSystem;
        
        [Header("Gameplay")]
        [SerializeField] private BattleGround _battleGround;
        [SerializeField] private BuildField _buildField;

        private ProgressService _progress;

        private LevelData CurrentLevel => _core.levels[_progress.Level.Value];


        private void Awake()
        {
            _startLevel.onClick.AddListener(StartLevel);
            _nextLevel.onClick.AddListener(GoToNextLevel);
        }

        private void Start()
        {
            _progress = new ProgressService();
            _progress.Health.Subscribe(CheckHealth).AddTo(this);
            
            _battleWindow.Construct(_progress);
            _resourcesView.Construct(_progress);
            _buildView.Construct(_progress);
            
            _monsterSystem.Init(_battleGround.SpawnPoint, _progress);
            _bulletSystem.Init();
            _weaponSystem.Construct(_progress, _bulletSystem);
            _weaponSystem.Init();
            _towerSystem.Init(); //todo construct??
            _buildField.Init(_progress);
            _battleGround.Construct(_monsterSystem);
            _battleGround.LastWaveCreated += StartCheckingMonsters;

            _levelText.text = "УРОВЕНЬ: " + (_progress.Level.Value + 1);
        }

        private void StartLevel()
        {
            _towerSystem.Active = true;
            _monsterSystem.Active = true;
            _bulletSystem.Active = true;
            _battleGround.StartLevel(CurrentLevel);
            _startMenu.SetActive(false);
            _gameplayPart.SetActive(true);
        }
        
        private void GoToNextLevel()
        {
            _progress.Level.Value++;
            _progress.SaveProgress();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void CheckHealth(int health)
        {
            if (health > 0)
            {
                return;
            }
            
            _monsterSystem.Active = false;
            _bulletSystem.Active = false;
            _towerSystem.Active = false;
            _weaponSystem.Active = false;
            Debug.LogError("GameOver");
        }

        private void StartCheckingMonsters()
        {
            _monsterSystem.NoMonsters += GameOver;
        }

        private void GameOver()
        {
            _monsterSystem.NoMonsters -= GameOver;
            
            _monsterSystem.Active = false;
            _bulletSystem.Active = false;
            _towerSystem.Active = false;
            _weaponSystem.Active = false;
            _endLevel.SetActive(true);
        }
    }
}