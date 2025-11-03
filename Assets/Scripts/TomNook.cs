using System.Collections;
using Unity.Behavior;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TomNook : MonoBehaviour
{

    public float radius;
    [Range(0, 360)]
    public float angle;

    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public GameObject playerRef;

    [SerializeField] private RuntimeBlackboardAsset m_blackboardAsset;

    public bool canSeePlayer;

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
        LayerMask playerMask = 1 << LayerMask.NameToLayer("Target");
        LayerMask moneyBagMask = 1 << LayerMask.NameToLayer("MoneyBags");

        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);


        Transform bestTarget = null;
        float closestDistance = float.MaxValue;

        // Check each target in range
        if (rangeChecks.Length != 0)
        {
            foreach (Collider col in rangeChecks)
            {
                Transform currentTarget = col.transform;
                Vector3 directionToTarget = (currentTarget.position - transform.position).normalized;
                float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);

                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                {
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    {
                        int targetLayer = 1 << currentTarget.gameObject.layer;

                        // Found the player
                        if ((targetLayer & playerMask) != 0)
                        {
                            bestTarget = currentTarget;
                            break;
                        }

                        // Found a money bag
                        if ((targetLayer & moneyBagMask) != 0)
                        {
                            if (distanceToTarget < closestDistance)
                            {
                                closestDistance = distanceToTarget;
                                bestTarget = currentTarget;
                            }
                        }
                    }
                }
            }
        }

        BlackboardVariable<Vector3> posTargetBbv = null;
        BlackboardVariable<bool> seeingVillagerBbv = null;

        foreach (var bbv in m_blackboardAsset.Blackboard.Variables)
        {
            if (bbv.Name == "PosTarget" && bbv is BlackboardVariable<Vector3> vectorBbv)
                posTargetBbv = vectorBbv;

            if (bbv.Name == "SeeingVillager" && bbv is BlackboardVariable<bool> boolBbv)
                seeingVillagerBbv = boolBbv;
        }

        if (bestTarget != null) //Target found
        {
            canSeePlayer = true;
            posTargetBbv.Value = bestTarget.position;
            seeingVillagerBbv.Value = true;
        }
        else //No target found
        {
            canSeePlayer = false;
            seeingVillagerBbv.Value = false;
        }

    }

}
