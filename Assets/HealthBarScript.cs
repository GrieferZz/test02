using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarScript: MonoBehaviour
{
    public Image Bar;

    public TextMeshProUGUI HPTMP;

    // Start is called before the first frame update

    RectTransform rectTran;
    public CharacterStates playerStates;
    float maxLength;
    void Start()
    {
        rectTran = Bar.GetComponent<RectTransform>();
        maxLength = rectTran.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        playerStates.currentHealth= (int)HPLimitation(playerStates.currentHealth);
        ShowCurrentHP();
        SetBarLength();
    }

    void ShowCurrentHP()
    {
        string healthString = $"{playerStates.currentHealth}/{playerStates.MaxHealth}";
        HPTMP.text = healthString ;
    }

    void SetBarLength()
    {
        rectTran.sizeDelta = new Vector2(maxLength*playerStates.currentHealth/playerStates.MaxHealth, rectTran.sizeDelta.y);
    }

    float HPLimitation(int currentHP)
    {
        if (playerStates.currentHealth>playerStates.MaxHealth)
        {
            return playerStates.MaxHealth;
        }
        
        if (playerStates.currentHealth< 0)
        {
            return 0;
        }
        return playerStates.currentHealth;
    }
}
