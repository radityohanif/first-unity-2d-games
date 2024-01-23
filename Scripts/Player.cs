#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;
    private AudioSource audioSource;
    private int score = 0;
    private int highScore = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        sprite.flipX = false;
    }

    void Update()
    {
        // Jika user menekan touch screen 
        if (Input.GetMouseButton(0))
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            /**
               jika touchPosition nilainya negatif berarti user menekan touch screen pada area kiri
               jika touchPosition nilainya positif berarti user menekan touch screen pada area kanan
            */
            if (touchPosition.x < 0)
            {
                rb.AddForce(Vector2.left * moveSpeed);
                sprite.flipX = true;
            }
            else
            {
                rb.AddForce(Vector2.right * moveSpeed);
                sprite.flipX = false;
            }
            PlayWalkAnimation(true);
        }
        else
        {
            PlayWalkAnimation(false);
            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Jika player menyentuh GameObject dengan tag "Block" maka game akan berakhir
        if (collision.gameObject.CompareTag("Block"))
        {
            StartCoroutine(GameOver());
        }
    }

    void PlayWalkAnimation(bool isWalking)
    {
        animator.SetBool("walking", isWalking);
    }

    void SetHighScore()
    {
        score = PlayerPrefs.GetInt("score");
        Debug.Log("score = " + score);
        highScore = PlayerPrefs.GetInt("highScore");
        Debug.Log("highScore = " + highScore);
        if (score > highScore)
        {
            GameData.SavePlayerData(new PlayerData
            {
                name = "",
                score = 0,
                highScore = score,
            });
        }
    }

    IEnumerator GameOver()
    {
        SetHighScore();
        animator.SetBool("die", true);
        audioSource.Play();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length); // Tunggu hingga animasi selesai
        SceneManager.LoadScene("Game"); // Reset "Game" scene
    }
}
#endif
