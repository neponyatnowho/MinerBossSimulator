using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePanelButton : MonoBehaviour
{
    [SerializeField]private GameObject _panel;

    private Button _closeButton;

    private void OnEnable()
    {
        _closeButton = GetComponent<Button>();
        _closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveAllListeners();
    }

    private void OnCloseButtonClick()
    {
        _panel.SetActive(false);
    }
}
