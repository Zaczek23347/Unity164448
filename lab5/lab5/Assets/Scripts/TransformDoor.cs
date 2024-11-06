using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform door; // Drzwi, kt�re maj� si� podnosi�
    public float openHeight = 5f; // Wysoko��, na kt�r� drzwi si� podnios�
    public float speed = 2f; // Pr�dko�� otwierania/zamykania

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpening = false;

    void Start()
    {
        // Pozycja zamkni�tych drzwi
        closedPosition = door.position;
        // Pozycja otwartych drzwi (przesuni�cie w osi Y)
        openPosition = closedPosition + Vector3.up * openHeight;
    }

    void Update()
    {
        // Interpolacja pozycji drzwi w zale�no�ci od stanu isOpening
        if (isOpening)
        {
            door.position = Vector3.Lerp(door.position, openPosition, speed * Time.deltaTime);
        }
        else
        {
            door.position = Vector3.Lerp(door.position, closedPosition, speed * Time.deltaTime);
        }
    }

    // Wykrywanie kolizji wej�cia do triggera
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpening = true;
        }
    }

    // Wykrywanie kolizji wyj�cia z triggera
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpening = false;
        }
    }
}