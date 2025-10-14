using System.Collections;
using System.Collections.Generic;
using Unity.AppUI.UI;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class Cohesion : FlockBehavior
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
        foreach (Transform t in context)
        {
            cohesionMove += t.position;
        }
        //calculate average
        cohesionMove /= context.Count;

        //offset for the specific agent position
        cohesionMove -= agent.transform.position;
        return cohesionMove;
    }
}
