using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botrunner : MonoBehaviour
{
    Ray ray;

    private UnityEngine.AI.NavMeshAgent nav;
    public Animator anim;
    public GameObject goal;
    public float turnSmoothing = 15f; // A smoothing value for turning the player.
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        goal = GameObject.Find("goal");
        nav.destination = goal.transform.position;
        nav.speed = 3f;
    }

    // Update is called once per frame
    void Update()
    {

    

        if (transform.position!= goal.transform.position)
        {
            anim.SetFloat("Speed", 5f);
      
        }
        else
        {
            anim.SetFloat("Speed", 0f);
        }
    }


 

}

