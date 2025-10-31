using System.Collections;
using Unity.Behavior;
using UnityEngine;

public class TomNook : MonoBehaviour
{

    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;
    [SerializeField] private RuntimeBlackboardAsset m_blackboardAsset;

    //private BlackboardVariable<Vector3> PosTarget;

    public bool canSeePlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) { 
                    canSeePlayer = true;
                    //PosTarget.Value = target.position;
                    foreach (var bbv in m_blackboardAsset.Blackboard.Variables)
                    {
                        Debug.Log("We are searching");
                        if(bbv.Name == "PosTarget")
                        {
                            if(bbv is BlackboardVariable<Vector3> vectorBbv){
                                Debug.Log("Found");
                                vectorBbv.Value = target.position;
                            }
                        }
                    }
                }
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }
}
