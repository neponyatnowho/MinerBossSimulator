using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepTransition : WorkerTransition
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BedEnterPoint>() && PlayZone.Worker.Energy < 0.1f)
            NeedTransit = true;

    }
}
