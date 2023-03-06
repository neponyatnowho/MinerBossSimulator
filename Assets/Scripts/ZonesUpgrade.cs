using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonesUpgrade : MonoBehaviour
{
    [SerializeField] private UpgradeItemPanel _upgradePanel;
    [SerializeField] private CameraClick _cameraClick;

    private void OnEnable()
    {
        _cameraClick.OnPlayZoneClick += OpenUpgradePanel;
    }
    private void OnDisable()
    {
        _cameraClick.OnPlayZoneClick -= OpenUpgradePanel;
    }

    private void OpenUpgradePanel(PlayZone playZone)
    {
        _upgradePanel.UpdateInfo(playZone);
        _upgradePanel.gameObject.SetActive(true);
    }
}
