using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    enum State
    {
        Alive,
        Dead,
        Patrolling,
        Chasing,
        Searching,
        Attacking,
        Retreating
    }

    private State state;


    public NavMeshAgent enemy;
    private Vector3 lastPlayerLocation;
    private Vector3 playerLocation;
    private Vector3 enemyLocation;
    public GameObject player;

    public Transform[] points;
    public int patrolDestinationPoint;
    public int patrolDestinationAmount;
    public float remaingDistance = 0.5f;
    public int viewDistance = 5;

    public float targetTime = 4.0f;

    void Start()
    {
        
        enemy = GetComponent<NavMeshAgent>();
        patrolDestinationPoint = 0;
        SwitchState(State.Patrolling);

    }

    void SwitchState(State newState)
    {
        state = newState;
    }

    // Update is called once per frame
    void Patrolling()
    {
        playerLocation = player.gameObject.transform.position;
        enemy.SetDestination(points[patrolDestinationPoint].position);

        if ((enemyLocation.x == points[patrolDestinationPoint].position.x) && (enemyLocation.z == points[patrolDestinationPoint].position.z))
        {
            patrolDestinationPoint++;

            if (patrolDestinationPoint >= patrolDestinationAmount)
            {
                patrolDestinationPoint = 0;
            }
        }
    }

    void Chasing()
    {

        enemy.SetDestination(playerLocation);




    }

    void Searching()
    {
        enemy.SetDestination(lastPlayerLocation);

        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            SwitchState(State.Retreating);
        }


    }

    void Retreating()
    {

        if ((enemyLocation.x == points[4].position.x) && (enemyLocation.z == points[4].position.z))
        {
            SwitchState(State.Patrolling);
        }
        enemy.SetDestination(points[4].position);

    }

    void Attacking()
    {
        enemy.SetDestination(enemyLocation);
    }

    void Update()
    {
        

        enemyLocation = enemy.transform.position;
        playerLocation = player.gameObject.transform.position;
        float distance = Vector3.Distance(playerLocation, enemyLocation);
        

        if (distance <= viewDistance && distance > 2)
        {
           SwitchState(State.Chasing);
           
        }

        if (distance <= 2)
        {
            SwitchState(State.Attacking);
        }

        switch (state)
        {
            case State.Alive:
                Debug.Log("State: Alive");
                break;

            case State.Dead:
                Debug.Log("State: Dead");
                break;

            case State.Patrolling:
                Debug.Log("State: Patrolling");
                Patrolling();
                break;
            case State.Chasing:
                Debug.Log("State: Chasing");
                Chasing();
                if (distance >= viewDistance)
                {
                    lastPlayerLocation = player.gameObject.transform.position;
                    SwitchState(State.Searching);
                }
                /*if (distance <= 2)
                {
                    SwitchState(State.Attacking);
                }*/
                break;
            case State.Searching:
                Debug.Log("State: Searching");
                
                Searching();
                break;
            case State.Retreating:
                Debug.Log("State: Retreating");
                Retreating();
                break;
            case State.Attacking:
                Debug.Log("State: Attakcing");
                Attacking();
                break;

        }
    }


}
