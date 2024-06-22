using UnityEngine;

public class UIRightTopCorner : MonoBehaviour
{
    public RectTransform targetUI; // 目标UI元素（如面板）
    public RectTransform movingUI; // 要移动的UI元素（如按钮）
    public Vector2 offset; // 偏移量，允许你调整位置

    void Start()
    {
        if (targetUI == null || movingUI == null)
        {
            Debug.LogError("UI elements not assigned!");
            return;
        }

        // 将 movingUI 的锚点设置为右上角
        movingUI.anchorMin = new Vector2(1, 1);
        movingUI.anchorMax = new Vector2(1, 1);

        // 将 pivot 也设置为右上角，这样它将以右上角为参考点进行定位
        movingUI.pivot = new Vector2(1, 1);
    }

    void Update()
    {
        if (targetUI != null && movingUI != null)
        {
            // 设置 movingUI 的位置，使其始终位于 targetUI 的右上角，并应用偏移量
            movingUI.anchoredPosition = new Vector2(targetUI.rect.width, targetUI.rect.height) + offset;
        }
    }
}
