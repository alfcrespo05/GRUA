using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 10f;
    public float height = 6f;
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        //Se posiciona detras de la grua
        Vector3 desiredPosition = target.position
            - target.forward * distance
            + Vector3.up * height;

        //Se mueve suavemente
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        //Mira a la grua
        transform.LookAt(target.position + Vector3.up * 2);
    }
}
