using CodeBase.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleWindow : MonoBehaviour
{
    private const string Shoot = "СТРЕЛЯЕМ";
    private const string Build = "СТРОИМ";

    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private WeaponControl _weaponControl;
    [SerializeField] private TowerBuild _towerBuild;

    private bool _isBuild;

    // Start is called before the first frame update
    void Start()
    {
        _button.onClick.AddListener(OnButtonClicked);
        SetBuild(true);
    }

    private void SetBuild(bool build)
    {
        _buttonText.text = build ? Build : Shoot;
        _towerBuild.Activate(build);
        _weaponControl.Activate(!build);
        _isBuild = build;
    }

    private void OnButtonClicked()
    {
        SetBuild(!_isBuild);
    }
}