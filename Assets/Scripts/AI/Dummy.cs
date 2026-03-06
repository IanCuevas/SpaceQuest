using UnityEngine;

public class Dummy : MonoBehaviour, IDamageable, IInteractable
{
    public void Interact()
    {
        // display dialog line
    }

    public void TakeDamage(float damageAmount)
    {
        // hp -= damageAmount
    }

    /*public void Explosion()
    {
        GameObject[] inRadius;

        foreach(GameObject g in inRadius)
        {
            if(g.GetComponent<IDamageable>() != null)
            {
                g.GetComponent<IDamageable>().TakeDamage(50f);
            }
        }
    }*/
}
