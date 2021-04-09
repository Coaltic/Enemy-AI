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

    public Animator animator;

    public NavMeshAgent enemy;
    private Vector3 lastPlayerLocation;
    private Vector3 playerLocation;
    private Vector3 enemyLocation;
    public GameObject player;

    public Transform[] points;
    public int patrolDestinationPoint;
    public int patrolDestinationAmount;
    //public float remaingDistance = 0.5f;
    public int viewDistance = 15;
    public float distance;

    //public bool Running = false;

    public float searchTime = 8.0f;

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
        animator.SetBool("Running", false);
        animator.SetBool("Searching", false);
        animator.SetBool("Attacking", false);
        animator.SetBool("Walking", true);


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
        animator.SetBool("Running", true);
        animator.SetBool("Searching", false);
        animator.SetBool("Attacking", false);
        animator.SetBool("Walking", false);

        enemy.SetDestination(playerLocation);

        if (distance <= 3)
        {
            SwitchState(State.Attacking);
        }

        if (distance >= viewDistance)
        {
            lastPlayerLocation = player.gameObject.transform.position;
            searchTime = 8.0f;
            SwitchState(State.Searching);
        }
    }

    void Searching()
    {
        animator.SetBool("Running", false);
        animator.SetBool("Searching", true);
        animator.SetBool("Attacking", false);
        animator.SetBool("Walking", false);

        enemy.SetDestination(lastPlayerLocation);

        searchTime -= Time.deltaTime;

        if (searchTime <= 0.0f)
        {
            SwitchState(State.Retreating);
        }


    }

    void Retreating()
    {
        animator.SetBool("Running", true);
        animator.SetBool("Searching", false);
        animator.SetBool("Attacking", false);
        animator.SetBool("Walking", false);



        enemy.SetDestination(points[patrolDestinationPoint].position);

        if ((enemyLocation.x == points[patrolDestinationPoint].position.x) && (enemyLocation.z == points[patrolDestinationPoint].position.z))
        {
            SwitchState(State.Patrolling);
        }

        
    }

    void Attacking()
    {
        animator.SetBool("Running", false);
        animator.SetBool("Searching", false);
        animator.SetBool("Attacking", true);
        animator.SetBool("Walking", false);

        enemy.SetDestination(enemyLocation);
    }

    void Update()
    {
        enemyLocation = enemy.transform.position;
        playerLocation = player.gameObject.transform.position;
        distance = Vector3.Distance(playerLocation, enemyLocation);
        

        if (distance <= viewDistance && distance > 3)
        {
           SwitchState(State.Chasing);
           
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
