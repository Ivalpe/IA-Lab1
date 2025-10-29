using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TomNook))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        TomNook tom = (TomNook)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(tom.transform.position, Vector3.up, Vector3.forward, 360, tom.radius);

        Vector3 viewAngle01 = DirectionFromAngle(tom.transform.eulerAngles.y, -tom.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(tom.transform.eulerAngles.y, tom.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(tom.transform.position, tom.transform.position + viewAngle01 * tom.radius);
        Handles.DrawLine(tom.transform.position, tom.transform.position + viewAngle02 * tom.radius);

        if (tom.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(tom.transform.position, tom.playerRef.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
