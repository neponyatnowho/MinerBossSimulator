using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    [SerializeField] private int _appleCount;
    [SerializeField] private Apple _appleTemplate;
    [SerializeField] private Transform _appleContainer;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _randomizePositionValue;

    private Queue<Apple> _applesQueue = new Queue<Apple>();
    private List<Apple> _activeApples = new List<Apple>();
    private AppleSpawnPoint[] _spawnPoints;
    public List<Apple> ActiveApples => _activeApples;
    public Transform AppleCointainer => _appleContainer;

    private void Awake()
    {
        UpdateSpawnPoints();
    }

    private void Start()
    {
        for (int i = 0; i < _appleCount; i++)
        {
            Apple spawned = Instantiate(_appleTemplate, _appleContainer);
            _applesQueue.Enqueue(spawned);
            spawned.gameObject.SetActive(false);
        }

        StartCoroutine(SpawnApple());
    }

    public void AddAppleToQueue(Apple apple)
    {
        _applesQueue.Enqueue(apple);
        _activeApples.Remove(apple);
    }

    private IEnumerator SpawnApple()
    {
        while (true) 
        {
            yield return new WaitForSeconds(_spawnDelay);

            if (_applesQueue.Count > 0)
            {
                Vector3 spawnPosition = GetSpawnPosition();
                var spawned = _applesQueue.Dequeue();
                _activeApples.Add(spawned);
                spawned.gameObject.SetActive(true);
                spawned.transform.position = spawnPosition;
            }
        }
    }

    private Vector3 GetSpawnPosition()
    {
        int randomSpawner = Random.Range(0, _spawnPoints.Length);
        float randomZPosition = Random.Range(-_randomizePositionValue, _randomizePositionValue);
        Vector3 spawnPosition = _spawnPoints[randomSpawner].transform.position;
        spawnPosition.z += randomZPosition;
        spawnPosition.x += randomZPosition;
        return spawnPosition;
    }

    public void UpdateSpawnPoints()
    {
        _spawnPoints = GetComponentsInChildren<AppleSpawnPoint>();
    }
}
