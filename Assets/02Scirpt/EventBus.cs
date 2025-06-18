using System;
using UnityEngine;

public class EventBus : MonoBehaviour
{
    public static Action OnSkillStart;
    public static Action OnSkillEnd;
    public Action OnManaChanged;
    public Action OnHPChanged;

    public Action<Enemy> OnEnemyHPChanged;
    public Action<Enemy> OnEnemyDied;

    public Action<BossAI> OnBossHPChanged;
    public Action<BossAI> OnBossDied;

    public static void SkillCastStart() => OnSkillStart?.Invoke();
    public static void SkillCastEnd() => OnSkillEnd?.Invoke();

    public void ManaChanged() => OnManaChanged?.Invoke();
    public void HPChanged() => OnHPChanged?.Invoke();

    public void RaiseEnemyHPChanged(Enemy enemy) => OnEnemyHPChanged?.Invoke(enemy);
    public void RaiseEnemyDied(Enemy enemy) => OnEnemyDied?.Invoke(enemy);

    public void BossHPChanged(BossAI boss) => OnBossHPChanged?.Invoke(boss);

    public void BossDied(BossAI boss) => OnBossDied?.Invoke(boss);


}
