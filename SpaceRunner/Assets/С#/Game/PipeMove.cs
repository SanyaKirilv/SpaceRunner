using UnityEngine;

public class PipeMove : MonoBehaviour 
{
    public int speed;
    
    private void FixedUpdate() => transform.position -= new Vector3(0, 0, 1 * speed * Time.fixedDeltaTime);
}
