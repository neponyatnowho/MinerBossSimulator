using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkTransition : WorkerTransition
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<RockEnterPoint>() && PlayZone.Worker.Satiety > 0.1f && PlayZone.Worker.Energy > 0.1f)
        {
            NeedTransit = true;
        }
    }
}
