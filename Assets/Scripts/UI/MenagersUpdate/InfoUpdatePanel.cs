using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InfoUpdatePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _specialDescriptionIn;
    [SerializeField] private TMP_Text _specialDescriptionOut;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Image _image;
    [SerializeField] private Button _updateButton;

    private float _price;

    [SerializeField] private ZonesMenager _zonesManager;

    public event UnityAction UpdateDone;
    public event UnityAction PanelClosed;

    private void OnEnable()
    {
        _updateButton.interactable = false;
        _updateButton.onClick.AddListener(OnUpdateButtonClick);
    }

    private void OnDisable()
    {
        PanelClosed?.Invoke();
        _updateButton.onClick.RemoveAllListeners();

    }

    private void Update()
    {
        if (enabled == false)
            return;

        _updateButton.interactable = _zonesManager.IsEnoughRotsheldMoney(_price);

    }

    public void FillInfoPanel(string name, string discription, string specialDiscription, float specialCoeficient, float curentCoeficient, float price, Sprite image)
    {
        _name.text = name;
        _description.text = discription;
        _specialDescriptionIn.text = specialDiscription + curentCoeficient.ToString();
        _specialDescriptionOut.text = specialDiscription + (curentCoeficient + specialCoeficient).ToString(format: "#.##");
        _priceText.text = FormatNumsHelper.FormatNum(price);
        _price = price;
        _image.sprite = image;
    }

    private void OnUpdateButtonClick()
    {
        if(_zonesManager.IsEnoughRotsheldMoney(_price))
        {
            _updateButton.interactable = false;
            UpdateDone?.Invoke();
        }
    }
}
