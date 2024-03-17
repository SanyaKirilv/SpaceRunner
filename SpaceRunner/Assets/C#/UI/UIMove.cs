using UnityEngine;

public class UIMove : MonoBehaviour
{
    public float Speed;

    private Vector2 _enablePosition;
    private Vector2 _disablePosition;
    private Vector2 _target;
    private bool _isMoving = false;
    private float _delay = 2f;

    private void Awake()
    {
        GetEnablePosition();
        GetDisablePosition();
        StartPosition();
    }

    public void MoveX(float _x) => ChangePosition(new(_x, _target.y));

    public void MoveY(float _y) => ChangePosition(new(_target.x, _y));

    public void Disable() => ChangePosition(_disablePosition);

    public void Enable() => ChangePosition(_enablePosition);

    private void ChangePosition(Vector2 nextPosition)
    {
        _target = nextPosition;
        _delay = 2.25f;
        _isMoving = true;
    }

    private void GetEnablePosition()
    {
        var anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        var xPos = anchoredPosition.x;
        var yPos = anchoredPosition.y;
        _enablePosition = new Vector2(xPos, yPos);
    }
    private void GetDisablePosition() =>
        _disablePosition = new(_enablePosition.x, _enablePosition.y + 4000);

    private void StartPosition() => GetComponent<RectTransform>().anchoredPosition = _disablePosition;

    private void FixedUpdate()
    {
        if (_delay > 0 && _isMoving)
        {
            _delay -= Time.fixedDeltaTime;
            GetComponent<RectTransform>().anchoredPosition =
                Vector2.Lerp(GetComponent<RectTransform>().anchoredPosition,
                _target, Time.fixedDeltaTime * Speed);
        }
        else
        {
            GetComponent<RectTransform>().anchoredPosition = _target;
            _isMoving = false;
        }
    }
}
