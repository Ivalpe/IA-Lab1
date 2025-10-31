using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckPlayerRange", story: "Check [Target] in [radius] and [angle] for [pos]", category: "Action", id: "6bab6e8a98ee6d3c6ed8fd5bb18036a8")]
public partial class CheckPlayerRangeAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> Radius;
    [SerializeReference] public BlackboardVariable<float> Angle;
    [SerializeReference] public BlackboardVariable<Vector3> Pos;

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

