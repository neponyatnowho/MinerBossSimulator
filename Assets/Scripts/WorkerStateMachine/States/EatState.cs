using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatState : WorkerState
{
    protected override void Enable()
    {
        IsEating = true;
        Animator.SetTrigger("eating");
        StartCoroutine(Eat());
    }
    private IEnumerator Eat()
    {
            yield return new WaitForSeconds(1f);
            PlayZone.Worker.HadLunch(PlayZone.AppleTree);
            IsEating = false;
    }
}
