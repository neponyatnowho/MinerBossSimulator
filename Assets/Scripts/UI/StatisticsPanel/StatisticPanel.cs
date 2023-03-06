using System.Collections.Generic;
using UnityEngine;

public class StatisticPanel : MonoBehaviour
{
    [SerializeField] private ZonesMenager _zonesMenager;
    [SerializeField] private TotalInfo _totalInfo;
    [SerializeField] private List<ZoneInfo> _zonesInfo;

    private void OnEnable()
    {
        List<PlayZone> activePlayZones = _zonesMenager.GetActivePlayZones();

        foreach (var zoneInfo in _zonesInfo)
        {
            zoneInfo.gameObject.SetActive(false);
        }

        for (int i = 0; i < activePlayZones.Count && i < _zonesInfo.Count; i++)
        {
            _zonesInfo[i].gameObject.SetActive(true);
            _zonesInfo[i].FillInfo(activePlayZones[i]);
        }

        _totalInfo.FillInfo(_zonesMenager);
    }
}