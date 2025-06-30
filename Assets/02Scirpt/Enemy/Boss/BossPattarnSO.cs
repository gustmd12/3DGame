using UnityEngine;

[CreateAssetMenu(fileName = "BossPatarnSO", menuName = "Scriptable Objects/BossPatarnSO")]
public abstract class BossPatternSO : ScriptableObject
{
    public string pattternName = "LaserPattern";
    public float delayBeforExecute = 1f;

    public abstract void Execute(GameObject boss);

}
