using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarScript: MonoBehaviour
{
     public GameObject HealthBarPrefab;
   
    Image HealthFrontSlider;
    Image HealthBackSlider;
    Transform HealthUIbar;

    
    Coroutine updateCoroutine;
    float bufftime=0.5f;

    public CharacterStates currentStates;
    void Awake()
    {
        
        AttackManager.instance.onAttackEvent+=UpdateHealthBar;
    }
    void OnEnable()
    {
        
                HealthUIbar=HealthBarPrefab.transform;
                HealthFrontSlider=HealthUIbar.GetChild(2).GetComponent<Image>();
                HealthBackSlider=HealthUIbar.GetChild(1).GetComponent<Image>();
               
            

        
    }
    void OnDisable() 
    {
         AttackManager.instance.onAttackEvent-=UpdateHealthBar;
    }

    public void UpdateHealthBar(GameObject creater,GameObject target,Bullet bullet)
    {
        if(target==currentStates.gameObject)
        {
           
            Debug.Log("血量更新");
            float sliderPercent=(float)currentStates.currentHealth/currentStates.MaxHealth;
            HealthFrontSlider.fillAmount=sliderPercent;
             if(updateCoroutine!=null)
            {
                StopCoroutine(updateCoroutine);
            }
            updateCoroutine=StartCoroutine(HealthBarSlowDown());
            if(HealthBackSlider.fillAmount<=0)
        {
            StopCoroutine(updateCoroutine);
            
        }


        }
        
    }
    IEnumerator HealthBarSlowDown()
    {
        
        float effectLength=HealthBackSlider.fillAmount-HealthFrontSlider.fillAmount;
        float elapsedTime=0f;
        while(elapsedTime<bufftime &&effectLength!=0)
        {
            elapsedTime+=Time.deltaTime;
            HealthBackSlider.fillAmount=Mathf.Lerp(HealthFrontSlider.fillAmount+effectLength,HealthFrontSlider.fillAmount,elapsedTime/bufftime);
            yield return null;
        }
        HealthBackSlider.fillAmount=HealthFrontSlider.fillAmount;
    }
    
}
