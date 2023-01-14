using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridID : MonoBehaviour
{
    public int lineCount, columnCount;
    public bool isFull;

    private void OnTriggerEnter(Collider other)
    {
        if (!isFull)
            RandomSystem.Instance.ObjectNewPlacement(other.gameObject, lineCount, columnCount);
    }
}
