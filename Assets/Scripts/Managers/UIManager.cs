using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Slider healthSlider;
    public TextMeshProUGUI healthText;

    [SerializeField] TextMeshProUGUI bolivarText;

    //[SerializeField] Image gunImage;
    //[SerializeField] Text weaponName;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(UpdatePlayerUI());
    }
    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator UpdatePlayerUI()
    {
        yield return new WaitForSeconds(0.1f);
        PlayerHealthHandler playerHealth = null;
        while(playerHealth == null)
        {
            playerHealth = FindObjectOfType<PlayerHealthHandler>();
        }
        healthSlider.maxValue = playerHealth.GetMaxHealth();
        healthSlider.value = playerHealth.GetCurrentHealth();
        Debug.Log("Vida max: " + playerHealth.GetMaxHealth());
        healthText.text = playerHealth.GetCurrentHealth() + "/" + playerHealth.GetMaxHealth();

        bolivarText.text = GameManager.instance.GetCurrentBolivar().ToString();
    
    }

    public void UpdateBolivarText(int cantidadBolivar)
    {
        bolivarText.text = cantidadBolivar.ToString();
    }
}
