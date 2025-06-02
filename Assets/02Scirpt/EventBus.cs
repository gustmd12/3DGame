using System;
using UnityEngine;

public class EventBus : MonoBehaviour
{
    public static Action OnSkillStart;
    public static Action OnSkillEnd;
    public Action OnManaChanged;
    public Action OnHPChanged;

    public static void SkillCastStart() => OnSkillStart?.Invoke();
    public static void SkillCastEnd() => OnSkillEnd?.Invoke();

    public void ManaChanged() => OnManaChanged?.Invoke();
    public void HPChanged() => OnHPChanged?.Invoke();
}
