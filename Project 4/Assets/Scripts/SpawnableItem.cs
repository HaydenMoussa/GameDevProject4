using UnityEngine;

public class SpawnableItem : MonoBehaviour
{
    [Header("Item Properties")]
    [SerializeField] private bool useGravity = true;
    [SerializeField] private float initialForce = 5f;
    [SerializeField] private Vector3 forceDirection = Vector3.up;
    
    private Rigidbody rb;
    private float spawnTime;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = useGravity;
        }
        
        spawnTime = Time.time;
    }
    
    private void Start()
    {
        // Apply initial force if we have a rigidbody
        if (rb != null && initialForce > 0)
        {
            // Add some randomness to the direction
            Vector3 randomizedDirection = forceDirection + new Vector3(
                Random.Range(-0.3f, 0.3f),
                0,
                Random.Range(-0.3f, 0.3f)
            );
            
            rb.AddForce(randomizedDirection.normalized * initialForce, ForceMode.Impulse);
        }
    }
    
    private void Update()
    {
    }
}