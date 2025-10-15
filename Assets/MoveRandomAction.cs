using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveRandom", story: "Move Random [Agent] with [Radius]", category: "Action", id: "fe72b88cd36dfdc75a1e0c2935c76ffe")]
public partial class MoveRandomAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<float> Radius;

    private NavMeshAgent navMeshAgent;
    private Vector3 randomPosition;
    protected override Status OnStart()
    {

        navMeshAgent = Agent.Value.GetComponent<NavMeshAgent>();

        if (GetRandomNavMeshPosition(Agent.Value.transform.position, Radius.Value, out randomPosition))
        {
            navMeshAgent.SetDestination(randomPosition);
            return Status.Running;
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
        if (NavMesh.SamplePosition(randomPoint, out hit, radius, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}

