using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridID : MonoBehaviour
{
    public int lineCount, columnCount;

    private void OnTriggerEnter(Collider other)
    {
        RandomSystem.Instance.ObjectNewPlacement(other.gameObject, lineCount, columnCount);
    }
}
