using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkState : WorkerState
{
    protected override void Enable()
    {
        IsWorking = true;
        StartCoroutine(Work());
    }

    private IEnumerator Work()
    {
        Animator.SetTrigger("working");
        PlayZone.Worker.GetPickaxe();
        while (true)
        {
            yield return new WaitForSeconds(1f);
            PlayZone.WorkIsComplete();
        }
    }
    private void OnDisable()
    {
        PlayZone.Worker.HidePickaxe();
    }
}
