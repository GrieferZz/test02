using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotManager : MonoBehaviour
{
    public GameObject target;
    public GameObject creator;
    public float addition;
    public int i;
    Coroutine currentcoroutine;
    
    public void DotUpdate()
    {
        
        if (target!=null&&creator!=null)
        {
           currentcoroutine= StartCoroutine(ExecutePerSecond());
            
    }
    }

    private IEnumerator ExecutePerSecond()
    {
        while (true)
        {
            target.GetComponent<CharacterStates>().currentHealth+=(int)(addition*creator.GetComponent<CharacterStates>().attackData.currentAttack);
             GameEventSystem.instance.HealthBarUpdate(target);
            yield return new WaitForSeconds(1.0f);
        }
    }
   private void OnDestroy() 
    {
        StopCoroutine(currentcoroutine);
    }
}
