using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWarriorPointPosition : MonoBehaviour
{
    [SerializeField] private Button _buttonPermissionMove;
    [SerializeField] private MelleTowerConfig _config;

    private bool _isClick = false;
    private int _leftMouseButton = 0;
    private MelleTower _melleTower;
    private float _timeBeforeStartMovement = 0.2f;

    public bool IsClick => _isClick;

    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null && hit.collider.TryGetComponent(out MelleTower melleTower))
            _melleTower = melleTower;

        if (_isClick && Input.GetMouseButtonDown(_leftMouseButton))
            StartCoroutine(MoveWarriorsOnClick());
    }

    private void OnEnable()
    {
        _buttonPermissionMove.onClick.AddListener(PermissionMove);
    }

    private void OnDisable()
    {
        _buttonPermissionMove.onClick.RemoveListener(PermissionMove);
    }

    private void PermissionMove()
    {
        _isClick = true;
    }

    private IEnumerator MoveWarriorsOnClick()
    {
        Warrior[] warriors = _melleTower.GetComponentsInChildren<Warrior>();

        MovePoint();
        
        int count = Mathf.Min(warriors.Length, _melleTower.PositionPoints.Length);

        for (int i = 0; i < count; i++)
        {
            if (_melleTower.PositionPoints[i] != null)
                warriors[i].SetTarget(_melleTower.PositionPoints[i].transform);

            yield return new WaitForSeconds(_timeBeforeStartMovement);
            _isClick = false;
        }
     
        yield return null;
    }

    private void MovePoint()
    {
        Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 towerPosition = _melleTower.transform.position;

        if (Vector2.Distance(towerPosition, targetPosition) > _config.Radius)
        {
            Vector2 direction = (targetPosition - towerPosition).normalized;
            targetPosition = towerPosition + direction * _config.Radius;
        }

        _melleTower.WarriorPositionPoints.transform.position = targetPosition;
    }
}