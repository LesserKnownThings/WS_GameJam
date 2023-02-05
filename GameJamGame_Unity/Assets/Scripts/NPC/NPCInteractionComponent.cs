using System.Collections;
using UnityEngine;

public delegate void OnFailedNPCActionDelegate(string failMessage, Sprite requirementSprite, Transform hoveringObject);

public class NPCInteractionComponent : MonoBehaviour, INPCRequirement, IHUDInteractor
{
    [SerializeField]
    private string requiredItemId;
    [SerializeField]
    private string failMessage = "It looks like you're missing ";
    [SerializeField]
    private string firstInteractMessage = "Hello if you want me to open this door I will require a";
    [SerializeField]
    private Sprite requirementSprite;

    [SerializeField]
    private HUDWindow messageWindow;
    [SerializeField]
    private Transform placeToShowMessage;

    public OnFailedNPCActionDelegate OnFailedAction;
    private bool hasAlreadyInteracted = false;

    public MonoBehaviour GetOwner()
    {
        return this;
    }

    private void Start()
    {
        World.Instance.GetHUD().AddSubWindow(messageWindow, this);

        World.Instance.GetInputManager().OnInputTriggered += OnInputTriggered;        
    }

    private void OnInputTriggered(InputActionType type, bool test)
    {
        if(type == InputActionType.Action && test)
        {
            UseItem(null);
        }
    }

    public void UseItem(InventoryComponent playerInventory)
    {
        if(playerInventory != null && playerInventory.TryUseItem(requiredItemId))
        {
            //Do whatever the NPC is supposed to do to unlock something?
        }
        else
        {
            if (!hasAlreadyInteracted)
            {
                hasAlreadyInteracted = true;
                OnFailedAction?.Invoke(firstInteractMessage, requirementSprite, placeToShowMessage);
            }
            else
            {
                OnFailedAction?.Invoke(failMessage, requirementSprite, placeToShowMessage);
            }
        }
    }
}