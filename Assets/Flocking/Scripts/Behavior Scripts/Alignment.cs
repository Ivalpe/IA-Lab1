using System.Collections;
using System.Collections.Generic;
using Unity.AppUI.UI;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class Alignment : FilteredFlockBehavior
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

        //apply context filter if there is one
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform t in filteredContext)
        {
            alignmentMove += t.transform.forward;
        }
        //calculate average
        alignmentMove /= context.Count;
        //fix y
        alignmentMove.y = 0f;

        return alignmentMove;
    }
   
}
