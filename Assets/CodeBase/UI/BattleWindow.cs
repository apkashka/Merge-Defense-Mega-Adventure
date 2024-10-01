using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BattleWindow : MonoBehaviour
{
    private const string Shoot = "СТРЕЛЯЕМ";
    private const string Build = "СТРОИМ";

    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _buttonText;

    [SerializeField] private WeaponSystem _weaponSystem;

    // Start is called before the first frame update
    void Start()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        _weaponSystem.Active = !_weaponSystem.Active;
        _buttonText.text = _weaponSystem.Active ? Shoot : Build;
    }
}