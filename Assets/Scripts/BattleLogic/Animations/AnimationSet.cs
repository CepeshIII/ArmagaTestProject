using UnityEngine;

[CreateAssetMenu(fileName = "AnimationSet", menuName = "Scriptable Objects/AnimationSet")]
public class AnimationSet : ScriptableObject
{
    public AnimationType animationType;

    public AnimationClip clip0;
    public AnimationClip clip90;
    public AnimationClip clip180;
    public AnimationClip clip270;
}
