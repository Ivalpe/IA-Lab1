using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/StayInRadius")]
public class StayInRadius : FlockBehavior
{
    public Vector3 center;
    public float radius = 15f;

    //maintain flock closer to the center
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //check how far agent is from center
        Vector3 centerOffset = center - agent.transform.position;
        float t = centerOffset.magnitude / radius;

        //if we are very close to center, continue as we are- dont change anything
        if(t < 0.9f)
        {
            return Vector3.zero;
        }

        //otherwise start pulling towards the center
        centerOffset = centerOffset * t * t;
        return centerOffset;
    }
}
