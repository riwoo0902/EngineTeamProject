using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestBoss : MonoBehaviour
{
    [field: SerializeField]
    private BossSO bossData;

    private float currentHp;
    private bool isPhaseTwo = false;
    private bool isAttack = false;

    [SerializeField] private Transform playerTransform;

    private void Start()
    {
        currentHp = bossData.maxHP;
        StartCoroutine(BossPhaseOneRoutine());
    }

    private IEnumerator BossPhaseOneRoutine()
    {
        while (currentHp > 0)
        {
            if (!isAttack)
            {
                isAttack = true;

                if (!isPhaseTwo && currentHp <= bossData.maxHP * bossData.phase2HP)
                {
                    isPhaseTwo = true;
                    Debug.Log("2 진입");
                }

                int patternType = Random.Range(0,2);
                if (patternType == 0)
                    yield return StartCoroutine(DoAttackPattern());
                else
                    yield return StartCoroutine(DoSkillPattern());

                float waitTime = isPhaseTwo ? bossData.attackCoolTime * 0.7f : bossData.attackCoolTime;
                yield return new WaitForSeconds(waitTime);

                isAttack = false;

            }
        }

        BossDie();
    }


    IEnumerator DoAttackPattern()
    {
        yield return new WaitForSeconds(Random.Range(0.2f, 0.6f));

        int index = Random.Range(0, bossData.attackPrefabs.Count);
        GameObject attackFx = bossData.attackPrefabs[index];

      
        Vector3 dir = (playerTransform.position.x > transform.position.x) ? Vector3.right : Vector3.left;
        Vector3 spawnPos = transform.position + dir * Random.Range(1.5f, 3f);

        Instantiate(attackFx, spawnPos, Quaternion.identity);

        Debug.Log("일반공격");

        yield return new WaitForSeconds(Random.Range(0.4f, 0.8f));
    }

    IEnumerator DoSkillPattern()
    {
        yield return new WaitForSeconds(Random.Range(0.3f, 0.7f));

        int index = Random.Range(0, bossData.skillPrefabs.Count);
        GameObject skillFx = bossData.skillPrefabs[index];


        Vector3 dir = (playerTransform.position.x > transform.position.x) ? Vector3.right : Vector3.left;
        Vector3 spawnPos = transform.position + dir * Random.Range(1.5f, 3f);

        Instantiate(skillFx, spawnPos, Quaternion.identity);

        Debug.Log("스킬공격");

        yield return new WaitForSeconds(Random.Range(0.4f, 0.8f));
    }


    private void BossDie()
    {
        Debug.Log($"사망");
        Destroy(gameObject);
    }
}
