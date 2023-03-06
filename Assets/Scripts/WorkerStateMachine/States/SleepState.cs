using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepState : WorkerState
{
    [SerializeField] private Transform _sleepCointainer;

    protected override void Enable()
    {
        IsSleeping = true;
        Animator.SetTrigger("sleeping");

        StartCoroutine(Sleep());
    }


    private IEnumerator Sleep()
    {
        transform.SetPositionAndRotation(_sleepCointainer.position, _sleepCointainer.rotation);

            yield return new WaitForSeconds(1.5f);
            PlayZone.Worker.TookSleep(PlayZone.Bed);
            IsSleeping = false;

    }
}
