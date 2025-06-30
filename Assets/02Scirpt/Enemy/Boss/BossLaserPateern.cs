using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossLaserPateern", menuName = "Scriptable Objects/BossLaserPateern")]
public class BossLaserPateern : BossPatternSO
{
    [SerializeField] GameObject LaserPrefab;
    private float fireRate = 2f;
    private int fireCount = 1;
    private Vector3 spawnOfset = new Vector3(0, 5, 0);

    public override void Execute(GameObject boss)
    {
        boss.GetComponent<MonoBehaviour>().StartCoroutine(FireLaser(boss.transform));
    }


    private IEnumerator FireLaser(Transform bossTransform)
    {
        
        Vector3 spawnPos = bossTransform.position + bossTransform.forward * 5f + spawnOfset;
        GameObject laser = Instantiate(LaserPrefab, spawnPos, bossTransform.rotation);
        Destroy(laser, 3f);
        
        yield return new WaitForSeconds(fireRate);
    }
}
