using UnityEngine;
using System.Collections.Generic;

public class RopeGenerator : MonoBehaviour
{
    public int segmentCount = 6;
    public float segmentLength = 0.3f;
    public int minSegments = 2;
    public int maxSegments = 12;

    private List<GameObject> segments = new List<GameObject>();
    private GameObject cargo;

    public float reelCooldown = 0.15f;
    private float reelTimer = 0f;

    public Material ropeMaterial;
    public Material cargoMaterial;

    private SoundManager soundManager;

    void Start()
    {
        BuildRope();
        soundManager = FindObjectOfType<SoundManager>();
    }

    void Update()
    {
        reelTimer -= Time.deltaTime;

        if (reelTimer <= 0f)
        {
            if (Input.GetKey(KeyCode.F))
            {
                Reel(-1); //Acorta la cuerda
                reelTimer = reelCooldown;
            }
            if (Input.GetKey(KeyCode.R))
            {
                Reel(1);  //Alarga la cuerda
                reelTimer = reelCooldown;
            }
        }
        //Sonido cuando se mueve
        Rigidbody lastSegment = segments[segments.Count - 1].GetComponent<Rigidbody>();
        bool isSwinging = lastSegment.velocity.magnitude > 0.5f;
        soundManager.PlayRope(isSwinging);
    }

    void BuildRope()
    {
        //Borra lo que exista
        foreach (GameObject s in segments)
            Destroy(s);
        segments.Clear();
        GameObject carriedBox = null;
        if (cargo != null)
        {
            PickupSystem pickup = cargo.GetComponent<PickupSystem>();
            if (pickup != null && pickup.IsCarryingBox())
                carriedBox = pickup.GetCarriedBox();
            Destroy(cargo);
        }

        Rigidbody previousRB = GetComponent<Rigidbody>();

        for (int i = 0; i < segmentCount; i++)
        {
            GameObject segment = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            segment.name = "RopeSegment_" + i;
            segment.transform.localScale = new Vector3(0.1f, segmentLength, 0.1f);
            segment.transform.position = transform.position - new Vector3(0, i * segmentLength * 2, 0);
            if (ropeMaterial != null)
                segment.GetComponent<Renderer>().material = ropeMaterial;

            Rigidbody rb = segment.AddComponent<Rigidbody>();
            rb.drag = 3f;
            rb.angularDrag = 3f;
            rb.maxAngularVelocity = 20f;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            HingeJoint hinge = segment.AddComponent<HingeJoint>();
            hinge.connectedBody = previousRB;

            segments.Add(segment);
            previousRB = rb;
        }

        //Spawnea la carga 
        cargo = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cargo.name = "Cargo";
        Outline outline = cargo.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = new Color(1f, 0.5f, 0f);
        outline.OutlineWidth = 5f;
        cargo.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        cargo.transform.position = transform.position - new Vector3(0, segmentCount * segmentLength * 2, 0);

        Rigidbody cargoRB = cargo.AddComponent<Rigidbody>();
        cargoRB.mass = 5f;
        cargoRB.maxAngularVelocity = 20f;
        cargoRB.interpolation = RigidbodyInterpolation.Interpolate;
        cargoRB.drag = 3f;
        cargoRB.angularDrag = 3f;

        HingeJoint cargoHinge = cargo.AddComponent<HingeJoint>();
        cargoHinge.connectedBody = previousRB;
        if (cargoMaterial != null)
            cargo.GetComponent<Renderer>().material = cargoMaterial;
        cargo.AddComponent<PickupSystem>();
        if (carriedBox != null)
        {
            PickupSystem newPickup = cargo.GetComponent<PickupSystem>();
            newPickup.AttachBox(carriedBox);
        }
    }

    void Reel(int direction)
    {
        segmentCount = Mathf.Clamp(segmentCount - direction, minSegments, maxSegments);
        BuildRope();
    }
}
