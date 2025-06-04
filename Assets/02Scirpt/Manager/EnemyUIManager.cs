using System.Collections.Generic;
using UnityEngine;

public class EnemyUIManager : MonoBehaviour
{
    [SerializeField] GameObject enemyUIPrefabs;
    [SerializeField] Transform uiParent;


    private Dictionary<Enemy, EnemyUI> uimap = new Dictionary<Enemy, EnemyUI>();
    private Queue<EnemyUI> uiPool = new Queue<EnemyUI>();
    Camera cam;

    EventBus eventBus;

    private void Awake()
    {
        cam = Camera.main;
        eventBus = FindAnyObjectByType<EventBus>();
    }

    private void OnEnable()
    {
        eventBus.OnEnemyHPChanged += OnEnemyHPChanged;
        eventBus.OnEnemyDied += OnEnemyDied;
    }

    private void OnDisable()
    {
        eventBus.OnEnemyHPChanged -= OnEnemyHPChanged;
        eventBus.OnEnemyDied -= OnEnemyDied;
    }

    private void OnEnemyHPChanged(Enemy enemy)
    {
        if(!uimap.ContainsKey(enemy))
        {
            Register(enemy);
        }

        uimap[enemy].UpdateHP(enemy.curHP);
    }

    private void OnEnemyDied(Enemy enemy)
    {
        if(uimap.TryGetValue(enemy, out var ui))
        {
            ui.gameObject.SetActive(false);
            uiPool.Enqueue(ui);
            uimap.Remove(enemy);
        }
    }

    public void Register(Enemy enemy)
    {
        if (uimap.ContainsKey(enemy)) return;

        EnemyUI ui = GetFromPool();
        ui.init(enemy);
        uimap.Add(enemy, ui);
    }

    private EnemyUI GetFromPool()
    {
        if(uiPool.Count > 0)
            return uiPool.Dequeue();
        else
            return Instantiate(enemyUIPrefabs, uiParent).GetComponent<EnemyUI>();
    }

    private void Update()
    {
        foreach(var kvp in uimap)
        {
            kvp.Value.UpdatePosition(cam);
        }
    }
}
