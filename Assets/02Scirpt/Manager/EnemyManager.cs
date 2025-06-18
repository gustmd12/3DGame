//using NUnit.Framework;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    [SerializeField] GameObject Boss;

    EventBus eventBus;

    private void Awake()
    {
        eventBus = FindAnyObjectByType<EventBus>();

        Boss.SetActive(false);

        Debug.Log($"몬스터의 수 {enemies.Count}");
    }
    

    private void Start()
    {
        eventBus.OnEnemyDied += HandleEnemyDied;
    }

    private void OnDestroy()
    {
        eventBus.OnEnemyDied -= HandleEnemyDied;
    }

    private void HandleEnemyDied(Enemy enemy)
    {
        
        if(enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }

        if(enemies.Count == 0)
        {
            Debug.Log("모든 몬스터 처치");
            Boss.SetActive(true);
        }
    }
}
