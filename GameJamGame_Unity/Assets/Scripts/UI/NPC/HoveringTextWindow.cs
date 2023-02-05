using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoveringTextWindow : HUDWindow
{
    [SerializeField]
    private TextMeshProUGUI textToDisplay;
    [SerializeField]
    private Image requirementIcon;
    [SerializeField]
    private float messageOnScreenTime = 2.0f;
    [SerializeField]
    private Transform textToPlace;

    private Camera cam;
    private Coroutine showMessageRoutine;

    public override void InitHUDWindow(IHUDInteractor interactor)
    {
        base.InitHUDWindow(interactor);

        NPCInteractionComponent interactionComponent = (NPCInteractionComponent)interactor;

        if(interactionComponent != null)
        {
            interactionComponent.OnFailedAction += OnFailedNPCInteraction;
        }

        cam = FindObjectOfType<Camera>();

        gameObject.SetActive(false);
    }

    private void OnFailedNPCInteraction(string failMessage, Sprite requirementSprite, Transform hoveringObject)
    {
        if (showMessageRoutine == null)
        {
            gameObject.SetActive(true);
            showMessageRoutine = StartCoroutine(TrackObjPos(hoveringObject));

            textToDisplay.text = failMessage;
            requirementIcon.sprite = requirementSprite;

            Invoke("CloseMessage", messageOnScreenTime);
        }
    }

    private void CloseMessage()
    {
        gameObject.SetActive(false);
        showMessageRoutine = null;
    }

    private IEnumerator TrackObjPos(Transform trackingObj)
    {
        while(gameObject.activeSelf)
        {
            if(cam != null)
            {
                Vector3 newPos = cam.WorldToScreenPoint(trackingObj.position);
                textToPlace.position = newPos;
            }
            yield return null;
        }
    }
}