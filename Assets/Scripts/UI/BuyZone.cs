using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyZone : MonoBehaviour
{
    [SerializeField] private ZonesMenager _zonesMenager;

    [SerializeField] private PlayZone _playZone;
    [SerializeField] private float _nonInteractableAlpha;
      
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Image _priceImage;

    private float _price;
    private Button _buyButton;
    private Camera _mainCamera;

    private void OnEnable()
    {
        _price = _playZone.Price;
        _priceText.text = FormatNumsHelper.FormatNum(_price);
        _buyButton = GetComponent<Button>();
        _buyButton.onClick.AddListener(OnBuyButtonClick);
        _zonesMenager.MoneyChanged += OnMoneyChanged;
        _mainCamera = Camera.main;
    }

    private void OnDisable()
    {
        _zonesMenager.MoneyChanged -= OnMoneyChanged;
        _buyButton.onClick.RemoveAllListeners();
    }

    private void Update()
    {
        Vector3 cameraDirection = new Vector3(transform.position.x, _mainCamera.transform.position.y, _mainCamera.transform.position.z);
        transform.rotation  = Quaternion.LookRotation(transform.position - cameraDirection);
    }
    
    private void OnMoneyChanged(float money)
    {
        bool isInteractable = _zonesMenager.IsEnoughMoney(_price);
        SetBuyButtonInteractable(isInteractable);
    }

    private void OnBuyButtonClick()
    {
        if (_zonesMenager.IsNewZoneBought(_playZone))
        {
            gameObject.SetActive(false);
        }
    }

    private void SetBuyButtonInteractable(bool isInteractable)
    {
        Color color = _priceText.color;
        color.a = isInteractable ? 1f : _nonInteractableAlpha;
        _priceText.color = color;
        _priceImage.color = color;
        _buyButton.interactable = isInteractable;
    }
}
