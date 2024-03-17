using UnityEngine;

public class PipeMove : MonoBehaviour
{
    public int speed;

    private void Start() => transform.Rotate(0, 0, Random.Range(0, 8) * 45 + 22.5f);

    private void FixedUpdate() => transform.position -= new Vector3(0, 0, 1 * speed * Time.fixedDeltaTime);
}
