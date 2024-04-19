using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStates : MonoBehaviour
{
    public WeaponStates_SO templateData;
    [HideInInspector]
    public WeaponStates_SO weaponStates;
    void Awake()
    {
        if(templateData!=null)
        {
            weaponStates=Instantiate(templateData);
        }
    }
}
