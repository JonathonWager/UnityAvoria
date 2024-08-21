using UnityEngine;
using UnityEngine.Rendering;

public class AdjustSortingOrder : MonoBehaviour
{
    public Transform feetPosition;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Set the sorting order based on the Y position of the feet
        spriteRenderer.sortingOrder = Mathf.RoundToInt(feetPosition.position.y * -1);
    }
}
