using UnityEngine;

[CreateAssetMenu(fileName = "AnimationData", menuName = "Scriptable Objects/AnimationData")]
public class AnimationData : ScriptableObject
{
    public string animationName;
    public Sprite[] frames;
    public float frameDelay;
    public bool loop;
}
