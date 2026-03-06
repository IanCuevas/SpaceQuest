using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TopDownPlayerMovement))]
[RequireComponent(typeof(SpriteAnimator))]
public class PlayerAnimation : MonoBehaviour
{
    public List<AnimationStateData> animations = new List<AnimationStateData>();
    private SpriteAnimator spriteAnimator;
    private PlayerAnimationState currentState = PlayerAnimationState.IDLE_DOWN;
    private Dictionary<PlayerAnimationState, AnimationData> animationDictionary = new Dictionary<PlayerAnimationState, AnimationData>();

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        InitializeDictionary();
        TopDownPlayerMovement playerMovement = GetComponent<TopDownPlayerMovement>();
        playerMovement.OnMove += SetAnimationState;
        spriteAnimator = GetComponent<SpriteAnimator>();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void InitializeDictionary()
    {
        foreach (AnimationStateData animationStateData in animations)
        {
            animationDictionary.Add(animationStateData.state, animationStateData.animation);
        }
    }



    public void SetAnimationState(Vector2 moveDirection)
    {
        if (moveDirection.y < 0)
        {
            currentState = PlayerAnimationState.WALK_DOWN;
            spriteRenderer.flipX = false;
        }
        else if (moveDirection.y > 0)
        {
            currentState = PlayerAnimationState.WALK_UP;
            spriteRenderer.flipX = false;
        }
        else if (moveDirection.x > 0)
        {
            currentState = PlayerAnimationState.WALK_LEFT;
            spriteRenderer.flipX = true;
        }

        else if (moveDirection.x < 0)
        {
            currentState = PlayerAnimationState.WALK_RIGHT;
            spriteRenderer.flipX = false;
        }

        if (moveDirection == Vector2.zero)
        {
            currentState = GetIdleState(currentState);
            if (currentState == PlayerAnimationState.IDLE_RIGHT)
            {
                spriteRenderer.flipX = false;
            }
            else if (currentState == PlayerAnimationState.IDLE_LEFT)
            {
                spriteRenderer.flipX = true;
            }
        }

        spriteAnimator.InitializeAnimation(animationDictionary[currentState]);


    }

    private PlayerAnimationState GetIdleState(PlayerAnimationState currentState)
    {
        PlayerAnimationState state = PlayerAnimationState.IDLE_UP;

        switch (currentState)
        {
            case PlayerAnimationState.WALK_UP:
                state = PlayerAnimationState.IDLE_UP;
                break;
            case PlayerAnimationState.WALK_DOWN:
                state = PlayerAnimationState.IDLE_DOWN;
                break;
            case PlayerAnimationState.WALK_LEFT:
                state = PlayerAnimationState.IDLE_LEFT;
                break;
            case PlayerAnimationState.WALK_RIGHT:
                state = PlayerAnimationState.IDLE_RIGHT;
                break;
            default:
                break;
        }

        return state;

    }


}



public enum PlayerAnimationState
{
    WALK_UP,
    WALK_DOWN,
    WALK_LEFT,
    WALK_RIGHT,
    IDLE_UP,
    IDLE_DOWN,
    IDLE_LEFT,
    IDLE_RIGHT
}

[Serializable]
public class AnimationStateData
{
    public PlayerAnimationState state;
    public AnimationData animation;
}
