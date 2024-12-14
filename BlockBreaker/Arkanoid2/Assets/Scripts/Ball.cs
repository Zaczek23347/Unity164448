using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    public float minY = -5.5f;
    public float maxVelocity = 6f;
    public float velocity = 6f;

    Rigidbody2D rb;

    public GameObject MainCamera;

    public TextMeshProUGUI scoreTxt;
    public GameObject[] livesImage;
    public GameObject gameOverPanel;
    public GameObject BallCopy;
    public Vector2 spawnOffSet = new Vector2(1, 0);

    LevelManager stats;

    private AudioSource audioSource; // Zmienna AudioSource

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * velocity;
        stats = MainCamera.GetComponent<LevelManager>();
        audioSource = GetComponent<AudioSource>(); // Inicjalizacja AudioSource
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < minY)
        {
            if (stats.lives <= 0)
            {
                GameOver();
            }
            else
            {
                if (GameObject.FindGameObjectsWithTag("Ball").Length > 1)
                    Destroy(gameObject);
                transform.position = Vector3.zero;
                rb.velocity = Vector2.down * 6f;
                stats.lives--;
                livesImage[stats.lives].SetActive(false);
            }
        }
        rb.velocity = rb.velocity * velocity;
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("brick"))
        {
            if (audioSource != null)
            {
                audioSource.Play(); // Odtwarzanie dŸwiêku
            }
            Destroy(collision.gameObject);
            stats.score += 10;
            scoreTxt.text = stats.score.ToString("00000");
            stats.brickCount--;
        }
        else if (collision.gameObject.CompareTag("life"))
        {
            if (audioSource != null)
            {
                audioSource.Play(); // Odtwarzanie dŸwiêku
            }
            Destroy(collision.gameObject);
            stats.score += 10;
            scoreTxt.text = stats.score.ToString("00000");
            stats.brickCount--;
            livesImage[stats.lives].SetActive(true);
            stats.lives++;
        }
        else if (collision.gameObject.CompareTag("multiply"))
        {
            if (audioSource != null)
            {
                audioSource.Play(); // Odtwarzanie dŸwiêku
            }

            Vector2 newPosition = (Vector2)transform.position + spawnOffSet;
            GameObject duplicatedObject = Instantiate(BallCopy, newPosition, Quaternion.identity);

            Rigidbody2D duplicatedRb = duplicatedObject.GetComponent<Rigidbody2D>();
            if (duplicatedRb != null)
            {
                duplicatedRb.velocity = Vector2.left * velocity;
            }

            Destroy(collision.gameObject);
            stats.score += 10;
            scoreTxt.text = stats.score.ToString("00000");
            stats.brickCount--;
        }
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        Destroy(gameObject);
    }
}
