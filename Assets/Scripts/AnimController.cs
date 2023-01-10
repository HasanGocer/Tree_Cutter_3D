using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class AnimController : MonoSingleton<AnimController>
{
    [SerializeField] private AnimancerComponent character;
    [SerializeField] private AnimationClip cut;

    public void CallIdleAnim()
    {
        character.Play(cut, 0.2f);
    }
}
