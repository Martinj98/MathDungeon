using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject gameOverScreen;
    public string startLevel;
    private bool isReviving;
    public Vector3 startPos;

    public Slider healthSlider;
    public bool isAlive=true;
    public int currentHealth;
    public int maxHealth;
    public ParticleSystem hitParticles;
    void Start()
    {
        hitParticles=GetComponent<ParticleSystem>();
        healthSlider.minValue = 0f;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    private void Update()
    {
        //Display gameOver Screen when the player dies.
        if (!isAlive&& Input.GetKeyDown(KeyCode.R)&& !isReviving) {
            isReviving = true ;
            StartCoroutine(restart());
        }    
    }

    private IEnumerator restart() {
        //Debug.Log("In the revive coroutine");
        SceneManager.LoadScene(startLevel);
        transform.position = startPos;

        yield return new WaitForSeconds(1f);
        //Restore to full health
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
        isAlive = true;

        gameOverScreen.SetActive(false);
        isReviving = false;


    }

    public void getHit(int damage) {
        hitParticles.Play();
        currentHealth -= damage;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die() {
        isAlive = false;
        gameOverScreen.SetActive(true);
        //Debug.Log("Player DIED");
    }

    

}
