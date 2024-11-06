using UnityEngine;
using UnityEngine.InputSystem;

public class CapsuleController : MonoBehaviour
{
    public float speed = 5f;
    public float lookSpeed = 2f;
    public float jumpHeight = 2f;
    private CharacterController controller;
    private Vector3 velocity;
    private float gravity = -9.81f;
    private bool isGrounded;
    private Vector2 moveInput;
    private Vector2 lookInput;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Sprawdzamy, czy posta� stoi na ziemi
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Ruch postaci na podstawie warto�ci moveInput
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        // ew poprawka
        // Vector3 move = transform.TransformDirection(new Vector3(moveInput.x, 0, moveInput.y));
        controller.Move(move * speed * Time.deltaTime);

        // Obr�t postaci (wok� osi Y)
        transform.Rotate(Vector3.up * lookInput.x * lookSpeed);

        // Grawitacja
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnMove(InputValue movementValue)
    {
        // Pobieramy dane ruchu (x i y) z wej�cia
        moveInput = movementValue.Get<Vector2>();
    }

    private void OnJump(InputValue movementValue)
    {
        // Je�li posta� jest na ziemi, ustawiamy pr�dko�� skoku
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void OnLook(InputValue lookValue)
    {
        lookInput = lookValue.Get<Vector2>();
    }

}