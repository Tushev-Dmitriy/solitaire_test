using UnityEngine;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour
{
    [Header("Prefabs & Sprites")]
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Sprite cardBack;

    [Header("Zones")]
    [SerializeField] private Transform deckParent;

    private void Start()
    {
        CreateDeck();
    }

    private void CreateDeck()
    {
        for (int i = 0; i < 52; i++)
        {
            var card = Instantiate(cardPrefab, deckParent);
            var image = card.GetComponent<Image>();
            image.sprite = cardBack;
            card.name = $"Card_{i + 1}";
        }
    }
}
