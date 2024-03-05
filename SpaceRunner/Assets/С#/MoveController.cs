using System.Collections;
using UnityEngine;

public class MoveController : MonoBehaviour {

    public Transform player;
    public Transform level;
    public float nextRotation;
    private void OnEnable() => SwipeDetector.onSwiped += ActionHandler;
    private void OnDisable() => SwipeDetector.onSwiped -= ActionHandler;

    private void ActionHandler(SwipeType type)
    {
        switch (type)
        {
            case SwipeType.Up:
                MoveUp();
                break;
            case SwipeType.Left:
                MoveLeft();
                break;
            case SwipeType.Right:
                MoveRight();
                break;
        }
    }

    private void MoveUp() => StartCoroutine(MoveLerp(3.5f, 0.25f));

    private void MoveLeft() => StartCoroutine(RotateLerp(-90, 0.5f));

    private void MoveRight() => StartCoroutine(RotateLerp(90, 0.5f));

    public IEnumerator MoveLerp(float moveValue, float duration)
    {
        var startPosition = player.position;
        var endPosition = new Vector3(0, startPosition.y + moveValue, 0);
        for(float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            player.position = Vector3.Lerp(startPosition, endPosition, timer / duration);
            yield return null;
        }
        player.position = endPosition;
    }

    IEnumerator RotateLerp(float rotateValue, float duration)
    {
        nextRotation += rotateValue;
        var startRotation = level.rotation;
        var endRotation = Quaternion.Euler(0, 0, nextRotation);
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            level.rotation = Quaternion.Slerp(startRotation, endRotation, timer / duration);
            yield return null;
        }
        level.rotation = endRotation;
    }
}
