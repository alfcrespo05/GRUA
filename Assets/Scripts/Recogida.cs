using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    public float pickupRange = 1.5f;
    public KeyCode pickupKey = KeyCode.Space;

    private GameObject attachedBox = null;

    void Update()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            if (attachedBox == null)
                TryPickup();
            else
                Drop();
        }
    }

    void TryPickup()
    {
        Collider[] nearby = Physics.OverlapSphere(transform.position, pickupRange);

        foreach (Collider col in nearby)
        {
            if (col.gameObject.name == "Caja")
            {
                attachedBox = col.gameObject;

                // Disable box physics while carried
                Rigidbody boxRB = attachedBox.GetComponent<Rigidbody>();
                if (boxRB != null)
                    boxRB.isKinematic = true;

                // Attach box to cargo
                attachedBox.transform.SetParent(transform);
                attachedBox.transform.localPosition = new Vector3(0, -0.6f, 0);
                attachedBox.GetComponent<Collider>().enabled = false;

                Debug.Log("Picked up box!");
                break;
            }
        }
    }

    public void Drop()
    {
        if (attachedBox == null) return;

        attachedBox.transform.SetParent(null);
        attachedBox.GetComponent<Collider>().enabled = true;

        Rigidbody boxRB = attachedBox.GetComponent<Rigidbody>();
        if (boxRB != null)
        {
            boxRB.isKinematic = false;

            // Transfer cargo momentum to box
            Rigidbody cargoRB = GetComponent<Rigidbody>();
            if (cargoRB != null)
                boxRB.velocity = cargoRB.velocity * 1.5f;
        }
        Outline outline = attachedBox.GetComponent<Outline>();
        if (outline != null)
            outline.enabled = true;
        attachedBox = null;
    }

    public bool IsCarryingBox()
    {
        return attachedBox != null;
    }
    public GameObject GetCarriedBox()
    {
        return attachedBox;
    }

    public void AttachBox(GameObject box)
    {
        attachedBox = box;
        attachedBox.transform.SetParent(transform);
        attachedBox.transform.localPosition = new Vector3(0, -0.6f, 0);

        Outline outline = attachedBox.GetComponent<Outline>();
        if (outline != null)
            outline.enabled = true;
    }
}
