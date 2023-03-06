using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

public class ZonesMenager : MonoBehaviour
{
    [SerializeField] BuyZone[] _buyZoneButtons;


    [SerializeField] private float _money;
    [SerializeField] private int _activeZonesCount;
    private float _rotsheldMoney;
    private PlayZone[] _playZones;

    [Range(1, 100)]
    [SerializeField] public float _moneyCoefficient;

    [Range(1, 100)]
    [SerializeField] public float _rotsheldMoneyCoefficient;

    public float Money => _money;
    public float RotsheldMoney => _rotsheldMoney;
    public float RotsheldMoneyCoefficient => _rotsheldMoneyCoefficient;

    public float MoneyCoefficient => _moneyCoefficient;

    public event UnityAction<float> MoneyChanged;
    public event UnityAction<float> RotsheldMoneyChanged;
    public event UnityAction<int> ActiveZonesCountChanged;

    private void OnEnable()
    {
        _buyZoneButtons = GetComponentsInChildren<BuyZone>();


        _playZones = GetComponentsInChildren<PlayZone>();

        foreach (var buyZoneButton in _buyZoneButtons)
        {
            buyZoneButton.gameObject.SetActive(false);
        }

        for (int i = 0; i < _playZones.Length; i++)
        {

            if (i > _activeZonesCount - 1)
            {
                _playZones[i].gameObject.SetActive(false);
            }
            else
            {
                _playZones[i].WorkCompleted += OnWorkCompleted;
            }
        }
        ActiveNextPlayZoneBuyButton();
    }

    private void Start()
    {
        MoneyChanged?.Invoke(_money);
        RotsheldMoneyChanged?.Invoke(_rotsheldMoney);
    }

    private void OnDisable()
    {
        foreach (var playZone in _playZones)
        {
            playZone.WorkCompleted -= OnWorkCompleted;
        }
    }

    public bool IsEnoughRotsheldMoney(float price)
    {
        return _rotsheldMoney >= price;
    }

    public bool IsEnoughMoney(float price)
    {
        return _money >= price;
    }

    private void OnWorkCompleted(PlayZone playzone)
    {
        _money += (playzone.WorkPayment * _moneyCoefficient);
        MoneyChanged?.Invoke(_money);
        float random = Random.Range(0f, 100f);
        if (random < playzone.RotsheldMoneyChance)
        {
            _rotsheldMoney += 1 * _rotsheldMoneyCoefficient;
            RotsheldMoneyChanged?.Invoke(_rotsheldMoney);
        }
    }

    public void OnUpdateMoneyMenager(Card card)
    {
        if (IsEnoughRotsheldMoney(card.Price))
        {
            _rotsheldMoney -= card.Price;
            _moneyCoefficient += card.SpecialCoeficient;
            RotsheldMoneyChanged?.Invoke(_rotsheldMoney);
        }
    }

    public void OnUpdateRotsheldMenager(Card card)
    {
        if (IsEnoughRotsheldMoney(card.Price))
        {
            _rotsheldMoney -= card.Price;
            _rotsheldMoneyCoefficient += card.SpecialCoeficient;
            RotsheldMoneyChanged?.Invoke(_rotsheldMoney);
        }
    }

    public void OnBoxOpen(Box box)
    {
        if (box.IsRotsheldsMoneyPrice)
        {
            _rotsheldMoney -= box.Price;
            _money += box.Prize;
        }
        else
        {
            _money -= box.Price;
            _rotsheldMoney += box.Prize;
        }

        MoneyChanged?.Invoke(_money);
        RotsheldMoneyChanged?.Invoke(_rotsheldMoney);
    }

    public bool IsPlayZoneItemUpdated(PlayZone playzone, string item)
    {
        bool isEnoughMoney = false;
        switch (item)
        {
            case "rock":
                if (IsEnoughMoney(playzone.Rock.UpgradePrice))
                {
                    _money -= playzone.Rock.UpgradePrice;
                    isEnoughMoney = true;
                }
                break;

            case "tree":

                if (IsEnoughMoney(playzone.AppleTree.UpgradePrice))
                {
                    _money -= playzone.AppleTree.UpgradePrice;
                    isEnoughMoney = true;
                }
                break;

            case "bed":

                if (IsEnoughMoney(playzone.Bed.UpgradePrice))
                {
                    _money -= playzone.Bed.UpgradePrice;
                    isEnoughMoney = true;
                }
                break;
        }
        MoneyChanged?.Invoke(_money);
        return isEnoughMoney;
    }

    public bool IsNewZoneBought(PlayZone playZone)
    {
        if (IsEnoughMoney(playZone.Price))
        {
            foreach (var zone in _playZones)
            {
                if (zone == playZone)
                {
                    _money -= zone.Price;
                    zone.gameObject.SetActive(true);
                    _activeZonesCount++;
                    ActiveNextPlayZoneBuyButton();
                    zone.WorkCompleted += OnWorkCompleted;
                    MoneyChanged?.Invoke(_money);

                }
            }
            return true;
        }
        else
        { return false; }
    }

    private void ActiveNextPlayZoneBuyButton()
    {
        if (_activeZonesCount < _buyZoneButtons.Length)
        {
            _buyZoneButtons[_activeZonesCount].gameObject.SetActive(true);
        }
        ActiveZonesCountChanged?.Invoke(_activeZonesCount);
    }

    public float AllActiveZonesWorkPayment()
    {
        float allPayment = 0f;
        List<PlayZone> activePlayzones = GetActivePlayZones();

        foreach (var zone in activePlayzones)
        {
            allPayment += zone.WorkPayment;
        }
        return allPayment;
    }

    public float AllActiveZonesRotsheldChance()
    {
        float allChances = 0f;
        List<PlayZone> activePlayzones = GetActivePlayZones();

        foreach (var zone in activePlayzones)
        {
            allChances += zone.RotsheldMoneyChance;
        }
        return allChances;
    }

    public List<PlayZone> GetActivePlayZones()
    {
        List<PlayZone> playZones = new List<PlayZone>(_activeZonesCount);
        for (int i = 0; i < _activeZonesCount; i++)
        {
            playZones.Add(_playZones[i]);
        }

        return playZones;
    }
}
