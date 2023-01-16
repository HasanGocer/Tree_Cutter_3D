using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animancer;

public class AnimController : MonoSingleton<AnimController>
{
    [SerializeField] private AnimancerComponent character;
    [SerializeField] private AnimationClip cut, idle;
    public GameObject focusTree;

    public void CallIdleAnim()
    {
        StartCoroutine(TreeShake());
    }

    private IEnumerator TreeShake()
    {
            character.Play(cut, 0.2f);
            yield return new WaitForSeconds(0.7f);
            StartCoroutine(ParticalSystem.Instance.CallTreeHitPartical());
            Shake(focusTree);
            yield return new WaitForSeconds(1.6f);
            character.Play(idle, 0.2f);
            yield return new WaitForSeconds(0.2f);
    }
    private void Shake(GameObject obj)
    {
        obj.transform.DOShakeScale(1, 0.02f);
    }

}
