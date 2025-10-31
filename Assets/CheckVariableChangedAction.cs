using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckVariableChanged", story: "If [vector3] changed", category: "Action", id: "c70594cba95c8ad7ca308ba3f9212f5d")]
public partial class CheckVariableChangedAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> Vector3;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

