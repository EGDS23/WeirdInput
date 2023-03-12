using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModuleUI : MonoBehaviour
{
    TMP_Text Key;
    TMP_Text Inventory;
    Image HealthBar;
    int maxHealth;
    public ModuleType type;

    void Start()
    {
        Key = transform.Find("Key").GetComponent<TMP_Text>();
        Inventory = transform.Find("Inventory").GetComponent<TMP_Text>();
        HealthBar = transform.Find("HP").GetComponent<Image>();
    }

    public void UpdateModule(KeyCode key, int hp){
        Key.text = key.ToString();
        maxHealth = hp;
        HealthBar.fillAmount = 1;
    }

    public void UpdateInventory(int count){
        Inventory.text = count.ToString();
    }

    public void UpdateHealthBar(int health){
        if(health <= 0) Key.text = "";
        HealthBar.fillAmount = Mathf.Clamp((float)health / maxHealth, 0, 1f);
    }
}
