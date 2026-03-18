using UnityEngine;

public class CircleCollider : MonoBehaviour
{
    public float radius = 1f;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
