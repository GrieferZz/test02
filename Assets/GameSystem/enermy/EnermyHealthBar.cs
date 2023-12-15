using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class EnermyHealthBar : MonoBehaviour
{
    public GameObject HealthBarPrefab;
    public Transform BarPoint;
    Image HealthSlider;
    Transform HealthUIbar;

    Transform cm;

    EnermyInformation enermyInformation;
    void Awake()
    {
        enermyInformation=GetComponent<EnermyInformation>();
    }
    void OnEnable()
    {
        cm=Camera.main.transform;
        foreach(Canvas canvas in FindObjectsOfType<Canvas>())
        {
            if(canvas.renderMode==RenderMode.WorldSpace)
            {
                HealthUIbar=Instantiate(HealthBarPrefab,canvas.transform).transform;
                HealthSlider=HealthUIbar.GetChild(0).GetComponent<Image>();
                HealthUIbar.gameObject.SetActive(false);
            }

        }
    }

    public void UpdateHealthBar()
    {
        if(enermyInformation.NowHealth<=0)
        {
            Destroy(HealthUIbar.gameObject);
        }
        HealthUIbar.gameObject.SetActive(true);
        float sliderPercent=(float)enermyInformation.NowHealth/enermyInformation.MaxHealth;
        HealthSlider.fillAmount=sliderPercent;
    }
    private void LateUpdate() 
    {
        if(HealthUIbar!=null)
        {
            HealthUIbar.position=BarPoint.position;
            HealthUIbar.forward=-cm.forward;
        }
        
    }
}
