using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [SerializeField] private GameObject[] _treesModels;

    [SerializeField] private int _level;
    [SerializeField] private float _satiety;
    [SerializeField] private AppleSpawner _spawner;
    [SerializeField] private List<Apple> _apples;
    [SerializeField] private float _upgradePrice;

    public int Level => _level;
    public float Satiety => _satiety;
    public float UpgradePrice => _upgradePrice;

    private void OnEnable()
    {
        _spawner = GetComponent<AppleSpawner>();
        SetActiveModel();
        _apples = _spawner.ActiveApples;
    }

    public Transform NearestApple(Vector3 position)
    {
        if(_apples == null)
            return null;

        Apple nearestApple = null;
        Vector3 nearestPosition;
        foreach (Apple apple in _apples)
        {
            if(nearestApple == null)
            {
               nearestApple = apple;
            }
            nearestPosition = nearestApple.transform.position;
            float nearestDistance = Vector3.Distance(position, nearestPosition);
            float currentDistance = Vector3.Distance(position, apple.transform.position);

            if(currentDistance < nearestDistance)
                nearestApple = apple;
        }
        return nearestApple.transform;
    }

    public void UpdateInfo()
    {
        _level++;
        if (_level % 10 == 0)
        {
            _upgradePrice *= 2f;
            _satiety += 0.1f;
            SetActiveModel();
            return;
        }
        _upgradePrice *= 1.2f;
        _satiety += 0.01f;
    }

    private void SetActiveModel()
    {
        var index = _level / 10;
        if (index > _treesModels.Length - 1)
            return;

        for (int i = 0; i <= _treesModels.Length - 1; i++)
        {
            if (i == index)
            {
                _treesModels[i].SetActive(true);
                _spawner.UpdateSpawnPoints();
            }
            else
            {
                _treesModels[i].SetActive(false);
            }
        }
    }
}
