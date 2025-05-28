using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimTable", menuName = "Scriptable Objects/AnimTable")]
public class AnimTable : ScriptableObject
{
    public string animationName; // Animator에서 사용되는 상태 이름
    public List<TimedEvent> events = new List<TimedEvent>();
}