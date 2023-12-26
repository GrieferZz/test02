using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarScript: MonoBehaviour
{
    public Image Bar;

    public TextMeshProUGUI HPTMP;

    [SerializeField]
    float currentHP = 100;

    [SerializeField]
    float maxHP = 100;
    // Start is called before the first frame update

    RectTransform rectTran;
    float maxLength;
    void Start()
    {
        rectTran = Bar.GetComponent<RectTransform>();
        maxLength = rectTran.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        currentHP = HPLimitation(currentHP);
        ShowCurrentHP();
        SetBarLength();
    }

    void ShowCurrentHP()
    {
        string healthString = $"{currentHP}/{maxHP}";
        HPTMP.text = healthString ;
    }

    void SetBarLength()
    {
        rectTran.sizeDelta = new Vector2(maxLength*currentHP/maxHP, rectTran.sizeDelta.y);
    }

    float HPLimitation(float currentHP)
    {
        if (currentHP>maxHP)
        {
            return maxHP;
        }
        
        if (currentHP < 0)
        {
            return 0;
        }
        return currentHP;
    }
}
