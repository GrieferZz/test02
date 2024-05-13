using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ExplostionCheck : MonoBehaviour
{
    //public List<GameObject>Enermies=new List<GameObject>();
    
    // Start is called before the first frame update
    void OnEnable()
    {
       GetComponent<SphereCollider>().enabled=false;
       
       StartCoroutine(LifeCycle(0.5f));
       StartCoroutine(TargetCycle());
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) 
    {
       GameEventSystem.instance.TargetTransmission(transform.parent.gameObject,other.gameObject);
       Debug.Log("爆炸检测"+other.gameObject);
    }
    IEnumerator LifeCycle(float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // 等待指定的延迟时间

        // 在这里编写需要延迟执行的函数逻辑
        
        Destroy(transform.parent.gameObject);
    }
    IEnumerator TargetCycle()
    {

        //yield return new WaitForSeconds(0.1f); // 等待指定的延迟时间

        // 在这里编写需要延迟执行的函数逻辑
        GetComponent<SphereCollider>().enabled=true;
        yield return new WaitForSeconds(0.4f);
        GetComponent<SphereCollider>().enabled=false;
    }
    
}
