using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TransformStep : ITimeStep
{
    public Vector3 position;
    public Quaternion rotation;
}
[CreateAssetMenu(fileName = "TransformInfo", menuName = "TimeControl/TransformInfo")]
public class TransformInfo : GenericInfo<TransformStep>
{  
    private Transform transform;
    public override void Record()
    {
        TransformStep newStep = new TransformStep();
        newStep.position = transform.position;
        newStep.rotation = transform.rotation;
        steps.Push(newStep);
    }
    protected override void Execute(TransformStep result, bool playBack)
    {
        transform.position = result.position;
        transform.rotation = result.rotation;
    }
    public override void Init(int Capacity, CustomizedStore store)
    {
        base.Init(Capacity, store);
        transform = store.transform;
    }

}
