using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    float movementX;
    float movementY;

    public float speed = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("On Move");
        Vector2 movementVector = context.ReadValue<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
