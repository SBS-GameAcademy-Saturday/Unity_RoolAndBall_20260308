using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public int winCount = 11;

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
        _rb.AddForce(movement * speed);
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

    void SetCountText()
    {
        countText.text = "Count: " + _counter.ToString();
        if(_counter >= winCount)
        {
            winTextObject.SetActive(true);
        }
    }
}
