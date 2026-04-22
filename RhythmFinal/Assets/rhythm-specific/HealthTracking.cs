using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthTracking : MonoBehaviour
{
    public Image healthBar;
    public float health = 1f;
    public bool hasBeenHurt;
    public GameObject deathScreen;

    public IEnumerator TakeDamage()
    {
        if (hasBeenHurt == false)
        {
            health -= .25f;
            hasBeenHurt = true;
        }
        yield return new WaitForSeconds(1f);
        hasBeenHurt = false;

    }
    public IEnumerator TakeSmallDamage()
    {
        if (hasBeenHurt == false)
        {
            health -= .1f;
            hasBeenHurt = true;
        }
        yield return new WaitForSeconds(.1f);
        hasBeenHurt = false;

    }

    public IEnumerator RefillHealth()
    {
        health = 1f;
        yield return null;
    }

    void Update()
    {
        healthBar.fillAmount = health;
        if (health <= 0)
        {
            StartCoroutine(Death());
        }
    }
    public IEnumerator Death()
    {
        deathScreen.SetActive(true);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}