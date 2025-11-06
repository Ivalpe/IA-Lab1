using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Unity.Behavior;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Villager : MonoBehaviour
{

    private NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
            {
                agent.SetDestination(hit.point);
            }
        }

    }

}