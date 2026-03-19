using UnityEngine;
public class JumpPad : MonoBehaviour
{
    public float jumpForce = 8f;
    public float craneJumpForce = 8f;
    public ParticleSystem jumpEffect;

    void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Boxes always bounce
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            if (jumpEffect != null)
                jumpEffect.Play();
        }

        // Crane only jumps once
        if (other.gameObject.name == "Grúa" || other.transform.root.name == "Grúa")
        {
            CraneController crane = other.transform.root.GetComponent<CraneController>();
            if (crane != null && !crane.isJumping)
            {
                crane.StopAllCoroutines();
                crane.StartCoroutine(crane.Jump(craneJumpForce));
                if (jumpEffect != null)
                    jumpEffect.Play();
            }
        }
    }

}
