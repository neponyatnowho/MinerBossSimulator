using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    [SerializeField] private ZonesMenager _zonesMenager;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private OpenBox _openBoxPanel;
    [SerializeField] private float _price;
    [SerializeField] private bool _isRotsheldsMoneyPrice;
    [SerializeField] private Sprite _prizeImage;
    [SerializeField] private float _prizeRange;


    private Button _buyButton;
    private float _prize;

    public bool IsRotsheldsMoneyPrice => _isRotsheldsMoneyPrice;
    public float Prize => _prize;
    public float Price => _price;

    private void OnEnable()
    {
        _buyButton= GetComponent<Button>();
        _priceText.text = FormatNumsHelper.FormatNum(_price);
        _buyButton.onClick.AddListener(OnBuyButtonClick);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveAllListeners();

    }

    private void Update()
    {
       if (_zonesMenager.IsEnoughMoney(_price) && !_isRotsheldsMoneyPrice || _zonesMenager.IsEnoughRotsheldMoney(_price) && _isRotsheldsMoneyPrice)
            _buyButton.interactable = true;
       else
            _buyButton.interactable = false;
    }

    private void OnBuyButtonClick()
    {
        if (_zonesMenager.IsEnoughMoney(_price) && !_isRotsheldsMoneyPrice || _zonesMenager.IsEnoughRotsheldMoney(_price) && _isRotsheldsMoneyPrice)
        {
            _prize = Random.Range((_prizeRange / 2f), _prizeRange);
            _zonesMenager.OnBoxOpen(this);
            _openBoxPanel.FillBoxInfo(_prizeImage, _prize);
            _openBoxPanel.gameObject.SetActive(true);
            _price *= 2f;
            _prizeRange *= 2f;
            _priceText.text = FormatNumsHelper.FormatNum(_price);
        }
        else
        {
            _buyButton.interactable = false;
        }

    }
}
