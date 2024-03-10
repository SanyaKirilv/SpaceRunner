using System.Collections;
using UnityEngine;

public class MoveController : MonoBehaviour {

    public Transform player;
    public Transform pipes;
    public float nextRotation;
    public bool onGround;
    private void OnEnable() => SwipeDetector.onSwiped += ActionHandler;
    private void OnDisable() => SwipeDetector.onSwiped -= ActionHandler;

    private void ActionHandler(SwipeType type)
    {
        switch (type)
        {
            case SwipeType.Up:
                if(onGround) {
                    MoveUp();
                    onGround = false;
                }
                break;
            case SwipeType.Left:
                MoveLeft();
                break;
            case SwipeType.Right:
                MoveRight();
                break;
        }
    }

    private void MoveUp() => StartCoroutine(MoveLerp(6f, 0.5f));

    private void MoveLeft() => StartCoroutine(RotateLerp(-60, 0.5f));

    private void MoveRight() => StartCoroutine(RotateLerp(60, 0.5f));

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
        onGround = true;
    }

    IEnumerator RotateLerp(float rotateValue, float duration)
    {
        nextRotation += rotateValue;
        var startRotation = pipes.rotation;
        var endRotation = Quaternion.Euler(0, 0, nextRotation);
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            pipes.rotation = Quaternion.Slerp(startRotation, endRotation, timer / duration);
            yield return null;
        }
        pipes.rotation = endRotation;
    }
}
