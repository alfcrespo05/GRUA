using UnityEngine;

public class ShapeChangerPad : MonoBehaviour
{
    public Material sphereMaterial;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Caja"))
        {
            // Save position, rotation and rigidbody values
            Vector3 pos = other.transform.position;
            Quaternion rot = other.transform.rotation;
            float mass = other.GetComponent<Rigidbody>().mass;
            float size = other.transform.localScale.x;

            // Destroy old box
            Destroy(other.gameObject);

            // Create sphere
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.name = "Caja";
            sphere.tag = "Caja";
            sphere.transform.position = pos;
            sphere.transform.rotation = rot;
            sphere.transform.localScale = new Vector3(size, size, size);

            // Add physics
            Rigidbody rb = sphere.AddComponent<Rigidbody>();
            rb.mass = mass;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

            // Add outline
            Outline outline = sphere.AddComponent<Outline>();
            outline.OutlineMode = Outline.Mode.OutlineAll;
            outline.OutlineColor = Color.white;
            outline.OutlineWidth = 5f;

            // Apply material
            if (sphereMaterial != null)
                sphere.GetComponent<Renderer>().material = sphereMaterial;
        }
    }
}
