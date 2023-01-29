using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;
    [SerializeField] float invencibilityTime = 1f;
    [SerializeField] SpriteRenderer playerSprite;

    private bool isInvincible;
    // Start is called before the first frame update
    void Start()
    {
        isInvincible = false;

        currentHealth = maxHealth;
        UIManager.instance.healthSlider.maxValue = maxHealth;
        UpdatePlayerHealth();
    }

    public void DamagePlayer(int amountOfDamage)
    {
        if (!isInvincible)
        {
            currentHealth -= amountOfDamage;
            UpdatePlayerHealth();
            isInvincible = true;

            StartCoroutine(Flasher());

            if (currentHealth <= 0)
            {
                //Muelto
                Debug.Log("Muelto");
                AudioManager.instance.PlayGameOverMusic();
                gameObject.SetActive(false);
            }
            StartCoroutine(PlayerIsInvicible());
        }
        
    }
    IEnumerator Flasher()
    {
        for (int i=0; i<5; i++)
        {
            playerSprite.color = new Color(
                    playerSprite.color.r,
                    .5f,
                    .5f,
                    0.3f
                );

            yield return new WaitForSeconds(.1f);

            playerSprite.color = new Color(
                    playerSprite.color.r,
                    255f,
                    255f,
                    1f
                );
            yield return new WaitForSeconds(.1f);

        }
    }
    IEnumerator PlayerIsInvicible()
    {
        yield return new WaitForSeconds(invencibilityTime);
        isInvincible = false;
    }
    private void UpdatePlayerHealth()
    {
        UIManager.instance.healthText.text = currentHealth + "/" + maxHealth; //Cambiamos el valor del texto de la vida
        UIManager.instance.healthSlider.value = currentHealth;//Movemos el slider
    }

    public void AddHpToPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdatePlayerHealth();
    }

    public void IncreaseMaxHealth(int maxHealthAmount)
    {
        maxHealth += maxHealthAmount;
        UIManager.instance.healthSlider.maxValue = maxHealth;
        UpdatePlayerHealth();
    }

    public int GetMaxHealth() { return maxHealth; }
    public int GetCurrentHealth() { return currentHealth; }

}
