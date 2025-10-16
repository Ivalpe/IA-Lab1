using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/PhysicsLayer")]
public class PhysicsLayerFilter : ContextFilter
{
    public LayerMask mask;

    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        foreach (Transform t in original)
        {
            //check if item is on layer
            if (mask == (mask | (1 << t.gameObject.layer)))
            {
                filtered.Add(t);
            }
        }
        return filtered;
    }
}
