using UnityEngine;

namespace InteractionSystem
{
    public class TestInteractable : MonoBehaviour, IInteractable
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void Interact()
        {
            Debug.Log("Test Interactable has been interacted with");
        }

        public void StopInteract()
        {
            Debug.Log("Test Interactable has stop being interacted with");
        }
    }
}
