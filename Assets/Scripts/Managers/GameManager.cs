using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int currentBolivares;

    public static GameManager instance;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.UpdateBolivarText(currentBolivares);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetBolivares(int cantidadImprimir)
    {
        currentBolivares += cantidadImprimir;
        UIManager.instance.UpdateBolivarText(currentBolivares);

    }
    public void SpendBolivares(int cantidadGastar)
    {
        currentBolivares -= cantidadGastar;
        if (currentBolivares <= 0)
        {
            currentBolivares = 0;
        }
        UIManager.instance.UpdateBolivarText(currentBolivares);

    }

    public int GetCurrentBolivar() { return currentBolivares; }
}
