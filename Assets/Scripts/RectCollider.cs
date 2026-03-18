using UnityEngine;

public class RectCollider : MonoBehaviour
{
    public float width = 2f;
    public float depth = 0.5f;

    public bool IsBlocking(Vector3 cranePos, float craneRadius)
    {
        // Convert crane position to local space of this object
        Vector3 localPos = transform.InverseTransformPoint(cranePos);

        // Check if crane is within rectangle + crane radius
        return Mathf.Abs(localPos.x) < (width / 2 + craneRadius) &&
               Mathf.Abs(localPos.z) < (depth / 2 + craneRadius);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(width, 1f, depth));
    }
}
