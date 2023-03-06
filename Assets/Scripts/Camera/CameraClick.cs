using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;

public class CameraClick : MonoBehaviour
{
    private PlayZone _selectedZone;
    private bool _isWordClick;
    public event UnityAction<PlayZone> OnPlayZoneClick;
    private void LateUpdate()
    {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:

                        if(!isPointerOverUIObject())
                            _isWordClick = true;
                        else
                            _isWordClick = false;

                        break;

                    case TouchPhase.Moved:

                        _isWordClick = false;
                        break;

                    case TouchPhase.Ended:
                        if (_isWordClick)
                        {
                            GameObject clicked = getClickedObject(out RaycastHit hit);
                            if (clicked != null && clicked.TryGetComponent<PlayZone>(out PlayZone playzone))
                            {
                                _selectedZone = playzone;
                                OnPlayZoneClick?.Invoke(_selectedZone);
                            }
                        }
                        break;
                }
            }
    }

    GameObject getClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        if (Physics.Raycast (ray.origin, ray.direction * 10, out hit))
        {
            if(!isPointerOverUIObject())
            {
                target = hit.collider.gameObject;
            }
        }
        return target;
    }
    private bool isPointerOverUIObject()
    {
        
        PointerEventData ped = new PointerEventData(EventSystem.current);
        ped.position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(ped, results);
        return results.Count > 0;
    }
}

