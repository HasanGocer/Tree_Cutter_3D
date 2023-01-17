using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animancer;

public class AnimController : MonoSingleton<AnimController>
{
    [SerializeField] private AnimancerComponent character;
    [SerializeField] private AnimationClip cut, idle;
    [SerializeField] private bool isStay;
    public GameObject focusTree;

    public void CallIdleAnim()
    {
        StartCoroutine(TreeShake());
    }

    private IEnumerator TreeShake()
    {
        if (!isStay)
        {
            character.Play(cut, 0.2f);
            isStay = true;
        }
        yield return new WaitForSeconds(0.7f);
        if (!isStay)
        {
            StartCoroutine(ParticalSystem.Instance.CallTreeHitPartical());
            Shake(focusTree);
        }
        yield return new WaitForSeconds(2.0f);
        if (!isStay)
        {
            //character.Play(idle, 0.2f);
            isStay = false;
        }
    }
    private void Shake(GameObject obj)
    {
        obj.transform.DOShakeScale(1, 0.02f);
    }

}
