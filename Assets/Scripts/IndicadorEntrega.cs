using UnityEngine;
public class ZoneIndicator : MonoBehaviour
{
    public float bobHeight = 0.3f;
    public float bobSpeed = 2f;
    public float rotateSpeed = 90f;
    public float hoverHeight = 2f;
    public Transform targetZone;

    void Update()
    {
        // Follow zone and bob
        transform.position = new Vector3(
            targetZone.position.x,
            targetZone.position.y + hoverHeight + Mathf.Sin(Time.time * bobSpeed) * bobHeight,
            targetZone.position.z
        );

        // Spin
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }
}
