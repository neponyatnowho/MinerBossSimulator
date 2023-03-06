using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    [SerializeField] private GameObject[] _bedsModels;

    [SerializeField] private int _level;
    [SerializeField] private float _energy;
    [SerializeField] private float _upgradePrice;

    public float Energy => _energy;
    public float UpgradePrice => _upgradePrice;

    public int Level => _level;
    public BedEnterPoint BedEnterPoint { get; private set; }

    private void Awake()
    {
        BedEnterPoint = GetComponentInChildren<BedEnterPoint>();
        SetActiveModel();
    }

    public void UpdateInfo()
    {
        _level++;
        if (_level % 10 == 0)
        {
            _upgradePrice *= 2f;
            _energy += 0.1f;
            SetActiveModel();
            return;
        }
        _upgradePrice *= 1.2f;
        _energy += 0.01f;
    }

    private void SetActiveModel()
    {
        var index = _level / 10;
        if (index > _bedsModels.Length - 1)
            return;

        for (int i = 0; i <= _bedsModels.Length - 1; i++)
        {
            if (i == index)
            {
                _bedsModels[i].SetActive(true);
            }
            else
            {
                _bedsModels[i].SetActive(false);
            }
        }
    }
}
