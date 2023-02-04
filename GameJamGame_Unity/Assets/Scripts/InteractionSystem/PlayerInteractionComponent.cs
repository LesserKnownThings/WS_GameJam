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
        
        // Start is called before the first frame update
        void Start()
        {
            _mask = LayerMask.GetMask("Interactable");
            _inputManager = World.Instance.GetInputManager();

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
            if (Input.GetButtonDown("Jump"))
            {
                TryInteracting();
            }

            var movementVector = _inputManager.movementVector;
            if (movementVector != Vector2.zero)
            {
                direction = movementVector;
            
            }
        }

        private IInteractable interactable;
        void TryInteracting()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance, _mask);
            
            if (hit.collider == null)
            {
                return;
            }
            
            interactable = hit.collider.GetComponent<IInteractable>();
            interactable?.Interact();
        }

        void StopInteracting()
        {
            interactable?.StopInteract();
        }
    }
}
