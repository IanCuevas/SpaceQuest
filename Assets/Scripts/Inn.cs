using UnityEngine;

public class Inn : MonoBehaviour
{
    public void Rest()
    {
        GameStateManager.Instance.ResetAllEnemies();

        if (PlayerStats.Instance != null)
            PlayerStats.Instance.RestoreFullHealth();
    }
}
