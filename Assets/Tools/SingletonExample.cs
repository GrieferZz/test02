using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SingletonExample : MonoBehaviour
{
    private static Dictionary<string, SingletonExample> _instances = new Dictionary<string, SingletonExample>();

    public static SingletonExample GetInstance(string id)
{
    if (!_instances.ContainsKey(id))
    {
        GameObject instanceObject = new GameObject("SingletonExample_" + id);
        SingletonExample instance = instanceObject.AddComponent<SingletonExample>();
        _instances.Add(id, instance);
        DontDestroyOnLoad(instanceObject); // 将该实例标记为不可销毁
    }

    return _instances[id];
}

    private void Awake()
    {
        if (!_instances.ContainsKey(this.gameObject.name))
        {
            _instances.Add(this.gameObject.name, this);
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}