using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class EnermyHealthBar : MonoBehaviour
{
    public GameObject HealthBarPrefab;
    public Transform BarPoint;
    Image HealthFrontSlider;
    Image HealthBackSlider;
    Transform HealthUIbar;

    Transform cm;
    Coroutine updateCoroutine;
    float bufftime=0.5f;

    CharacterStates currentStates;
    void Awake()
    {
        currentStates=GetComponent<CharacterStates>();
        GameEventSystem.instance.onHealthBarUpdate+=UpdateHealthBar;
    }
    void OnEnable()
    {
        cm=Camera.main.transform;
        foreach(Canvas canvas in FindObjectsOfType<Canvas>())
        {
            if(canvas.renderMode==RenderMode.WorldSpace)
            {
                HealthUIbar=Instantiate(HealthBarPrefab,canvas.transform).transform;
                HealthFrontSlider=HealthUIbar.GetChild(1).GetComponent<Image>();
                HealthBackSlider=HealthUIbar.GetChild(0).GetComponent<Image>();
                HealthUIbar.gameObject.SetActive(false);
            }

        }
    }
    void OnDisable() 
    {
        GameEventSystem.instance.onHealthBarUpdate-=UpdateHealthBar;
    }

    public void UpdateHealthBar(GameObject target)
    {
        if(target==gameObject)
        {
           
            HealthUIbar.gameObject.SetActive(true);
            float sliderPercent=(float)currentStates.currentHealth/currentStates.MaxHealth;
            HealthFrontSlider.fillAmount=sliderPercent;
             if(updateCoroutine!=null)
            {
                StopCoroutine(updateCoroutine);
            }
            updateCoroutine=StartCoroutine(HealthBarSlowDown());
            if(HealthFrontSlider.fillAmount<=0)
        {
            StopCoroutine(updateCoroutine);
            Destroy(HealthUIbar.gameObject);
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
    private void LateUpdate() 
    {
        if(HealthUIbar!=null)
        {
            HealthUIbar.position=BarPoint.position;
            HealthUIbar.forward=cm.forward;
        }
        
    }
}
