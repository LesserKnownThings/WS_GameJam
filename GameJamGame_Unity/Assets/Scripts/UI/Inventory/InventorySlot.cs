using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    private Image slotIcon;
    [SerializeField]
    private Sprite defaultSprite;

    private Color defaultColor = Color.gray;

    public bool IsEmpty()
    {
        return slotIcon.sprite == defaultSprite;
    }

    private void Start()
    {
        if (slotIcon == null)
        {
            slotIcon = GetComponentInChildren<Image>();
        }
    }

    public void PopulateSlot(Sprite iconSprite)
    {
        if(slotIcon != null)
        {
            slotIcon.sprite = iconSprite;
            slotIcon.color = Color.white;
        }
    }

    public void ClearSlot()
    {
        if (slotIcon != null)
        {
            slotIcon.sprite = defaultSprite;
            slotIcon.color = defaultColor;
        }
    }
}