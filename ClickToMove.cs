//-----------------FILE NAME ClickToMove---------------------

using UnityEngine;
using System.Collections;
using UnityEngine;


public class ClickToMove : MonoBehaviour
{
    private NaveMeshAgent naveAgen;

    private void Start()
    {
        navAgent = Getcomponent<NaveMeshAgent>()
    }


    private void Update()
    {
        if (Input.GetMouseButtonDone(0))
        {
            //create a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycasHit hit;

            //check if the ray hits the ground (navMesh)
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, NavMesh.AllAreas))
            {
                //Move the agent to the clicked position
                navAgent.SetDestination(hit.point);
            }
        }

    }


}