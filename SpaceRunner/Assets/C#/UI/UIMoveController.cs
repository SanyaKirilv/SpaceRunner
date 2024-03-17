using System.Collections.Generic;
using UnityEngine;

public class UIMoveController : MonoBehaviour
{
    public List<UIMove> ObjectsToMove;
    public bool LoadOnStartup;

    private void Start() => Switch(LoadOnStartup);

    public void Switch(bool state)
    {
        foreach (var obj in ObjectsToMove)
        {
            if (state) obj.Enable();
            else obj.Disable();
        }
    }
}
