using System;
using UnityEngine;

public class SwipeDetector : MonoBehaviour {
    public static Action<SwipeType> onSwiped;
    public bool detectSwipeAfterRelease = false;
	public float SWIPE_THRESHOLD = 20f;
	private Vector2 fingerDownPos;
	private Vector2 fingerUpPos;

	private void Update() {
		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) {
				fingerUpPos = touch.position;
				fingerDownPos = touch.position;
			}
			if (touch.phase == TouchPhase.Moved) {
				if (!detectSwipeAfterRelease) {
					fingerDownPos = touch.position;
					DetectSwipe ();
				}
			}
			if (touch.phase == TouchPhase.Ended) {
				fingerDownPos = touch.position;
				DetectSwipe ();
			}
		}
	}

    private float VerticalMoveValue() => Mathf.Abs(fingerDownPos.y - fingerUpPos.y);
	private float HorizontalMoveValue() => Mathf.Abs(fingerDownPos.x - fingerUpPos.x);

	private void DetectSwipe() {
		if (VerticalMoveValue() > SWIPE_THRESHOLD && VerticalMoveValue() > HorizontalMoveValue()) {
			if (fingerDownPos.y - fingerUpPos.y > 0)
				onSwiped?.Invoke(SwipeType.Up);
			else if (fingerDownPos.y - fingerUpPos.y < 0)
				onSwiped?.Invoke(SwipeType.Down);
			fingerUpPos = fingerDownPos;
        }
		else if (HorizontalMoveValue() > SWIPE_THRESHOLD && HorizontalMoveValue() > VerticalMoveValue()) {
			if (fingerDownPos.x - fingerUpPos.x > 0)
				onSwiped?.Invoke(SwipeType.Right);
			else if (fingerDownPos.x - fingerUpPos.x < 0)
				onSwiped?.Invoke(SwipeType.Left);
			fingerUpPos = fingerDownPos;
        }
	}
}
