using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField]
    private WindowActionType actionType;

    [Space(10)]
    [SerializeField]
    private string buttonText = "{Text}";

    [SerializeField]
    private Color buttonColor = Color.white;

    [SerializeField]
    private Sprite buttonImage;

    [Space(10)]
    [Header("Text")]
    [Space(10)]

    [SerializeField]
    [Tooltip("This will be used as the default text size")]
    private float minTextSize = 15.0f;
    [SerializeField]
    private float maxTextSize = 25.0f;

    [SerializeField]
    private FontStyles fontStyle;

    [SerializeField]
    private bool isAutoScale = false;

    [SerializeField]
    private TextAlignmentOptions textAlignmentOptions;

    Dictionary<WindowActionType, SubWindow> windows;

    private void Start()
    {
        SetButton();

        Button button = GetComponent<Button>();
        if(button != null)
        {
            if (actionType == WindowActionType.Back)
            {
                button.onClick.AddListener(() => World.Instance.GetUIManager().BackAction());
            }
            else
            {
                button.onClick.AddListener(() => World.Instance.GetUIManager().CallAction(actionType));
            }
        }
    }

    private void SetButton()
    {
        TextMeshProUGUI buttonTextMesh = GetComponentInChildren<TextMeshProUGUI>();

        if (buttonTextMesh != null)
        {
            buttonTextMesh.SetText(buttonText);
            buttonTextMesh.fontSize = minTextSize;
            buttonTextMesh.alignment = textAlignmentOptions;
            buttonTextMesh.fontStyle = fontStyle;
            buttonTextMesh.enableAutoSizing = isAutoScale;
            buttonTextMesh.fontSizeMin = minTextSize;
            buttonTextMesh.fontSizeMax = maxTextSize;
        }

        Image image = GetComponent<Image>();

        if (image != null)
        {
            image.color = buttonColor;
            image.sprite = buttonImage;

        }
    }

    private void OnValidate()
    {
        SetButton();
    }
}
