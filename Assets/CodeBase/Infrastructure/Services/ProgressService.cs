using UniRx;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
    public class ProgressService //todo rename
    {
        private const string ProgressKey = "Progress";

        public readonly ReactiveProperty<int> Level = new();
        public readonly ReactiveProperty<int> Gold = new();
        public readonly ReactiveProperty<int> Health = new();
        public readonly ReactiveProperty<int> Clip = new();
        public readonly TowerField TowerField;
        
        private MdmaPlayer _mdmaPlayer;

        public ProgressService()
        {
            LoadOrInit();
            TowerField = new TowerField();
        }

        private void LoadOrInit()
        {
            _mdmaPlayer = PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<MdmaPlayer>()
                          ?? new MdmaPlayer();

            LinkProperties();
        }

        private void LinkProperties()
        {
            //todo to this simple??
            Level.Value = _mdmaPlayer.level;
            Gold.Value = _mdmaPlayer.gold;
            Health.Value = _mdmaPlayer.health;
            Clip.Value = _mdmaPlayer.clip;
        }

        public void SaveProgress()
        {
            TowerField.Save();

            _mdmaPlayer.level = Level.Value;
            _mdmaPlayer.gold = Gold.Value;
            PlayerPrefs.SetString(ProgressKey, _mdmaPlayer.ToJson());
            PlayerPrefs.Save();
        }

        [MenuItem("MDMA/Reset progress")]
        public static void DeleteProgress()
        {
            TowerField.DeleteFile();

            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}