using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModuleUI : MonoBehaviour
{
    TMP_Text Key;
    TMP_Text Inventory;
    int inventoryCount = 0;
    Image HealthBar;
    int maxHealth;
    public ModuleType type;

    public delegate void OnReplacementHandler(ModuleType type);
    public event OnReplacementHandler OnReplacement;

    void Start()
    {
        Key = transform.Find("Key").GetComponent<TMP_Text>();
        Inventory = transform.Find("Inventory").GetComponent<TMP_Text>();
        HealthBar = transform.Find("HP").GetComponent<Image>();

        Key.text = KeyCode.None.ToString();
        Inventory.text = inventoryCount.ToString();
    }

    public void UpdateModule(KeyCode key, int hp){
        Key.text = key.ToString();
        maxHealth = hp;
        HealthBar.fillAmount = 1;
    }

    public void UpdateInventory(int count){
        inventoryCount += count;
        Inventory.text = inventoryCount.ToString();
    }

    public void UpdateHealthBar(int health){
        // if(health <= 0) Key.text = "";
        HealthBar.fillAmount = Mathf.Clamp((float)health / maxHealth, 0, 1f);
    }

    public void UpdateKey(KeyCode key){
        Key.text = key.ToString();
    }

    public void ReplaceMod(){
        if(inventoryCount <= 0) return;
        // only replace if module is destroyed
        if(Key.text != KeyCode.None.ToString()) return;
        if(OnReplacement != null) {
            OnReplacement(type);
            UpdateInventory(-1);
        }
    }
}
