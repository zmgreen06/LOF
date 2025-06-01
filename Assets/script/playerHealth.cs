using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 100;
    public Image healthBar;

    
    //, delay = 0.15f;

    // public UnityEvent OnBegin, OnDone;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(int amount){
        health -= amount;
        healthBar.fillAmount = health / 100f;
        

        if(health <=0)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
            TakeDamage(10);
            
        }
    }

    // public void PlayFeedback(GameObject sender)
    // {
    //     StopAllCoroutines();
    //     OnBegin?.Invoke();
    //     Vector2 direction = (sender.transform.position-transform.position).normalized;
    //     rb2d.AddForce(direction*strength, ForceMode2d.Impulse);
    //     StartCoroutine(Reset());
    // }

    // private IEnumerator Reset()
    // {
    //     yield return new WaitForSeconds(delay);
    //     rb2d.velocity = Vector3.zero;
    //     OnDone?.Invoke();
    // }
}
