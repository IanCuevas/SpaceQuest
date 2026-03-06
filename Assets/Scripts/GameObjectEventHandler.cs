using UnityEngine;
using UnityEngine.Events;

public class GameObjectEventHandler : MonoBehaviour
{
    public UnityEvent OnEnabled;

    public void Start()
    {
        OnEnabled?.Invoke();
    }

    public void DestroyGameobject()
    {
        Destroy(this.gameObject);
    }
}
