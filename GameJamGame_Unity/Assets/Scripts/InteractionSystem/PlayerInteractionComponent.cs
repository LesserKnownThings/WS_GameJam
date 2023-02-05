using System;
using Unity.VisualScripting;
using UnityEngine;

namespace InteractionSystem
{
    public class PlayerInteractionComponent : MonoBehaviour
    {
        [SerializeField] private Vector2 direction = Vector2.down;
        [SerializeField]
        private float raycastDistance = 10.0f;

        private LayerMask _mask;
        
        private InputManager _inputManager;
        private InventoryComponent _inventoryComponent;
        
        // Start is called before the first frame update
        void Start()
        {
            _mask = LayerMask.GetMask("Interactable");
            _inputManager = World.Instance.GetInputManager();
            _inventoryComponent = GetComponent<InventoryComponent>();
            _inputManager.OnInputTriggered += OnInputTriggered;
        }
        private void OnInputTriggered(InputActionType type, bool isPressed)
        {
            switch (type)
            {
                case InputActionType.None:
                    break;
                case InputActionType.LMC:
                    break;
                case InputActionType.RMC:
                    break;
                case InputActionType.MMC:
                    break;
                case InputActionType.Escape:
                    break;
                case InputActionType.Action:
                    if(isPressed)
                    {
                        TryInteracting();
                    }
                    else
                    {
                        
                    }
                    break;
                default:
                    break;
            }
        }
        private void Update()
        {
            var movementVector = _inputManager.movementVector;
            if (movementVector != Vector2.zero)
            {
                direction = movementVector;
            
            }
        }

        private IInteractable interactable;
        void TryInteracting()
        {
            // will only hit gameobjects that are on the Interactable layer
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance, _mask);

            Debug.DrawLine(transform.position, (Vector2) transform.position + direction * raycastDistance, Color.green, 2.0f);
            // Debug.Log("hit collider: " + hit.transform.name);
            if (hit.collider == null)
            {
                return;
            }

            interactable = hit.collider.GetComponent<IInteractable>();
            interactable?.Interact();

            INPCRequirement nPCInteractionComponent = hit.transform.GetComponent<INPCRequirement>();
            if(nPCInteractionComponent != null)
            {
                nPCInteractionComponent.UseItem(_inventoryComponent);
            }
        }

        void StopInteracting()
        {
            interactable?.StopInteract();
        }
    }
}
