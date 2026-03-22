using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public int winCount = 11;

    public float decelationRate = 1;

    private Rigidbody _rb;

    private float _movementX;
    private float _movementY;

    private int _counter = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _counter = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(_movementX, 0.0f, _movementY);
        if(movement.magnitude <= 0.1)
        {
            _rb.linearVelocity = Vector3.Lerp(_rb.linearVelocity, Vector3.zero, 
                decelationRate * Time.deltaTime);
        }
        else
        {
            _rb.AddForce(movement * speed);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("On Move");
        Vector2 movementVector = context.ReadValue<Vector2>();
        _movementX = movementVector.x;
        _movementY = movementVector.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            _counter++;
            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the current object
            Destroy(gameObject);
            // Update the winText to display "You Lose!"
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + _counter.ToString();
        if(_counter >= winCount)
        {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }
}
