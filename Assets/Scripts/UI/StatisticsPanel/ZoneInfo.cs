using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZoneInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameZoneText;
    [SerializeField] private TMP_Text _rotsheldCoinChanceValueText;
    [SerializeField] private TMP_Text _workPaymentValueText;
    [SerializeField] private TMP_Text _energyRestorationValueText;
    [SerializeField] private TMP_Text _satietyRestorationValueText;


    public void FillInfo(PlayZone playZone)
    {
        _nameZoneText.text = playZone.Name;
        _rotsheldCoinChanceValueText.text = playZone.RotsheldMoneyChance.ToString(format: "0.##") + " %";
        _workPaymentValueText.text = playZone.WorkPayment.ToString(format: "0.##") + " /s";
        _energyRestorationValueText.text = playZone.Bed.Energy.ToString(format: "0.##");
        _satietyRestorationValueText.text = playZone.AppleTree.Satiety.ToString(format: "0.##");
    }
}
