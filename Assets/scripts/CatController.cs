using UnityEngine;
using UnityEngine.UI;

public class CatController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    public float rotationSpeed = 720f;

    [Header("Stamina")]
    public float maxStamina = 100f;
    public float staminaDrain = 15f;
    public float staminaRegen = 10f;
    private float currentStamina;

    [Header("UI")]
    public Slider staminaBar;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private float currentSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // locks rotation
        if (rb != null)
            rb.freezeRotation = true;

        currentStamina = maxStamina;

        if (staminaBar != null)
        {
            staminaBar.maxValue = maxStamina;
            staminaBar.value = currentStamina;
        }

    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(-h, 0f, -v).normalized;

        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0f;

        // stamina logic
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0f && moveDirection.sqrMagnitude > 0.01f)
        {
            currentStamina -= staminaDrain * Time.deltaTime;
            if (currentStamina < 0f) currentStamina = 0f;

            // only sprint if there is stamina
            if (currentStamina > 0f)
                currentSpeed = sprintSpeed;
            else
                currentSpeed = moveSpeed; // stamina empty
        }
        else
        {
            currentSpeed = moveSpeed;

            // regen stamina
            if (currentStamina < maxStamina)
            {
                currentStamina += staminaRegen * Time.deltaTime;
                if (currentStamina > maxStamina) currentStamina = maxStamina;
            }
        }

        // ui logic
        if (staminaBar != null)
        {
            staminaBar.value = currentStamina;
        }
    }

    void FixedUpdate()
    {
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            // correct rotation
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

            // move cat at current speed
            rb.MovePosition(rb.position + moveDirection * currentSpeed * Time.fixedDeltaTime);
        }
    }
}
