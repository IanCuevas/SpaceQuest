using UnityEngine;

public class Inn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Rest()
    {
        GameStateManager.Instance.ResetEnemies();
    }
}
