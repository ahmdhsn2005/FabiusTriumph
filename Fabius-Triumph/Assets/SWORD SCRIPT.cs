using UnityEngine;

public class SwordRotation : MonoBehaviour
{
    public float rotationSpeed = 100f; // Speed of rotation
    public Transform playerHand; // Assign the player's hand transform in the Inspector
    private bool isTriggered = false; // Track if the sword is triggered
    private Rigidbody swordRigidbody;

    void Start()
    {
        // Optional: Get Rigidbody if needed for physics-based interactions
        swordRigidbody = GetComponent<Rigidbody>();
        if (swordRigidbody != null)
        {
            swordRigidbody.isKinematic = true; // Prevent physics movement by default
        }
    }

    void Update()
    {
        if (!isTriggered)
        {
            // Rotate the sword continuously around its Y-axis
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with the sword
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player touched the sword!");
            isTriggered = true; // Stop the rotation
            AttachToPlayer(); // Attach the sword to the player's hand
        }
    }

    void AttachToPlayer()
    {
        // Stop the rotation and reset the sword's parent
        transform.SetParent(playerHand); // Make the sword a child of the player's hand
        transform.localPosition = Vector3.zero; // Reset position relative to the hand
        transform.localRotation = Quaternion.identity; // Reset rotation relative to the hand

        // Disable Rigidbody (optional)
        if (swordRigidbody != null)
        {
            swordRigidbody.isKinematic = true;
            swordRigidbody.detectCollisions = false; // Prevent unnecessary collisions
        }

        Debug.Log("Sword attached to the player!");
    }
}
