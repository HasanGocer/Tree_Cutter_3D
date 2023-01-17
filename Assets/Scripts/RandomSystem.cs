using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class RandomSystem : MonoSingleton<RandomSystem>
{
    [System.Serializable]
    public class RandomField
    {
        public bool[] ObjectGrid;
        public int[] ObjectInt;
        public GameObject[] objectGO;
    }

    [System.Serializable]
    public class Arrays
    {
        public List<RandomField> randomFields;
        //public int EquipInt;
        // public bool EquipBool;
        //public GameObject EquipObjectGO;
    }
    public Arrays arrays;

    public List<GameObject> ObjectList = new List<GameObject>();
    [SerializeField] private GameObject _objectPosTemplate;
    // [SerializeField] private GameObject _objectEquipPosTemplate;
    [SerializeField] private int _OPObjectCount;
    [SerializeField] private int _xDÝstance, _zDÝstance;
    [SerializeField] private float _scale;


    public void BackToThePlace(GameObject obj)
    {
        ObjectID objectID = obj.GetComponent<ObjectID>();

        obj.transform.position = new Vector3(_objectPosTemplate.transform.position.x + objectID.ColumnCount * _scale, _objectPosTemplate.transform.position.y + 1, _objectPosTemplate.transform.position.z + objectID.lineCount * _scale);

        GameManager.Instance.RandomPlacementWrite(RandomSystem.Instance.arrays);
    }

    public void ObjectNewPlacement(GameObject obj, int lineCount, int columnCount)
    {
        ObjectID objectID = obj.GetComponent<ObjectID>();

        arrays.randomFields[objectID.lineCount].objectGO[objectID.ColumnCount].GetComponent<GridID>().isFull = false;
        arrays.randomFields[objectID.lineCount].ObjectGrid[objectID.ColumnCount] = false;
        arrays.randomFields[objectID.lineCount].ObjectInt[objectID.ColumnCount] = 0;

        objectID.lineCount = lineCount;
        objectID.ColumnCount = columnCount;

        arrays.randomFields[objectID.lineCount].objectGO[objectID.ColumnCount].GetComponent<GridID>().isFull = true;
        arrays.randomFields[objectID.lineCount].ObjectGrid[objectID.ColumnCount] = true;
        arrays.randomFields[objectID.lineCount].ObjectInt[objectID.ColumnCount] = objectID.objectID;
    }

    public void ObjectShake(GameObject obj)
    {
        obj.transform.DOShakeScale(0.2f);
    }

    public void ObjectPoolAdd(GameObject obj)
    {
        ObjectID objectID = obj.GetComponent<ObjectID>();

        arrays.randomFields[objectID.lineCount].ObjectGrid[objectID.ColumnCount] = false;
        arrays.randomFields[objectID.lineCount].ObjectInt[objectID.ColumnCount] = 0;

        ObjectPool.Instance.AddObject(_OPObjectCount + objectID.objectID - 1, obj);
        ObjectList.Remove(obj);
    }
    public Vector3 CallPosition(int lineCount, int columnCount)
    {
        return new Vector3(_objectPosTemplate.transform.position.x + columnCount * _scale, _objectPosTemplate.transform.position.y + 1f, _objectPosTemplate.transform.position.z + lineCount * _scale);
    }

    public void NewObjectUgrade(int ID, int xDÝstance, int zDistance)
    {
        GameObject obj = GetObject(_OPObjectCount + ID);
        AddList(obj, ObjectList);
        ObjectIDPlacement(ID, obj, ObjectList, true);
        ObjectPositionPlacement(obj, _objectPosTemplate, ID + 1, xDÝstance, zDistance, true);
        AxeChenge();
    }
    public void NewObjectSpawn()
    {
        NewObject(_OPObjectCount, _xDÝstance, _zDÝstance, _objectPosTemplate, ObjectList);
        AxeChenge();
        GameManager.Instance.RandomPlacementWrite(RandomSystem.Instance.arrays);
    }
    public void StartObject()
    {
        for (int i1 = 0; i1 < 5; i1++)
            for (int i2 = 0; i2 < 5; i2++)
                if (arrays.randomFields[i1].ObjectGrid[i2])
                {
                    objectPlacement(_OPObjectCount, arrays.randomFields[i1].ObjectInt[i2], i1, i2, _objectPosTemplate, ObjectList);
                    arrays.randomFields[i1].objectGO[i2].GetComponent<GridID>().isFull = true;
                }
        AxeChenge();
    }

    private void AxeChenge()
    {
        TreeManager.Instance.AllAxeClose();
        int temp = 0;
        for (int i1 = 0; i1 < 5; i1++)
            for (int i2 = 0; i2 < 5; i2++)
                if (RandomSystem.Instance.arrays.randomFields[i1].ObjectGrid[i2])
                    if (RandomSystem.Instance.arrays.randomFields[i1].ObjectInt[i2] > temp)
                        temp = RandomSystem.Instance.arrays.randomFields[i1].ObjectInt[i2];

        TreeManager.Instance.Axes[temp - 1].SetActive(true);
    }
    private void objectPlacement(int OPObjectCount, int ID, int xDÝstance, int zDistance, GameObject objectPosTemplate, List<GameObject> objects)
    {
        GameObject obj = GetObject(OPObjectCount + ID - 1);
        AddList(obj, objects);
        ObjectIDPlacement(ID, obj, objects, false);
        ObjectPositionPlacement(obj, objectPosTemplate, ID, xDÝstance, zDistance, false);
    }
    private void NewObject(int OPObjectCount, int xDÝstance, int zDistance, GameObject objectPosTemplate, List<GameObject> objects)
    {
        int ID = 1;
        GameObject obj = GetObject(OPObjectCount);
        AddList(obj, objects);
        ObjectIDPlacement(ID, obj, objects, false);
        ObjectPositionRandomPlacement(obj, objectPosTemplate, xDÝstance, zDistance);
    }
    private GameObject GetObject(int OPObjectCount)
    {
        return ObjectPool.Instance.GetPooledObject(OPObjectCount);
    }
    private void AddList(GameObject obj, List<GameObject> objects)
    {
        objects.Add(obj);
    }
    private void ObjectIDPlacement(int ID, GameObject obj, List<GameObject> objects, bool isNewPlace)
    {
        ObjectID objectID = obj.GetComponent<ObjectID>();

        if (isNewPlace)
            objectID.objectID = ID + 1;
        else
            objectID.objectID = ID;

        objectID.ListCount = objects.Count - 1;
        obj.layer = default;
    }
    private void ObjectPositionRandomPlacement(GameObject obj, GameObject objectPosTemplate, int xDistance, int zDistance)
    {
        int tempX = Random.Range(0, xDistance);
        int tempZ = Random.Range(0, zDistance);
        bool isFull = false;
        if (arrays.randomFields[tempX].ObjectGrid[tempZ] == true)
            isFull = true;
        if (!isFull)
        {
            ObjectID objectID = obj.GetComponent<ObjectID>();
            objectID.lineCount = tempX;
            objectID.ColumnCount = tempZ;
            arrays.randomFields[tempX].objectGO[tempZ].GetComponent<GridID>().isFull = true;
            arrays.randomFields[tempX].ObjectGrid[tempZ] = true;
            arrays.randomFields[tempX].ObjectInt[tempZ] = 1;
            obj.transform.position = new Vector3(objectPosTemplate.transform.position.x + tempZ * _scale, objectPosTemplate.transform.position.y + 1f, objectPosTemplate.transform.position.z + tempX * _scale);
        }
        else
            ObjectPositionRandomPlacement(obj, objectPosTemplate, xDistance, zDistance);
    }
    private void ObjectPositionPlacement(GameObject obj, GameObject objectPosTemplate, int ID, int tempX, int tempZ, bool isNewPlace)
    {
        ObjectID objectID = obj.GetComponent<ObjectID>();
        objectID.lineCount = tempX;
        objectID.ColumnCount = tempZ;
        obj.transform.position = new Vector3(objectPosTemplate.transform.position.x + tempZ * _scale, objectPosTemplate.transform.position.y + 1f, objectPosTemplate.transform.position.z + tempX * _scale);

        if (isNewPlace)
        {
            arrays.randomFields[tempX].ObjectGrid[tempZ] = true;
            arrays.randomFields[tempX].ObjectInt[tempZ] = ID;
        }
    }
}
