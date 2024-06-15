using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIManager : MonoBehaviour
{
    public GameObject panel;
    public GameObject itemUIPrefab; // Prefab voor een enkele rij met item
    public List<Sprite> itemIcons; // Voeg hier de itemafbeeldingen toe
    public List<string> itemNames = new List<string> { "Shoe", "Key", "Book", "Medkit" }; // Voeg hier de itemnamen toe
    private Dictionary<string, Image> itemCheckmarks = new Dictionary<string, Image>();

    void Start()
    {
        InitializeUI();
    }

    void InitializeUI()
    {
        for (int i = 0; i < itemNames.Count; i++)
        {
            GameObject itemUI = Instantiate(itemUIPrefab, panel.transform);
            Image icon = itemUI.transform.Find("Icon").GetComponent<Image>();
            Text nameText = itemUI.transform.Find("Name").GetComponent<Text>();
            Image checkmark = itemUI.transform.Find("Checkmark").GetComponent<Image>();

            icon.sprite = itemIcons[i];
            nameText.text = itemNames[i];
            checkmark.enabled = true; // Standaard aan, je kunt dit wijzigen naar false indien gewenst

            itemCheckmarks.Add(itemNames[i], checkmark);
        }
    }

    public void UpdateItem(string itemName, bool collected)
    {
        if (itemCheckmarks.ContainsKey(itemName))
        {
            itemCheckmarks[itemName].enabled = collected;
        }
    }
}
