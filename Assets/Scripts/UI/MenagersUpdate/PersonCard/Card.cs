using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class Card : MonoBehaviour
{
    [SerializeField] protected ZonesMenager _zonesMenager;
    [Header("Info")]
    [SerializeField] private string _name;
    [SerializeField] private string _discription;
    [SerializeField] private string _sprecialDiscription;
    [Range(0.1f, 100)]
    [SerializeField] protected float _sprecialCoeficient;
    [SerializeField] protected float _price;
    [SerializeField] private Sprite _image;
    [SerializeField] private InfoUpdatePanel _infoUpdatePanel;
    protected float _currentCoifitient;

    [Header("Resources")]
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Image _cardImage;
    private Button _updateButton;

    public float SpecialCoeficient => _sprecialCoeficient;
    public float Price => _price;

    protected abstract void Enable();

    private void OnEnable()
    {
        Enable();
        _priceText.text = _price.ToString();
        _cardImage.sprite = _image;
        _updateButton = GetComponent<Button>();
        _updateButton.onClick.AddListener(OnUpdateButtonClick);

    }

    private void OnDisable()
    {
        _updateButton.onClick.RemoveAllListeners();

    }
    private void OnUpdateButtonClick()
    {
        _infoUpdatePanel.UpdateDone += OnUpdateDone;
        _infoUpdatePanel.PanelClosed += OnUpdatePanelClosed;
        UpdateInfo();
        _infoUpdatePanel.gameObject.SetActive(true);
    }

    protected void UpdateInfo()
    {
        _priceText.text = _price.ToString();
        _infoUpdatePanel.FillInfoPanel(_name, _discription, _sprecialDiscription, _sprecialCoeficient, _currentCoifitient, _price, _image);
    }

    protected abstract void OnUpdateDone();

    private void OnUpdatePanelClosed()
    {
        _infoUpdatePanel.UpdateDone -= OnUpdateDone;
        _infoUpdatePanel.PanelClosed -= OnUpdatePanelClosed;
    }
}
