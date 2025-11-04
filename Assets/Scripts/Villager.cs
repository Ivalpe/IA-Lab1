using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Unity.Behavior;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Villager : MonoBehaviour
{
    public GameObject SmellSource;

    private NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(CreateSmellSource());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
        
    }

    private IEnumerator CreateSmellSource()
    {
        WaitForSeconds wait = new WaitForSeconds(2);

        while (true)
        {
            yield return wait;
            //Instantiate(SmellSource);
        }
    }
}