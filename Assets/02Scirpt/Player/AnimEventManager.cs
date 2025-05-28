using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[System.Serializable]
public class AnimationEventData
{
    public string animationName;
    public List<TimedEvent> events;
}

[System.Serializable]
public class TimedEvent
{
    public float time; // 타이밍 (초 단위)
    public string eventType; // 예: "Shoot", "Damage", "PlayEffect"
    public string param; // 투사체 ID, 이펙트 이름, 데미지량 등
}


public class AnimEventManager : MonoBehaviour
{
    [SerializeField]
    public List<AnimationEventData> allEventData; // 에디터에서 등록하거나 외부에서 로드

    private Dictionary<string, AnimationEventData> dataMap;

    private void Awake()
    {
        dataMap = allEventData.ToDictionary(x => x.animationName);
    }

    public List<TimedEvent> GetEventsForAnimation(string animationName)
    {
        if (dataMap.TryGetValue(animationName, out var data))
            return data.events;

        return null;
    }
}