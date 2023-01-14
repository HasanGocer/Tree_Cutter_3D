using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoSingleton<TreeManager>
{
    public List<GameObject> Trees = new List<GameObject>();
    public List<GameObject> Axes = new List<GameObject>();

    public void StartTreeManager()
    {
        Trees[ItemData.Instance.factor.maxTreeHealth / 10].SetActive(true);
    }

    public void AxeChange()
    {
        Axes[ItemData.Instance.factor.axeCount - 2].SetActive(false);
        Axes[ItemData.Instance.factor.axeCount - 1].SetActive(true);
    }
    public void AllAxeClose()
    {
        foreach (GameObject i in Axes)
            i.SetActive(false);
    }

}
