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

        void TryInteracting()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance, _mask);
            
            if (hit.collider == null)
            {
                return;
            }
            
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            interactable?.Interact();
        }
    }
}
