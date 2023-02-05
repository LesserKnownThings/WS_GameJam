using System.Collections;
using UnityEngine;

public class FadeTrigger : MonoBehaviour
{
    [SerializeField]
    private Transform newSpawnPosition;
    [SerializeField]
    private LayerMask layerToCheck;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == layerToCheck.value)
        {
            World.Instance.FadeLevel(true, collision.transform, newSpawnPosition.position);
        }
    }
}