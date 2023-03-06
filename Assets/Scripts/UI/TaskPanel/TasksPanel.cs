using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TasksPanel : MonoBehaviour
{
    [SerializeField] private ZonesMenager _zonesManager;
    [SerializeField] private TMP_Text _taskText;
    [SerializeField] private TMP_Text _progresText;
    [SerializeField] private Image _taskImage;
    [SerializeField] private Slider _slider;
    [SerializeField] private Task _testTask;

    private void OnEnable()
    {
        _zonesManager.ActiveZonesCountChanged += OnZonesCountChanged;
    }
    private void OnDisable()
    {
        _zonesManager.ActiveZonesCountChanged -= OnZonesCountChanged;

    }
    private void Start()
    {
        _taskText.text = _testTask.Description;
        _taskImage.sprite = _testTask.TaskSprite;
        _slider.maxValue = _testTask.TaskCount;
    }

    private void OnZonesCountChanged(int activeZoneCount)
    {
        _slider.value = activeZoneCount;
        _progresText.text = activeZoneCount.ToString() + "/" + _testTask.TaskCount;

    }
}
