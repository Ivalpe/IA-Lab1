using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetTag", story: "Set [tag] to [gameobject]", category: "Action", id: "e802750acd638b3890839c7cc39cc1af")]
public partial class SetTagAction : Action
{
    [SerializeReference] public BlackboardVariable<string> Tag;
    [SerializeReference] public BlackboardVariable<GameObject> Gameobject;

    protected override Status OnStart()
    {
        Gameobject.Value.tag = Tag; 
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

