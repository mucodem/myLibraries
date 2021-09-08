using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCLeader : MonoBehaviour
{
    public enum LeaderMoveType
    {
        Stroll, RunAwayOrChase
    }

    [Header("Needed values")]
    [SerializeField] Transform player;
    [SerializeField] float multiplier;
    [SerializeField] float range = 30f;

    [Header("References")]
    Spawner spawner;
    NavMeshAgent agent;
    LeaderMoveType moveType;
    Leader leader;
    [SerializeField] UIManager uIManager;

    void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        agent = GetComponent<NavMeshAgent>();
        leader = GetComponent<Leader>();
        StartCoroutine(StrollAround());
    }

    void Update()
    {
        switch (moveType)
        {
            case LeaderMoveType.Stroll:
                CheckAndReStroll();
                break;
            case LeaderMoveType.RunAwayOrChase:
                Chechker();
                break;
        }
    }

    IEnumerator StrollAround() // set random target to go
    {
        yield return new WaitForEndOfFrame();
        agent.SetDestination(spawner.spawnPoint[Random.Range(0, spawner.spawnPoint.Length)].position); // set a random location to go for agent
    }

    void CheckAndReStroll() // if reached to target , set a new target
    {
        if (agent.remainingDistance <= 1f)
        {
            //Debug.Log("REMAINING DISTANCE IS LESS THEN 1");
            agent.SetDestination(spawner.spawnPoint[Random.Range(0, spawner.spawnPoint.Length)].position); // set a random location to go for agent
        }
    }

    void Chechker()
    {
        Vector3 runTo = transform.position + ((transform.position - player.position) * multiplier);
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < range) 
        {
            if(leader.follower.Count > player.GetComponent<Leader>().follower.Count)
            {
                agent.SetDestination(-runTo); // chase
            }
            else
            {
                agent.SetDestination(runTo); // run away
            }
        }     
    }
}
