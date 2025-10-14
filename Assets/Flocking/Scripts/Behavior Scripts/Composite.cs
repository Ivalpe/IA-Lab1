using System.Collections;
using System.Collections.Generic;
using Unity.AppUI.UI;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class Composite : FlockBehavior
{
    public FlockBehavior[] behaviors;
    public float[] behaviorWeights;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //check if weight array and behavior array are the same length as they should be
        if(behaviorWeights.Length != behaviors.Length)
        {
            //log error if not equal
            Debug.LogError("Data mismatch in " + name, this);
            return Vector3.zero;
        }

        //setting up movement
        Vector3 move = Vector3.zero;

        //go through behaviors
        for(int i = 0; i < behaviors.Length; i++)
        {
            Vector3 partialMove = behaviors[i].CalculateMove(agent, context, flock) * behaviorWeights[i];

            if(partialMove != Vector3.zero)
            {
                //limit partial move to weight otherwise pass it as normal
                if (partialMove.sqrMagnitude > behaviorWeights[i] * behaviorWeights[i])
                {
                    partialMove.Normalize();
                    partialMove *= behaviorWeights[i];
                }
               
                move += partialMove;
            }
        }
        //fix y axis
        move.y = 0f;
        return move;
    }

}
