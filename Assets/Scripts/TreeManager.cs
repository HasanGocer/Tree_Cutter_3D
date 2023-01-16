using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoSingleton<TreeManager>
{
    public List<GameObject> Trees = new List<GameObject>();
    public List<GameObject> Axes = new List<GameObject>();

    public void StartTreeManager()
    {
        AnimController.Instance.focusTree = TreeManager.Instance.Trees[ItemData.Instance.factor.maxTreeHealth / 10];
        Trees[ItemData.Instance.factor.maxTreeHealth % TreeManager.Instance.Trees.Count].SetActive(true);
    }

    public void AllAxeClose()
    {
        foreach (GameObject i in Axes)
            i.SetActive(false);
    }

}
