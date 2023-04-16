using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int fruits = 0;
    [SerializeField]private AudioSource collectionSoundEffect;
    [SerializeField] private Text fruitText;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Fruits"))
        {
            collectionSoundEffect.Play();
            var animator = collision.gameObject.GetComponent<Animator>();
            animator.Play("Fruit_Disappear");
            Destroy(collision.gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
            fruits++;
            fruitText.text = "Score: " + fruits;
            
        }
    }
}
