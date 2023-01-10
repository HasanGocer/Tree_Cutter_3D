using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RandomSystem : MonoSingleton<RandomSystem>
{
    [System.Serializable]
    public class Arrays
    {
        public bool[,] ObjectGrid = new bool[5, 5];
        public int[,] ObjectInt = new int[5, 5];
        public int EquipInt;
        public bool EquipBool;
    }
    public Arrays arrays = new Arrays();

    public List<GameObject> ObjectList = new List<GameObject>();
    [SerializeField] private GameObject _objectPosTemplate;
    [SerializeField] private GameObject _objectEquipPosTemplate;
    [SerializeField] private int _OPObjectCount;
    [SerializeField] private int _xDÝstance, _zDÝstance;
    [SerializeField] private float _scale;

    public void BackToThePlace(GameObject obj)
    {
        ObjectID objectID = obj.GetComponent<ObjectID>();
        if (objectID.lineCount != 6)
            obj.transform.position = new Vector3(_objectPosTemplate.transform.position.x + objectID.lineCount * _scale, _objectPosTemplate.transform.position.y, _objectPosTemplate.transform.position.z + objectID.ColumnCount * _scale);
        else
            obj.transform.position = _objectEquipPosTemplate.transform.position;
    }

    public void ObjectNewPlacement(GameObject obj, int lineCount, int columnCount)
    {
        ObjectID objectID = obj.GetComponent<ObjectID>();
        objectID.lineCount = lineCount;
        objectID.ColumnCount = columnCount;
        if (lineCount == 6)
            RandomSystem.Instance.arrays.EquipInt = obj.GetComponent<ObjectID>().objectID;
    }

    public void ObjectShake(GameObject obj)
    {
        obj.transform.DOShakeScale(0.2f);
    }

    public void ObjectPoolAdd(GameObject obj)
    {
        ObjectID objectID = obj.GetComponent<ObjectID>();
        ObjectList.RemoveAt(objectID.ListCount);
        arrays.ObjectInt[objectID.lineCount, objectID.ColumnCount] = 0;
        arrays.ObjectGrid[objectID.lineCount, objectID.ColumnCount] = false;
        ObjectPool.Instance.AddObject(_OPObjectCount, obj);
    }

    public void NewObjectSpawn()
    {
        NewObject(_OPObjectCount, _xDÝstance, _zDÝstance, _objectPosTemplate, ObjectList);
    }
    public void StartObject()
    {
        for (int i1 = 0; i1 < 5; i1++)
        {
            for (int i2 = 0; i2 < 5; i2++)
            {
                if (arrays.ObjectGrid[i1, i2])
                    objectPlacement(_OPObjectCount, arrays.ObjectInt[i1, i2], i1, i2, _objectPosTemplate, ObjectList);
            }
        }
        if (arrays.EquipBool)
            objectEquipPlacement(_OPObjectCount, arrays.EquipInt, _objectEquipPosTemplate, ObjectList);
    }

    private void objectEquipPlacement(int OPObjectCount, int ID, GameObject objectPosTemplate, List<GameObject> objects)
    {
        GameObject obj = GetObject(OPObjectCount);
        AddList(obj, objects);
        ObjectIDPlacement(ID, obj, objects);
        MaterialPlacement(obj, ID);
        ObjectPositionEquipPlacement(obj, objectPosTemplate);
    }
    private void objectPlacement(int OPObjectCount, int ID, int xDÝstance, int zDistance, GameObject objectPosTemplate, List<GameObject> objects)
    {
        GameObject obj = GetObject(OPObjectCount);
        AddList(obj, objects);
        ObjectIDPlacement(ID, obj, objects);
        MaterialPlacement(obj, ID);
        ObjectPositionPlacement(obj, objectPosTemplate, xDÝstance, zDistance);
    }
    private void NewObject(int OPObjectCount, int xDÝstance, int zDistance, GameObject objectPosTemplate, List<GameObject> objects)
    {
        int ID = 1;
        GameObject obj = GetObject(OPObjectCount);
        AddList(obj, objects);
        ObjectIDPlacement(ID, obj, objects);
        MaterialPlacement(obj, ID);
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
    private void ObjectIDPlacement(int ID, GameObject obj, List<GameObject> objects)
    {
        ObjectID objectID = obj.GetComponent<ObjectID>();

        objectID.objectID = ID;
        objectID.ListCount = objects.Count - 1;
        obj.layer = default;

    }
    private void MaterialPlacement(GameObject obj, int ID)
    {
        obj.GetComponent<MeshRenderer>().material = MaterialSystem.Instance.Materials[ID];
    }
    private void ObjectPositionRandomPlacement(GameObject obj, GameObject objectPosTemplate, int xDistance, int zDistance)
    {
        int tempX = Random.Range(0, xDistance);
        int tempZ = Random.Range(0, zDistance);
        bool isFull = false;
        if (arrays.ObjectGrid[tempX, tempZ] == true)
            isFull = true;
        if (!isFull)
        {
            ObjectID objectID = obj.GetComponent<ObjectID>();
            objectID.lineCount = tempX;
            objectID.ColumnCount = tempZ;
            arrays.ObjectGrid[tempX, tempZ] = true;
            arrays.ObjectInt[tempX, tempZ] = 1;
            obj.transform.position = new Vector3(objectPosTemplate.transform.position.x + tempX * _scale, objectPosTemplate.transform.position.y, objectPosTemplate.transform.position.z + tempZ * _scale);
        }
        else
            ObjectPositionRandomPlacement(obj, objectPosTemplate, xDistance, zDistance);
    }
    private void ObjectPositionPlacement(GameObject obj, GameObject objectPosTemplate, int tempX, int tempZ)
    {
        ObjectID objectID = obj.GetComponent<ObjectID>();
        objectID.lineCount = tempX;
        objectID.ColumnCount = tempZ;
        obj.transform.position = new Vector3(objectPosTemplate.transform.position.x + tempX * _scale, objectPosTemplate.transform.position.y, objectPosTemplate.transform.position.z + tempZ * _scale);
    }
    private void ObjectPositionEquipPlacement(GameObject obj, GameObject objectPosTemplate)
    {
        ObjectID objectID = obj.GetComponent<ObjectID>();
        objectID.lineCount = 6;
        objectID.ColumnCount = 6;
        obj.transform.position = objectPosTemplate.transform.position;
    }
}
