using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    /*enum State
    {
        Alive,
        Dead,
        Moving
    }*/

    //private State state;


    public NavMeshAgent agent;
    private Vector3 playerLocation;
    public GameObject player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerLocation = player.gameObject.transform.position;
        agent.SetDestination(playerLocation);


        /*switch (state)
        {
            case State.Alive:
                Debug.Log("State: Alive");
                break;

            case State.Dead:
                Debug.Log("State: Dead");
                break;

            case State.Moving:
                Debug.Log("State: Moving");
                break;
        }*/
    }


}
