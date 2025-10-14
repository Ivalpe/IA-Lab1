using System.Collections;
using System.Collections.Generic;
using Unity.AppUI.UI;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]

public class Avoidance : FlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //no neighbors, change nothing
        if (context.Count == 0)
        {
            return Vector3.zero;
        }

        //else: works opposite to cohesion

        //add points together
        Vector3 avoidanceMove = Vector3.zero;
        //keep count of how many are within avoidance radius 
        int numAvoid = 0;
        foreach (Transform t in context)
        {
            if(Vector3.SqrMagnitude(t.position - agent.transform.position) < flock.getSquareAvoidanceRadius)
            {
                numAvoid++;
                avoidanceMove += agent.transform.position - t.position;
            }
        }

        if(numAvoid > 0)
        {
            avoidanceMove /= numAvoid;
        }
       
        return avoidanceMove;
    }
}
