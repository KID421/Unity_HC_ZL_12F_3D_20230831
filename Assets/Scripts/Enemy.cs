using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("移動速度"), Range(0, 10)]
    private float speed = 3f;
    [SerializeField, Header("攻擊冷卻"), Range(0, 10)]
    private float attackCD = 4.5f;

    private NavMeshAgent agent;
    private Transform target;
    private Animator ani;
    private string parWalk = "開關走路";
    private string parAttack = "觸發攻擊";

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        target = GameObject.Find("小黑").transform;

        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        agent.SetDestination(target.position);

        print($"<color=#f69>距離：{agent.remainingDistance}</color>");

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            ani.SetBool(parWalk, true);
        }
        else if (agent.remainingDistance != 0)
        {
            ani.SetTrigger(parAttack);
            agent.isStopped = true;
        }
    }
}
