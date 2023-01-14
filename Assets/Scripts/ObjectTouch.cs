using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTouch : MonoBehaviour
{
    public bool isTouch;
    [SerializeField] private ObjectID objectID;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTouch && other.CompareTag("Axe"))
        {
            if (other.GetComponent<ObjectID>().objectID == objectID.objectID)
            {
                print(1);
                other.GetComponent<ObjectTouch>().isTouch = true;
                print(2);
                isTouch = true;
                print(2);
                ObjectID objectID = other.GetComponent<ObjectID>();
                print(3);

                RandomSystem.Instance.ObjectPoolAdd(other.gameObject);
                print(4);
                RandomSystem.Instance.ObjectPoolAdd(this.gameObject);
                print(5);
                RandomSystem.Instance.NewObjectUgrade(objectID.objectID, objectID.lineCount, objectID.ColumnCount);
                print(6);
                GameManager.Instance.RandomPlacementWrite(RandomSystem.Instance.arrays);
                print(7);
            }
        }
    }
}
