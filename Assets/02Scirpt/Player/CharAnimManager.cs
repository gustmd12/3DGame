using System.Collections.Generic;
using UnityEngine;

public class CharAnimManager : MonoBehaviour
{
    [SerializeField]
    private AnimEventManager eventManager;

    private Animator animator;
    private List<TimedEvent> currentEvents;
    private int currentIndex;
    private float animElapsedTime;

    private string currentAnimStateName;
    private bool isPlaying;

    SkillManager skillManager;
    AnimatorController animatorController;
    private void Awake()
    {
        skillManager = FindAnyObjectByType<SkillManager>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        animatorController = GetComponent<AnimatorController>();
    }

    public void PlayAnimation(int animID)
    {
        //animator.Play(animName);
        animator.SetInteger("animation", animID);
        currentAnimStateName = $"animation{animID.ToString()}";
        currentEvents = eventManager.GetEventsForAnimation(animID.ToString());

        //Debug.Log($"Count : {currentEvents.Count} ");
        animElapsedTime = 0f;
        currentIndex = 0;
        isPlaying = true;
    }

    public void PlayerMove(bool isMoving)
    {
        animator.SetBool("Move", isMoving);
        if (isMoving)
        {
            if(!isPlaying)
            {
                animator.SetInteger("animation", 1);
            }
        }
    }

    private void Update()
    {
        if (!isPlaying || currentEvents == null) return;

        animElapsedTime += Time.deltaTime;

        while (currentIndex < currentEvents.Count && currentEvents[currentIndex].time <= animElapsedTime)
        {
            ExecuteTimedEvent(currentEvents[currentIndex]);
            currentIndex++;
        }
    }

    private void ExecuteTimedEvent(TimedEvent evt)
    {
        //Debug.Log($"[타이밍 이벤트] {evt.eventType} at {evt.time}s | param: {evt.param}");

        switch (evt.eventType)
        {
            case "Shoot":
                break;
            case "Damage":
                break;
            case "Effect":
                break;
            case "RSkill":
                skillManager.TryCastSkill(3);
                break;
            case "Attack":
                break;
            case "AnimFinish":
                animator.SetInteger("animation", 1);
                isPlaying = false;
                break;
        }
    }
}