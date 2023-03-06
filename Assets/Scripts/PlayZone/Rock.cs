using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private GameObject[] _rocksModels;
    [SerializeField] private int _level;
    [SerializeField] private float _workPayment;
    [SerializeField] private float _rotsheldMoneyChance;
    [SerializeField] private float _upgradePrice;

    public int Level => _level;
    public float WorkPayment => _workPayment;
    public float RotsheldMoneyChance => _rotsheldMoneyChance;
    public float UpgradePrice => _upgradePrice;
    public RockEnterPoint RockEnterPoint { get; private set;}

    private void Awake()
    {
        RockEnterPoint = GetComponentInChildren<RockEnterPoint>();
        SetActiveModel();
    }
    public void UpdateInfo()
    {
        _level++;
        if(_level % 10 == 0)
        {
            _upgradePrice *= 2f;
            _workPayment *= 2f;
            _rotsheldMoneyChance *= 1.5f;
            SetActiveModel();
            return;
        }
        _upgradePrice *= 1.2f;
        _workPayment += _workPayment/10f;
        _rotsheldMoneyChance += 0.1f;
    }

    private void SetActiveModel()
    {
        var index = _level / 10;
        if (index > _rocksModels.Length - 1)
            return;

        for (int i = 0; i <= _rocksModels.Length - 1; i++)
        {
            if(i == index)
            {
                _rocksModels[i].SetActive(true);
            }
            else
            {
                _rocksModels[i].SetActive(false);
            }
        }
    }
}
