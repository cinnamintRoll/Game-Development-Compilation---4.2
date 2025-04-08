using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [Header("Collection Settings")]
    public int collectedItems = 0;
    public int requiredItems = 3;
    public TextMeshProUGUI collectionCounterText;

    [Header("Game State")]
    public bool allItemsCollected = false;
    public GameObject winPanel;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        UpdateCollectionUI();

        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if player collided with a collectible
        if (other.CompareTag("Collectible"))
        {
            CollectItem(other.gameObject);
        }

        // Check if player reached extraction point and has all items
        if (other.CompareTag("ExtractionPoint") && allItemsCollected)
        {
            WinGame();
        }
    }

    void CollectItem(GameObject item)
    {
        collectedItems++;
        UpdateCollectionUI();

        // Disable the collectible (alternative to Destroy to avoid errors)
        item.SetActive(false);

        // Check if all items are collected
        if (collectedItems >= requiredItems)
        {
            allItemsCollected = true;
            Debug.Log("All items collected! Head to the extraction point!");
        }
    }

    void UpdateCollectionUI()
    {
        if (collectionCounterText != null)
        {
            collectionCounterText.text = "Items: " + collectedItems + " / " + requiredItems;
        }
    }

    void WinGame()
    {
        Debug.Log("Game Won!");

        // Show win panel
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
    }
}