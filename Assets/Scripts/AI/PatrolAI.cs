using System.Collections;
using System.Collections.Specialized;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AIMovement))]
public class PatrolAI : MonoBehaviour
{
    public Vector2 maxRange;
    private Vector2 startingPosition;
    public Vector2 newPosition;
    private AIMovement aiMovement;

    private bool isMoving;
    private void Start()
    {
        startingPosition = transform.position;
        aiMovement = GetComponent<AIMovement>();
    }


    public void GetRandomPositionInRange()
    {
        newPosition = new Vector2(Random.Range(startingPosition.x - maxRange.x, startingPosition.x + maxRange.x),
            Random.Range(startingPosition.y - maxRange.y, startingPosition.y + maxRange.y));

        aiMovement.InitializeMovement(newPosition);
    }



}
