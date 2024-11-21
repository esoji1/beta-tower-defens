using TMPro;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField] private Transform _cameraPositionBorders;

    private Vector3 _lastDragPosition;
    private float _dragModifier = 24f;
    private float _scrollOffset = 30f;
    private float _scrollModifier = 16f;

    private void LateUpdate()
    {
        bool isLeftMouseButtonPressed = Drag();

        if (!isLeftMouseButtonPressed)
        {
            Scroll();
        }

        KeepCameraRightPosition();
    }

    private bool Drag()
    {
        const int LeftButtonIndex = 0;

        bool isLeftMouseButtonPressed = false;

        if (Input.GetMouseButtonDown(LeftButtonIndex))
        {
            _lastDragPosition = Input.mousePosition;
            isLeftMouseButtonPressed = true;
        }

        if (Input.GetMouseButton(LeftButtonIndex))
        {
            transform.Translate((_lastDragPosition - Input.mousePosition) * _dragModifier * Time.deltaTime);
            _lastDragPosition = Input.mousePosition;
            isLeftMouseButtonPressed = true;
        }

        return isLeftMouseButtonPressed;
    }

    private void Scroll()
    {
        Vector3 offset = Vector3.zero;

        if (Input.mousePosition.x < _scrollOffset)
        {
            offset += Vector3.left; 
        }
        else if (Input.mousePosition.x > (Screen.width - _scrollOffset))
        {
            offset += Vector3.right;
        }

        if (Input.mousePosition.y < _scrollOffset)
        {
            offset += Vector3.down;
        }
        else if (Input.mousePosition.y > (Screen.height - _scrollOffset))
        {
            offset += Vector3.up;
        }

        transform.Translate(offset * _scrollModifier * Time.deltaTime);
    }

    private void KeepCameraRightPosition()
    {
        float left = _cameraPositionBorders.position.x - _cameraPositionBorders.localScale.x / 2f;
        float right = _cameraPositionBorders.position.x + _cameraPositionBorders.localScale.x / 2f;
        float top = _cameraPositionBorders.position.y - _cameraPositionBorders.localScale.y / 2f;
        float bottom = _cameraPositionBorders.position.y + _cameraPositionBorders.localScale.y / 2f;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, left, right),
                                         Mathf.Clamp(transform.position.y, top, bottom),
                                         transform.position.z);
    }
}
