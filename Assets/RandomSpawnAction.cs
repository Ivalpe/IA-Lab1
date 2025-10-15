using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RandomSpawn", story: "Random Spawn [Agent] with [Origin] and [Radius]", category: "Action", id: "eeacba62c86db22680ee716eedba2747")]
public partial class RandomSpawnAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Origin;
    [SerializeReference] public BlackboardVariable<float> Radius;

    protected override Status OnStart()
    {
        Vector3 originPosition = Origin.Value.transform.position;

        if (GetRandomNavMeshPosition(originPosition, Radius.Value, out Vector3 newPosition))
        {
            Agent.Value.transform.position = newPosition;
            NavMeshAgent navAgent = Agent.Value.GetComponent<NavMeshAgent>();
            navAgent.Warp(newPosition);

            return Status.Success;
        }

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }

    private bool GetRandomNavMeshPosition(Vector3 origin, float radius, out Vector3 result)
    {
        Vector2 randomCircle = UnityEngine.Random.insideUnitCircle * radius;
        Vector3 randomPoint = origin + new Vector3(randomCircle.x, 0, randomCircle.y);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, radius * 0.5f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}

