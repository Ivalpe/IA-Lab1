using System.Collections;
using System.Collections.Generic;
using Unity.AppUI.UI;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class Alignment : FlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //no neighbors, keep going the way you are going
        if (context.Count == 0)
        {
            return agent.transform.forward;
        }

        //add direction that each one is facing together
        Vector3 alignmentMove = Vector3.zero;
        foreach (Transform t in context)
        {
            alignmentMove += t.transform.forward;
        }
        //calculate average
        alignmentMove /= context.Count;

        return alignmentMove;
    }
   
}
