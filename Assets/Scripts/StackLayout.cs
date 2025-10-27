using UnityEngine;

public class StackLayout : MonoBehaviour
{
    [SerializeField] private float offsetY;

    private void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var card = transform.GetChild(i) as RectTransform;
            if (card != null)
            {
                card.anchoredPosition = new Vector2(0, i * offsetY);
            }
        }
    }
}
