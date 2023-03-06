using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private AppleSpawner spawner;
    private Rigidbody _rigidbody;

    public Rigidbody Rigidbody => _rigidbody;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        spawner = GetComponentInParent<AppleSpawner>();
    }
    public void OnEaten()
    {
        gameObject.SetActive(false);
        spawner.AddAppleToQueue(this);
    }
}
