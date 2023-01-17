using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
                other.GetComponent<ObjectTouch>().isTouch = true;
                isTouch = true;
                ObjectID objectID = other.GetComponent<ObjectID>();

                Vibration.Vibrate(30);
                StartCoroutine(ParticalSystem.Instance.CallMergePartical(this.gameObject));
                StartCoroutine(ParticalSystem.Instance.CallMergePartical(other.gameObject));
                Shake(other.gameObject);
                Shake(this.gameObject);

                RandomSystem.Instance.ObjectPoolAdd(other.gameObject);
                RandomSystem.Instance.ObjectPoolAdd(this.gameObject);
                RandomSystem.Instance.NewObjectUgrade(objectID.objectID, objectID.lineCount, objectID.ColumnCount);
                GameManager.Instance.RandomPlacementWrite(RandomSystem.Instance.arrays);
            }
        }
    }

    private void Shake(GameObject obj)
    {
        obj.transform.DOShakeScale(1, 0.6f);
    }
}
