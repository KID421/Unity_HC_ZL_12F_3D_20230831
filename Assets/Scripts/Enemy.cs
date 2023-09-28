using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("移動速度"), Range(0, 10)]
    private float speed = 3f;
    [SerializeField, Header("攻擊冷卻"), Range(0, 10)]
    private float attackCD = 4.5f;
    [SerializeField, Header("攻擊區域")]
    private GameObject goAttackArea;
    [SerializeField, Header("啟動攻擊區域時間"), Range(0, 5)]
    private float showAttackAreaTime = 1.5f;
    [SerializeField, Header("啟動攻擊區域持續時間"), Range(0, 5)]
    private float showAttackAreaDurationTime = 0.5f;

    private NavMeshAgent agent;
    private Transform target;
    private Animator ani;
    private string parWalk = "開關走路";
    private string parAttack = "觸發攻擊";
    private bool canAttack = true; 

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        target = GameObject.Find("小黑").transform;

        ani = GetComponent<Animator>();

        // StartCoroutine(Test());
    }

    private void Update()
    {
        agent.SetDestination(target.position);

        // print($"<color=#f69>距離：{agent.remainingDistance}</color>");

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            ani.SetBool(parWalk, true);
        }
        else if (agent.remainingDistance != 0)
        {
            if (canAttack) StartCoroutine(AttackEffect());
        }
    }

    private IEnumerator AttackEffect()
    {
        canAttack = false;
        agent.isStopped = true;
        ani.SetTrigger(parAttack);

        Vector3 look = target.position;
        look.y = transform.position.y;
        transform.LookAt(look);

        yield return new WaitForSeconds(showAttackAreaTime);
        goAttackArea.SetActive(true);
        yield return new WaitForSeconds(showAttackAreaDurationTime);
        goAttackArea.SetActive(false);
        ani.SetBool(parWalk, false);
        yield return new WaitForSeconds(attackCD);
        canAttack = true;
        agent.isStopped = false;
    }

    private IEnumerator Test()
    {
        print("嗨，我是第一行");
        yield return new WaitForSeconds(1);
        print("第二行~");
        yield return new WaitForSeconds(2);
        print("第三行~");
    }
}
