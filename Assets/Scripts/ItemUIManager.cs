using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUIManager : MonoBehaviour
{
    public GameObject panel;
    public GameObject itemUIPrefab;
    public List<Texture2D> itemTextures;
    public List<string> itemNames = new List<string> { "Key", "Books", "Shoe", "Aidkit" };
    private Dictionary<string, Image> itemCheckmarks = new Dictionary<string, Image>();

    void Start()
    {
        InitializeUI();
    }

    void InitializeUI()
    {
        Debug.Log("Initializing UI...");

        float yOffset = 0f;

        for (int i = 0; i < itemNames.Count; i++)
        {
            GameObject itemUI = Instantiate(itemUIPrefab, panel.transform);

            itemUI.transform.localPosition += new Vector3(100f, -yOffset, 0f);

            Image icon = itemUI.transform.Find("Icon").GetComponent<Image>();
            TextMeshProUGUI nameText = itemUI.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            Image checkmark = itemUI.transform.Find("Checkmark").GetComponent<Image>();

            icon.sprite = SpriteFromTexture(itemTextures[i]);
            nameText.text = itemNames[i];
            checkmark.enabled = false;

            itemCheckmarks.Add(itemNames[i], checkmark);

            yOffset += 300f;
        }
    }

    private Sprite SpriteFromTexture(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }

    public void UpdateItem(string itemName, bool collected)
    {
        if (itemCheckmarks.ContainsKey(itemName))
        {
            itemCheckmarks[itemName].enabled = collected;
            Debug.Log($"Item {itemName} updated. Collected: {collected}");
        }
        else
        {
            Debug.LogWarning($"Item {itemName} not found in dictionary.");
        }
    }

    public void RemoveItem(string itemTag)
    {

        GameObject[] items = GameObject.FindGameObjectsWithTag(itemTag);

        if (items.Length > 0)
        {
            GameObject itemToRemove = items[0];
            Destroy(itemToRemove);

            UpdateItem(itemTag, true);
        }
        else
        {
            Debug.LogWarning($"No item with tag '{itemTag}' found to remove.");
        }
    }
}
