using UnityEngine;

public class RedirectorPad : MonoBehaviour
{
    public LauncherPad targetLauncher;
    public float rotationAmount = 90f;
    private bool craneOnPad = false;
    public Transform arrowProjector;

    void Update()
    {
        if (targetLauncher != null)
            transform.forward = -targetLauncher.transform.forward;

        // Follow pad position and Y rotation only
        if (arrowProjector != null)
        {
            arrowProjector.position = new Vector3(transform.position.x, arrowProjector.position.y, transform.position.z);
            arrowProjector.rotation = Quaternion.Euler(90, transform.eulerAngles.y, 0);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.root.name == "Grúa" && !craneOnPad)
        {
            craneOnPad = true;
            targetLauncher.transform.Rotate(Vector3.up * rotationAmount);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.root.name == "Grúa")
            craneOnPad = false;
    }
}
