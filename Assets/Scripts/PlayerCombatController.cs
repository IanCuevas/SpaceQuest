using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatController : MonoBehaviour
{
    public InputAction attackInput;
    public Transform attackSpawn;
    public GameObject attackPrefab;

    private void Start()
    {
        attackInput.Enable();
        attackInput.performed += Attack;
    }
    public void Attack(InputAction.CallbackContext context)
    {
        Instantiate(attackPrefab, attackSpawn.position, Quaternion.identity);
    }
}
