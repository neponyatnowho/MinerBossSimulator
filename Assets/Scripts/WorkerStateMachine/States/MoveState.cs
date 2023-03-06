using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MoveState : WorkerState
{
    private Transform _point;
    protected override void Enable()
    {
        Animator.SetTrigger("walking");
    }
    private void Update()
    {
            if (PlayZone.Worker.Satiety < 0.1f)
            {
                _point = PlayZone.AppleTree.NearestApple(transform.position);
            } 
            else if(PlayZone.Worker.Energy < 0.1f)
            {
                _point = PlayZone.Bed.BedEnterPoint.transform;
            }
            else if(PlayZone.Worker.Satiety > 0.1f && PlayZone.Worker.Energy > 0.1f)
            {
                _point = PlayZone.Rock.RockEnterPoint.transform;
            }

            IsMoving = true;
            Move(_point);
    }

    private void Move(Transform point)
    {
        Vector3 direction = point.position - transform.position;

        var rotation = Quaternion.LookRotation(direction);
        rotation.z = 0;
        rotation.x = 0;
        transform.rotation = rotation;

        direction.Normalize();

        transform.position = transform.position + (direction * PlayZone.Worker.WalkSpeed * Time.deltaTime);
    }

}
