using UnityEngine;

public class UIMove : MonoBehaviour {
    public Vector2 enablePosition;
    public Vector2 disablePosition;
    public Vector2 target;
    public bool loadOnStartup;
    public float speed;
    public AudioSource audioSource;
    public bool isMoving = false;
    private float delay = 2f;

    public void MoveX(float _x) {
        PlaySound(target.x, _x);
        ChangePosition(new(_x, target.y));
    }
    public void MoveY(float _y) {
        PlaySound(target.y, _y);
        ChangePosition(new(target.x, _y));
    }

    public void Disable() => ChangePosition(disablePosition);
    public void Enable() => ChangePosition(enablePosition);

    private void ChangePosition(Vector2 nextPosition) {
        target = nextPosition;
        delay = 2.25f;
        isMoving = true;
    }

    private void Awake() {
        GetEnablePosition();
        GetDisablePosition();
        StartupPosition();
    }
    private void PlaySound(float pos, float _pos) {
        if(pos != _pos && audioSource != null) 
            audioSource.Play();
    }
    private void GetEnablePosition() {
        var anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        var xPos = anchoredPosition.x;
        var yPos = anchoredPosition.y;
        enablePosition = new Vector2(xPos, yPos);
    }
    private void GetDisablePosition() => 
        disablePosition = new(enablePosition.x, enablePosition.y + 4000);

    private void StartupPosition() {
        GetComponent<RectTransform>().anchoredPosition = disablePosition;
        if(loadOnStartup) Enable();
        else Disable();
    }


    private void FixedUpdate() {
        if(delay > 0 && isMoving) {
            delay -= Time.fixedDeltaTime;
            GetComponent<RectTransform>().anchoredPosition = 
                Vector2.Lerp(GetComponent<RectTransform>().anchoredPosition, 
                target, Time.fixedDeltaTime * speed);
        }
        else {
            GetComponent<RectTransform>().anchoredPosition = target;
            isMoving = false;
        }
    }
}
