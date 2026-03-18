using UnityEngine;

public class CraneController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 80f;
    public float armRotateSpeed = 60f;
    public Transform arm;
    public float reelSpeed = 2f;
    public Transform ropeAnchor;
    public float trolleySpeed = 3f;
    public float trolleyMin = 0.5f;
    public float trolleyMax = 3.5f;
    public float jumpHeight = 5f;
    private float verticalVelocity = 0f;
    public bool isJumping = false;
    public float platformBoundary = 9f;

    private SoundManager soundManager;

    public float craneRadius = 1f;

    public float craneGroundOffset = 0.79f;
    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        soundManager = FindObjectOfType<SoundManager>();
        soundManager.PlayMotor(true);
    }

    void Update()
    {

        float move = Input.GetAxis("Vertical");
        float rotate = Input.GetAxis("Horizontal");

        //Moviemiento con W y S. Aparte de un limite que le para
        Vector3 newPos = transform.position + transform.forward * move * moveSpeed * Time.deltaTime;

        bool blocked = false;
        CircleCollider[] obstacles = FindObjectsOfType<CircleCollider>();

        foreach (CircleCollider obs in obstacles)
        {
            float dist = Vector2.Distance(
                new Vector2(newPos.x, newPos.z),
                new Vector2(obs.transform.position.x, obs.transform.position.z)
            );
            if (dist < obs.radius + craneRadius)
            {
                Debug.Log("Blocked by: " + obs.gameObject.name);
                blocked = true;
                break;
            }
        }
        RectCollider[] rectObstacles = FindObjectsOfType<RectCollider>();
        foreach (RectCollider rect in rectObstacles)
        {
            if (rect.IsBlocking(newPos, craneRadius))
            {
                Debug.Log("Blocked by: " + rect.gameObject.name);
                blocked = true;
                break;
            }
        }

        if (!blocked && Mathf.Abs(newPos.x) < platformBoundary && Mathf.Abs(newPos.z) < platformBoundary)
            transform.position = newPos;

        transform.Rotate(Vector3.up * rotate * rotateSpeed * Time.deltaTime);

        //Rotacion de el brazo con Q y E
        if (Input.GetKey(KeyCode.E))
            arm.Rotate(Vector3.up * armRotateSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.Q))
            arm.Rotate(Vector3.up * -armRotateSpeed * Time.deltaTime);

        //Movimiento de la cuerda sobre el brazo con C y V
        if (Input.GetKey(KeyCode.C))
            ropeAnchor.localPosition = new Vector3(
                Mathf.Clamp(ropeAnchor.localPosition.x - trolleySpeed * Time.deltaTime, trolleyMin, trolleyMax),
                ropeAnchor.localPosition.y,
                ropeAnchor.localPosition.z);
        if (Input.GetKey(KeyCode.V))
            ropeAnchor.localPosition = new Vector3(
                Mathf.Clamp(ropeAnchor.localPosition.x + trolleySpeed * Time.deltaTime, trolleyMin, trolleyMax),
                ropeAnchor.localPosition.y,
                ropeAnchor.localPosition.z);

    }
    public void ResetPosition()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
    public System.Collections.IEnumerator Jump(float force)
    {
        verticalVelocity = 0f;
        isJumping = true;
        verticalVelocity = force;

        while (verticalVelocity > -20f)
        {
            transform.position += Vector3.up * verticalVelocity * Time.deltaTime;
            verticalVelocity -= 20f * Time.deltaTime;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 10f))
            {
                if (verticalVelocity < 0 && transform.position.y <= hit.point.y + craneGroundOffset)
                {
                    transform.position = new Vector3(transform.position.x, hit.point.y + craneGroundOffset, transform.position.z);
                    isJumping = false;
                    yield break;
                }
            }
            else if (transform.position.y <= 0.79f)
            {
                // Fallback if raycast misses
                transform.position = new Vector3(transform.position.x, 0.79f, transform.position.z);
                isJumping = false;
                yield break;
            }

            yield return null;
        }
        isJumping = false;
    }
    public System.Collections.IEnumerator Descend(float targetY)
    {
        isJumping = true;
        verticalVelocity = -5f;

        while (transform.position.y > targetY)
        {
            transform.position += Vector3.up * verticalVelocity * Time.deltaTime;
            verticalVelocity -= 10f * Time.deltaTime;
            yield return null;
        }

        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
        isJumping = false;
    }
    public void ResetHeight()
    {
        transform.position = new Vector3(transform.position.x, 0.79f, transform.position.z);
        verticalVelocity = 0f;
        isJumping = false;
    }
}
