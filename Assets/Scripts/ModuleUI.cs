using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModuleUI : MonoBehaviour
{
    [SerializeField] TMP_Text Key;
    [SerializeField] TMP_Text Inventory;
    [SerializeField] Image HealthBar;
    int inventoryCount = 0;
    int maxHealth;
    public ModuleType type;

    public delegate void OnReplacementHandler(ModuleType type);
    public event OnReplacementHandler OnReplacement;

    void Awake()
    {
        //Key = transform.Find("Key").GetComponent<TMP_Text>();
        //Inventory = transform.Find("Inventory").GetComponent<TMP_Text>();
        //HealthBar = transform.Find("HP").GetComponent<Image>();

        inventoryCount = 0;
        Debug.Log("init");
        Key.text = KeyCode.None.ToString();
        Inventory.text = inventoryCount.ToString();
    }

    public void UpdateModule(KeyCode key, int hp){
        Debug.Log("Updated with key" + key);
        Key.text = key.ToString();
        maxHealth = hp;
        HealthBar.fillAmount = 1;
    }

    public void UpdateInventory(int count){
        inventoryCount += count;
        Debug.Log(Inventory.text);
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
