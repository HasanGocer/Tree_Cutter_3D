using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animancer;

public class AnimController : MonoSingleton<AnimController>
{
    [SerializeField] private AnimancerComponent character;
    [SerializeField] private AnimationClip cut;
    public GameObject focusTree;

    public void CallIdleAnim()
    {
        StartCoroutine(TreeShake());
        character.Play(cut, 0.2f);
    }

    private IEnumerator TreeShake()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.7f);
            StartCoroutine(ParticalSystem.Instance.CallTreeHitPartical());
            Shake(focusTree);
            yield return new WaitForSeconds(1.8f);
        }
    }
    private void Shake(GameObject obj)
    {
        obj.transform.DOShakeScale(1, 0.02f);
    }

}
