using System.Collections.Generic;
using UnityEngine;

public class InstantiationHelper : MonoBehaviour
{
    public GameObject HelperInstantiate(GameObject prefab)
    {
        return Instantiate(prefab);
    }
    public  GameObject HelperInstantiatePrefab(GameObject prefab, Transform parent)
    {
        return GameObject.Instantiate(prefab, parent);
    }
    public  T[] HelperFindObjectsOfType<T>() where T : Object
    {
        return GameObject.FindObjectsOfType<T>();
    }
    public  void HelperDestroy(GameObject prefab,float s)
    {
         Destroy(prefab,s);
    }
     public  void AddItems(GameObject prefab)
    {
        affiliatedItems.Add(prefab);
    }
    public List<GameObject> affiliatedItems=new List<GameObject>();
    private void OnDestroy() 
    {
        foreach(GameObject gameObject in affiliatedItems )
        {
            Destroy(gameObject);
        }
    }
         
}