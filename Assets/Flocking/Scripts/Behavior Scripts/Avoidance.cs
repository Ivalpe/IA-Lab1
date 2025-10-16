using System.Collections;
using System.Collections.Generic;
using Unity.AppUI.UI;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]

public class Avoidance : FilteredFlockBehavior
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

        //apply context filter if there is one
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform t in filteredContext)
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

        //fix y
        avoidanceMove.y = 0f;

        return avoidanceMove;
    }
}
