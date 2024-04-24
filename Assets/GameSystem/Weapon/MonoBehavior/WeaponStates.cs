using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStates : MonoBehaviour
{
    public WeaponStates_SO templateData;
    [HideInInspector]
    public WeaponStates_SO weaponStates;
    void Update()
    {
        if(templateData!=null&&weaponStates==null)
        {
            weaponStates=Instantiate(templateData);
        }
    }
}
