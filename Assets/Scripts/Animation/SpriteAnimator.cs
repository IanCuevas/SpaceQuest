using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SpriteAnimator : MonoBehaviour
{
    private SpriteRenderer sr;
    bool isPlaying = false;
    public UnityEvent OnAnimationComplete;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

    }

    public void InitializeAnimation(AnimationData animationData)
    {

        StopAllCoroutines();
        StartCoroutine(PlayAnimation(animationData));
    }

    private IEnumerator PlayAnimation(AnimationData animation)
    {
        isPlaying = true;
        sr.sprite = animation.frames[0];
        int frameCount = animation.frames.Length;
        int frameIndex = 0;

        while (isPlaying)
        {
            yield return new WaitForSeconds(animation.frameDelay);
            frameIndex++;
            if (frameIndex >= animation.frames.Length)
            {
                if (animation.loop)
                {
                    frameIndex = 0;
                }
                else
                {
                    isPlaying = false;
                    OnAnimationComplete?.Invoke();
                    break;
                }

            }
            sr.sprite = animation.frames[frameIndex];


            yield return null;
        }
        yield return null;
    }

    public void StopPlaying()
    {
        isPlaying = false;
    }
}
