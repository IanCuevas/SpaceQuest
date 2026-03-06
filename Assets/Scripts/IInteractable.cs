using UnityEngine;

public interface IInteractable
{
    public void Interact();
}

public interface IDamageable
{
    public void TakeDamage(float damageAmount);
}