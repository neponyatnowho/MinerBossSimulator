using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItemPanel : MonoBehaviour
{
    [SerializeField] private ZonesMenager _zonesMenager;
    [SerializeField] private TMP_Text _nameZoneText;
    [SerializeField] private TMP_Text _rotsheldCoinChanceValueText;
    [SerializeField] private TMP_Text _workPaymentValueText;
    [SerializeField] private TMP_Text _energyRestorationValueText;
    [SerializeField] private TMP_Text _satietyRestorationValueText;

    [Header("Rock")]
    [SerializeField] private TMP_Text _rockLevelText;
    [SerializeField] private TMP_Text _rockNextLevelUpText;
    [SerializeField] private TMP_Text _rockPriceText;
    [SerializeField] private Button _rockUpgradeButton;

    [Header("Tree")]
    [SerializeField] private TMP_Text _treeLevelText;
    [SerializeField] private TMP_Text _treeNextLevelUpText;
    [SerializeField] private TMP_Text _treePriceText;
    [SerializeField] private Button _treeUpgradeButton;


    [Header("Bed")]
    [SerializeField] private TMP_Text _bedLevelText;
    [SerializeField] private TMP_Text _bedNextLevelUpText;
    [SerializeField] private TMP_Text _bedPriceText;
    [SerializeField] private Button _bedUpgradeButton;



    private PlayZone _playZone;
    public void UpdateInfo(PlayZone playZone)
    {
        _playZone = playZone;
        _nameZoneText.text = _playZone.Name;
        _rotsheldCoinChanceValueText.text = _playZone.RotsheldMoneyChance.ToString(format: "0.##") + " %";
        _workPaymentValueText.text = _playZone.WorkPayment.ToString(format: "0.##") + " /s";
        _energyRestorationValueText.text = _playZone.Bed.Energy.ToString(format: "0.##");
        _satietyRestorationValueText.text = _playZone.AppleTree.Satiety.ToString(format: "0.##");

        _rockLevelText.text = _playZone.Rock.Level.ToString() + " lvl";
        var nextUplvl = _playZone.Rock.Level + 10 - (_playZone.Rock.Level % 10);
        _rockNextLevelUpText.text = "x2: " + nextUplvl.ToString() + " lvl";
        _rockPriceText.text = FormatNumsHelper.FormatNum(_playZone.Rock.UpgradePrice);

        _treeLevelText.text = _playZone.AppleTree.Level.ToString() + " lvl";
        nextUplvl = _playZone.AppleTree.Level + 10 - (_playZone.AppleTree.Level % 10);
        _treeNextLevelUpText.text = "x2: " + nextUplvl.ToString() + " lvl";
        _treePriceText.text = FormatNumsHelper.FormatNum(_playZone.AppleTree.UpgradePrice);

        _bedLevelText.text = _playZone.Bed.Level.ToString() + " lvl";
        nextUplvl = _playZone.Bed.Level + 10 - (_playZone.Bed.Level % 10);
        _bedNextLevelUpText.text = "x2: " + nextUplvl.ToString() + " lvl";
        _bedPriceText.text = FormatNumsHelper.FormatNum(_playZone.Bed.UpgradePrice);
    }

    private void OnEnable()
    {
        _zonesMenager.MoneyChanged += MoneyChanged;
        _rockUpgradeButton.onClick.AddListener(UpdateRock);
        _treeUpgradeButton.onClick.AddListener(UpdateTree);
        _bedUpgradeButton.onClick.AddListener(UpdateBed);
        MoneyChanged(_zonesMenager.Money);

    }

    private void OnDisable()
    {
        _zonesMenager.MoneyChanged -= MoneyChanged;
        _rockUpgradeButton.onClick.RemoveAllListeners();
        _treeUpgradeButton.onClick.RemoveAllListeners();
        _bedUpgradeButton.onClick.RemoveAllListeners();
    }

    private void MoneyChanged(float money)
    {
        _rockUpgradeButton.interactable = _playZone.Rock.UpgradePrice <= money;
        _treeUpgradeButton.interactable = _playZone.AppleTree.UpgradePrice <= money;
        _bedUpgradeButton.interactable = _playZone.Bed.UpgradePrice <= money;
    }

    private void UpdateRock()
    {
        if(_zonesMenager.IsPlayZoneItemUpdated(_playZone, "rock"))
        {
            _playZone.Rock.UpdateInfo();
            _playZone.UpdateMoneyInfo();
            UpdateInfo(_playZone);
        }
    }

    private void UpdateTree()
    {
        if (_zonesMenager.IsPlayZoneItemUpdated(_playZone, "tree"))
        {
            _playZone.AppleTree.UpdateInfo();
            UpdateInfo(_playZone);
        }
    }

    private void UpdateBed()
    {
        if (_zonesMenager.IsPlayZoneItemUpdated(_playZone, "bed"))
        {
            _playZone.Bed.UpdateInfo();
            UpdateInfo(_playZone);
        }
    }

}
