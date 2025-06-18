using System.Collections.Generic;
using UnityEngine;

public class BossUIManager : MonoBehaviour
{
    [SerializeField] GameObject BossUIPrefabs;
    [SerializeField] Transform uiParent;


    private Dictionary<BossAI, BossUI> uimap = new Dictionary<BossAI, BossUI>();
    private Queue<BossUI> uiPool = new Queue<BossUI>();
    Camera cam;

    EventBus eventBus;

    private void Awake()
    {
        cam = Camera.main;
        eventBus = FindAnyObjectByType<EventBus>();
    }

    private void OnEnable()
    {
        eventBus.OnBossHPChanged += OnBossHPChanged;
        eventBus.OnBossDied += OnBossDied;
    }

    private void OnDisable()
    {
        eventBus.OnBossHPChanged -= OnBossHPChanged;
        eventBus.OnBossDied -= OnBossDied;
    }

    private void OnBossHPChanged(BossAI boss)
    {
        if (!uimap.ContainsKey(boss))
        {
            Register(boss);
        }

        uimap[boss].UpdateHP(boss.curHP);

    }

    private void OnBossDied(BossAI boss)
    {
        if (uimap.TryGetValue(boss, out var ui))
        {
            ui.gameObject.SetActive(false);
            uiPool.Enqueue(ui);
            uimap.Remove(boss);
        }
    }

    public void Register(BossAI boss)
    {
        if (uimap.ContainsKey(boss)) return;

        BossUI ui = GetFromPool();
        ui.Bossinit(boss);
        uimap.Add(boss, ui);
    }

    private BossUI GetFromPool()
    {
        if (uiPool.Count > 0)
            return uiPool.Dequeue();
        else
            return Instantiate(BossUIPrefabs, uiParent).GetComponent<BossUI>();
    }

    private void Update()
    {
        foreach (var kvp in uimap)
        {
            kvp.Value.UpdatePosition(cam);
        }
    }

}
