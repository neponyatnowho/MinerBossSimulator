using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _smothing;

    [Header("Movement")]
    [SerializeField] private float _MovementSpeed;
    [SerializeField] private Vector2Int _minMaxX;
    [SerializeField] private Vector2Int _minMaxZ;

    [Header("Zooming")]
    [SerializeField] private float _ZoomSpeed;
    [SerializeField] private Vector2Int _zoomMinMax;
    [SerializeField] private float _zoomZOffset;
    private float _zoom;



    private Vector3 _lastCameraPosition;
    private Vector3 _lastTouchPosition;

    private Vector3 _nextPosition;
    private bool _isOnUI;

    private void Start()
    {
        _zoom = 0f;
        _nextPosition = transform.position;
        _lastCameraPosition = transform.position;
    }

    private void Update()
    {

        if (Input.touchCount > 0)
        {

            if (Input.touchCount == 1)
            {

                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (isPointerOverUIObject())
                        {
                            _isOnUI = true;
                            break;
                        }
                        else
                        {
                            _isOnUI = false;
                        }
                        _lastCameraPosition = transform.position;
                        _lastTouchPosition = touch.position;

                        break;

                    case TouchPhase.Moved:
                        if(!_isOnUI)
                        {
                            Vector3 touchDirection = new Vector3(touch.position.x - _lastTouchPosition.x, 0f, touch.position.y - _lastTouchPosition.y);

                            float Offset = Mathf.InverseLerp(_zoomMinMax.x, _zoomMinMax.y, transform.position.y);
                            Offset = Mathf.Clamp(Offset, 0.5f, 1f);

                            Vector3 newPosition = _lastCameraPosition - touchDirection * _MovementSpeed * Offset;
                            newPosition = GetClampedPosition(newPosition);
                        }
                        break;

                    case TouchPhase.Ended:
                        if (!_isOnUI)
                            _lastCameraPosition = transform.position;
                        break;
                }
            }
                

            if (Input.touchCount == 2)
            {
                if (isPointerOverUIObject())
                {
                    _isOnUI = true;
                }
                else
                {
                    _isOnUI = false;
                }

                Touch touch1 = Input.touches[0];
                Touch touch2 = Input.touches[1];

                switch (touch2.phase)
                {
                    case TouchPhase.Began:
                        _lastCameraPosition = transform.position;
                        _lastTouchPosition = touch2.position;

                        break;

                    case TouchPhase.Moved:
                        Vector2 Touch1PrewPos = touch1.position - touch1.deltaPosition;
                        Vector2 Touch2PrewPos = touch2.position - touch2.deltaPosition;

                        float prevTouchDeltaMag = (Touch1PrewPos - Touch2PrewPos).magnitude;
                        float touchDeltaMag = (touch1.position - touch2.position).magnitude;

                        float DeltaMagDif = prevTouchDeltaMag - touchDeltaMag;

                        _zoom = -DeltaMagDif * _ZoomSpeed;

                        Vector3 newPosition = transform.position + (transform.forward * _zoom);
                        
                        if(newPosition.y >= _zoomMinMax.y || newPosition.y <= _zoomMinMax.x)
                            break;
                        


                        newPosition = GetClampedPosition(newPosition);
                        _lastCameraPosition = transform.position;

                        break;

                    case TouchPhase.Ended:
                        _lastTouchPosition = touch1.position;

                        break;
                }

                if(touch1.phase == TouchPhase.Ended)
                {
                    _lastTouchPosition = touch2.position;
                }
            }
            if(!_isOnUI)
            {
                transform.position -= (transform.position - _nextPosition) * Time.deltaTime * _smothing;

            }
        }
        else if(!_isOnUI)
        {
            transform.position -= (transform.position - _nextPosition) * Time.deltaTime * (_smothing / 5f);
        }
    }

    private Vector3 GetClampedPosition(Vector3 newPosition)
    {
        float Offset = Mathf.InverseLerp(_zoomMinMax.y, _zoomMinMax.x, transform.position.y) * _zoomZOffset;
        newPosition.x = Mathf.Clamp(newPosition.x, _minMaxX.x, _minMaxX.y);
        newPosition.y = Mathf.Clamp(newPosition.y, _zoomMinMax.x, _zoomMinMax.y);
        newPosition.z = Mathf.Clamp(newPosition.z, _minMaxZ.x, _minMaxZ.y + Offset);
        _nextPosition = newPosition;
        return newPosition;
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
