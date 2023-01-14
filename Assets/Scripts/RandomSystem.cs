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
        public int EquipInt;
        public bool EquipBool;
        public GameObject EquipObjectGO;
    }
    public Arrays arrays;

    public List<GameObject> ObjectList = new List<GameObject>();
    [SerializeField] private GameObject _objectPosTemplate;
    [SerializeField] private GameObject _objectEquipPosTemplate;
    [SerializeField] private int _OPObjectCount;
    [SerializeField] private int _xD�stance, _zD�stance;
    [SerializeField] private float _scale;


    public void BackToThePlace(GameObject obj)
    {
        ObjectID objectID = obj.GetComponent<ObjectID>();
        if (objectID.lineCount != 6)
            obj.transform.position = new Vector3(_objectPosTemplate.transform.position.x + objectID.ColumnCount * _scale, _objectPosTemplate.transform.position.y + 1, _objectPosTemplate.transform.position.z + objectID.lineCount * _scale);
        else
        {
            obj.transform.position = _objectEquipPosTemplate.transform.position;
            obj.transform.position += new Vector3(0, 1, 0);
        }
        GameManager.Instance.RandomPlacementWrite(RandomSystem.Instance.arrays);
    }

    public void ObjectNewPlacement(GameObject obj, int lineCount, int columnCount)
    {
        ObjectID objectID = obj.GetComponent<ObjectID>();

        if (objectID.lineCount != 6)
        {
            arrays.randomFields[objectID.lineCount].objectGO[objectID.ColumnCount].GetComponent<GridID>().isFull = false;
            arrays.randomFields[objectID.lineCount].ObjectGrid[objectID.ColumnCount] = false;
            arrays.randomFields[objectID.lineCount].ObjectInt[objectID.ColumnCount] = 0;
        }
        else
        {
            arrays.EquipObjectGO.GetComponent<GridID>().isFull = false;
            arrays.EquipBool = false;
            arrays.EquipInt = 0;
        }
        objectID.lineCount = lineCount;
        objectID.ColumnCount = columnCount;


        if (lineCount == 6)
        {
            RandomSystem.Instance.arrays.EquipInt = obj.GetComponent<ObjectID>().objectID;
            arrays.EquipObjectGO.GetComponent<GridID>().isFull = true;
            GameSystem.Instance.focusObjectID = objectID;
            arrays.EquipBool = true;
            arrays.EquipInt = objectID.objectID;
        }
        else
        {
            arrays.randomFields[objectID.lineCount].objectGO[objectID.ColumnCount].GetComponent<GridID>().isFull = true;
            arrays.randomFields[objectID.lineCount].ObjectGrid[objectID.ColumnCount] = true;
            arrays.randomFields[objectID.lineCount].ObjectInt[objectID.ColumnCount] = objectID.objectID;
            TreeManager.Instance.AllAxeClose();
            TreeManager.Instance.Axes[objectID.objectID].SetActive(true);
        }
    }

    public void ObjectShake(GameObject obj)
    {
        obj.transform.DOShakeScale(0.2f);
    }

    public void ObjectPoolAdd(GameObject obj)
    {
        ObjectID objectID = obj.GetComponent<ObjectID>();
        ObjectList.Remove(obj);
        arrays.randomFields[objectID.lineCount].ObjectGrid[objectID.ColumnCount] = false;
        arrays.randomFields[objectID.lineCount].ObjectInt[objectID.ColumnCount] = 0;
        ObjectPool.Instance.AddObject(_OPObjectCount, obj);
    }
    public Vector3 CallPosition(int lineCount, int columnCount)
    {
        if (lineCount != 6)
            return new Vector3(_objectPosTemplate.transform.position.x + columnCount * _scale, _objectPosTemplate.transform.position.y + 1f, _objectPosTemplate.transform.position.z + lineCount * _scale);
        else
            return new Vector3(_objectEquipPosTemplate.transform.position.x, _objectEquipPosTemplate.transform.position.y + 1, _objectEquipPosTemplate.transform.position.z);
    }

    public void NewObjectUgrade(int ID, int xD�stance, int zDistance)
    {
        GameObject obj = GetObject(_OPObjectCount + ID);
        AddList(obj, ObjectList);
        ObjectIDPlacement(ID, obj, ObjectList, true);
        ObjectPositionPlacement(obj, _objectPosTemplate, ID + 1, xD�stance, zDistance, true);
    }
    public void NewObjectSpawn()
    {
        NewObject(_OPObjectCount, _xD�stance, _zD�stance, _objectPosTemplate, ObjectList);
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

        if (arrays.EquipBool)
        {
            objectEquipPlacement(_OPObjectCount, arrays.EquipInt, _objectEquipPosTemplate, ObjectList);
            arrays.EquipObjectGO.GetComponent<GridID>().isFull = true;
        }
    }

    private void objectEquipPlacement(int OPObjectCount, int ID, GameObject objectPosTemplate, List<GameObject> objects)
    {
        GameObject obj = GetObject(OPObjectCount);
        AddList(obj, objects);
        ObjectIDPlacement(ID, obj, objects, false);
        ObjectPositionEquipPlacement(obj, objectPosTemplate);
    }
    private void objectPlacement(int OPObjectCount, int ID, int xD�stance, int zDistance, GameObject objectPosTemplate, List<GameObject> objects)
    {
        GameObject obj = GetObject(OPObjectCount + ID - 1);
        AddList(obj, objects);
        ObjectIDPlacement(ID, obj, objects, false);
        ObjectPositionPlacement(obj, objectPosTemplate, ID, xD�stance, zDistance, false);
    }
    private void NewObject(int OPObjectCount, int xD�stance, int zDistance, GameObject objectPosTemplate, List<GameObject> objects)
    {
        int ID = 1;
        GameObject obj = GetObject(OPObjectCount);
        AddList(obj, objects);
        ObjectIDPlacement(ID, obj, objects, false);
        ObjectPositionRandomPlacement(obj, objectPosTemplate, xD�stance, zDistance);
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
        if (tempX != 6)
        {
            obj.transform.position = new Vector3(objectPosTemplate.transform.position.x + tempZ * _scale, objectPosTemplate.transform.position.y + 1f, objectPosTemplate.transform.position.z + tempX * _scale);

            if (isNewPlace)
            {
                arrays.randomFields[tempX].ObjectGrid[tempZ] = true;
                arrays.randomFields[tempX].ObjectInt[tempZ] = ID;
            }
        }
        else
        {
            GameSystem.Instance.focusObjectID = objectID;
            if (isNewPlace)
            {
                arrays.EquipBool = true;
                arrays.EquipInt = ID;
            }
            obj.transform.position = _objectEquipPosTemplate.transform.position;
            obj.transform.position += new Vector3(0, 1, 0);
        }
    }
    private void ObjectPositionEquipPlacement(GameObject obj, GameObject objectPosTemplate)
    {
        ObjectID objectID = obj.GetComponent<ObjectID>();
        GameSystem.Instance.focusObjectID = objectID;
        objectID.lineCount = 6;
        objectID.ColumnCount = 6;
        obj.transform.position = objectPosTemplate.transform.position;
        obj.transform.position += new Vector3(0, 1, 0);
        TreeManager.Instance.AllAxeClose();
        TreeManager.Instance.Axes[objectID.objectID].SetActive(true);
    }
}
