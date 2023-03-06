using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayZone : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private float _price;
    [SerializeField] private float _workPayment;
    private float _rotsheldMoneyChance;
    private AppleTree _appleTree;
    private Worker _worker;
    private Bed _bed;
    private Rock _rock;


    public string Name => _name;
    public float Price => _price;
    public Worker Worker => _worker;
    public AppleTree AppleTree => _appleTree;
    public Bed Bed => _bed;
    public Rock Rock => _rock;

    public float WorkPayment => _workPayment;
    public float RotsheldMoneyChance => _rotsheldMoneyChance;

    public event UnityAction<PlayZone> WorkCompleted;
    private void Awake()
    {
        GetZoneInfo();
    }

    public void WorkIsComplete()
    {
        WorkCompleted?.Invoke(this);
    }

    private void GetZoneInfo()
    {
        _worker = GetComponentInChildren<Worker>();
        _rock = GetComponentInChildren<Rock>();
        _bed = GetComponentInChildren<Bed>();
        _appleTree = GetComponentInChildren<AppleTree>();
        UpdateMoneyInfo();
    }
    public void UpdateMoneyInfo()
    {
        _workPayment = _rock.WorkPayment;
        _rotsheldMoneyChance = _rock.RotsheldMoneyChance;
    }
}
