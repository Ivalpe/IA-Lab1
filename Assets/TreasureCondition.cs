using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Treasure", story: "Treasure is taked", category: "Variable Conditions", id: "48932cd875de44662ce541c21a4f1660")]
public partial class TreasureCondition : Condition
{

    public override bool IsTrue()
    {
        return true;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
