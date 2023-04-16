using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public float fallBoundary = -10f;
    public float jumpForce = 10f;
    [SerializeField] private AudioSource deathSoundEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
        
    }
    private void Update()
    {
        if (transform.position.y <= fallBoundary)
        {
            RestartLevel();
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")|| collision.gameObject.CompareTag("Traps"))
        {
            Die();
        }
    }
    private void Die()
    {
        deathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("death");
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
