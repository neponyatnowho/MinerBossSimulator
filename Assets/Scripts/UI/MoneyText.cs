using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyText : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private TMP_Text _rotsheldMoneyText;
    [SerializeField] private ZonesMenager _zoneMenager;

    private void OnEnable()
    {
        _zoneMenager.MoneyChanged += SetMoneyText;
        _zoneMenager.RotsheldMoneyChanged += SetRotsheltMoneyText;
    }

    private void OnDisable()
    {
        _zoneMenager.MoneyChanged -= SetMoneyText;
        _zoneMenager.RotsheldMoneyChanged -= SetRotsheltMoneyText;

    }

    private void SetMoneyText(float money)
    {
        _moneyText.text = FormatNumsHelper.FormatNum(money);
    }

    private void SetRotsheltMoneyText(float money)
    {
        _rotsheldMoneyText.text = FormatNumsHelper.FormatNum(money);
    }
}
