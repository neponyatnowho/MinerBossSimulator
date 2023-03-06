using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotsheldMenagerCard : Card
{
    protected override void Enable()
    {
        _currentCoifitient = _zonesMenager.RotsheldMoneyCoefficient;
    }

    protected override void OnUpdateDone()
    {
        if(_zonesMenager.IsEnoughRotsheldMoney(_price))
        {
            _zonesMenager.OnUpdateRotsheldMenager(this);
            _price *= 2;
            _sprecialCoeficient *= 2;
            _currentCoifitient = _zonesMenager.RotsheldMoneyCoefficient;
            UpdateInfo();
        }
    }
}
