using UnityEngine;

public class Treasure : MonoBehaviour, IInteractable
{
    public int treasureID;
    public int goldValue = 10;
    public int mapID;

    public void Interact()
    {
        if (PlayerStats.Instance != null)
            PlayerStats.Instance.AddGold(goldValue);

        GameStateManager.Instance.RegisterTreasureCollected(mapID, treasureID);

        gameObject.SetActive(false);
    }
}
