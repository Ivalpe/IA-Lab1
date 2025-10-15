using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/FollowLeader")]
public class FollowLeader : FlockBehavior
{
    public Transform Leader;
    Vector3 currentVelocity;
    public float agentSmoothTime = 0.5f;
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //moves agent towards flock leader
        
        //if leader doesnt exist log error
        if(Leader == null)
        {
            Debug.LogError("No leader detected in " + name, this);
            return Vector3.zero;
        }

        //find offset between agent and leader
        Vector3 leaderOffset = Leader.position - agent.transform.position;
        leaderOffset = leaderOffset.normalized;

        //smooth it like cohesion so it doesnt flicker
        leaderOffset = Vector3.SmoothDamp(agent.transform.forward, leaderOffset, ref currentVelocity, agentSmoothTime);
        return leaderOffset;
    }
    
}
