using System.Collections;
using System.Collections.Generic;
using Unity.AppUI.UI;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class Cohesion : FilteredFlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //no neighbors, change nothing
        if(context.Count == 0)
        {
            return Vector3.zero;
        }

        //else: finds middle point between all neighbors to move there

        //add points together
        Vector3 cohesionMove = Vector3.zero;

        //apply context filter if there is one
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform t in filteredContext)
        {
            cohesionMove += t.position;
        }
        //calculate average
        cohesionMove /= context.Count;

        //offset for the specific agent position
        cohesionMove -= agent.transform.position;
        //fix y
        cohesionMove.y = 0f;
        return cohesionMove;
    }
}
