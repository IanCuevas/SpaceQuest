using UnityEngine;
using UnityEngine.Events;

public class InteractableItem : MonoBehaviour
{
    public UnityEvent OnInteract;

    public void Interact()
    {
        OnInteract?.Invoke();
    }
}
