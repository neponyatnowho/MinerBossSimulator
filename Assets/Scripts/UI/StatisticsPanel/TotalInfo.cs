using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text _multiplyMValueText;
    [SerializeField] private TMP_Text _multiplyRValueText;
    [SerializeField] private TMP_Text _rotsheldCoinChanceValueText;
    [SerializeField] private TMP_Text _workPaymentValueText;


    public void FillInfo(ZonesMenager zoneMenager)
    {
        _multiplyMValueText.text = "x " + zoneMenager.MoneyCoefficient.ToString();
        _multiplyRValueText.text = "x " + zoneMenager.RotsheldMoneyCoefficient.ToString();
        _rotsheldCoinChanceValueText.text = zoneMenager.AllActiveZonesRotsheldChance() + " %";
        _workPaymentValueText.text = FormatNumsHelper.FormatNum(zoneMenager.AllActiveZonesWorkPayment() * zoneMenager.MoneyCoefficient) + " /s";
    }
}
