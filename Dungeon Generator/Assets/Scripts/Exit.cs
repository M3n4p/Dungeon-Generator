using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public string[] Tags;
    public bool IsDefault;

    public Vector3 backwardVector
    {
        get
        {
            return -transform.forward;
        }
    }

    private void OnDrawGizmos()
    {
        var scale = 1.0f;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * scale);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * scale);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position - transform.right * scale, transform.position + transform.right * scale);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.125f);
    }
}
