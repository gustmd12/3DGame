using UnityEngine;

public class Targetable : MonoBehaviour
{
    

    public Vector3 GetTargetPoint()
    {
        return transform.position;
    }
    
    public void OnTargeted()
    {
        Debug.Log(gameObject.name + "target");
    }
}
