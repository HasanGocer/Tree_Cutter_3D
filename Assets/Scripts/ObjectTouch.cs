using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTouch : MonoBehaviour
{
    public bool isTouch;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTouch)
        {
            other.GetComponent<ObjectTouch>().isTouch = true;
            isTouch = true;
            ObjectID objectID = other.GetComponent<ObjectID>();

            RandomSystem.Instance.ObjectPoolAdd(other.gameObject);
            RandomSystem.Instance.ObjectPoolAdd(this.gameObject);
            RandomSystem.Instance.NewObjectUgrade(objectID.objectID, objectID.lineCount, objectID.ColumnCount);
        }
    }
}
