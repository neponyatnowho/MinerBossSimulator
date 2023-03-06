using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatTransition : WorkerTransition
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Apple>(out Apple apple) && PlayZone.Worker.Satiety < 0.1f)
        {
            NeedTransit = true;
            apple.OnEaten();
        }

    }
}
