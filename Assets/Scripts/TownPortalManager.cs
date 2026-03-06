using UnityEngine;

public class TownPortalManager : MonoBehaviour
{
    public int targetMapID;

    public int targetEntryPointID;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (MapNavigation.Instance != null)
            MapNavigation.Instance.GoToMap(targetMapID, targetEntryPointID);
        else
            Debug.LogWarning("[TownPortalManager] MapNavigation.Instance is null. Is the MainGame scene loaded?");
    }
}
