using System.Collections;
using UnityEngine;

public class MoveController : MonoBehaviour
{

    public Transform player;
    public Transform pipeParent;
    private float nextRotation;
    private bool onGround;

    private void OnEnable()
    {
        Obstacle.onTouched += ActionHandler;
        SwipeDetector.onSwiped += ActionHandler;
    }

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

    private void ActionHandler(ObstacleType type)
    {
        if (type == ObstacleType.Ground)
            onGround = true;
    }

    private void MoveUp()
    {
        if (onGround)
        {
            onGround = false;
            StartCoroutine(MoveLerp(12.5f, 0.125f));
        }
    }

    private void MoveLeft() => StartCoroutine(RotateLerp(-45, 0.125f));

    private void MoveRight() => StartCoroutine(RotateLerp(45, 0.125f));

    public IEnumerator MoveLerp(float moveValue, float duration)
    {
        var startPosition = player.position;
        var endPosition = new Vector3(0, startPosition.y + moveValue, 0);
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            player.position = Vector3.Lerp(startPosition, endPosition, timer / duration);
            yield return null;
        }
        player.position = endPosition;
    }

    private IEnumerator RotateLerp(float rotateValue, float duration)
    {
        nextRotation += rotateValue;
        var startRotation = pipeParent.rotation;
        var endRotation = Quaternion.Euler(0, 0, nextRotation + 22.5f);
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            pipeParent.rotation = Quaternion.Slerp(startRotation, endRotation, timer / duration);
            yield return null;
        }
        pipeParent.rotation = endRotation;
    }
}
