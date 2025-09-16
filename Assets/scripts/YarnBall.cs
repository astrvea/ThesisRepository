using UnityEngine;

public class YarnBall : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // initial random direction
        Vector3 startDir = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
        rb.linearVelocity = startDir * moveSpeed;
    }

    void FixedUpdate()
    {
        // speed consistent
        if (rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * moveSpeed;
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (rb.linearVelocity.magnitude > 0.01f)
        {
            Vector3 incoming = rb.linearVelocity.normalized;
            Vector3 normal = collision.contacts[0].normal;

            // bounce
            Vector3 reflected = Vector3.Reflect(incoming, normal);

            /*// non stuck
            rb.position += normal * 0.05f;

            rb.linearVelocity = reflected.normalized * moveSpeed;*/
        }
    }
}
