using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMenagerCard : Card
{
    protected override void Enable()
    {
        _currentCoifitient = _zonesMenager.MoneyCoefficient;
    }

    protected override void OnUpdateDone()
    {
        if (_zonesMenager.IsEnoughRotsheldMoney(_price))
        {
            _zonesMenager.OnUpdateMoneyMenager(this);
            _price *= 2;
            _sprecialCoeficient *= 1.5f;
            _currentCoifitient = _zonesMenager.MoneyCoefficient;
            UpdateInfo();
        }
    }
}
