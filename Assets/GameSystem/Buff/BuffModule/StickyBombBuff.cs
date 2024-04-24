using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "_stickbomb", menuName = "BuffSystem/stickbomb", order = 0)]
public class StickyBombBuff: BaseBuffModule
{
    public GameObject stickyBombPrefab;
    private GameObject stickyBomb;
    public BuffInfo stickyBombBuff;

    public override void Apply(BuffInfo buffInfo, DamageInfo damageInfo = null)
    {
        
        stickyBombBuff=buffInfo;
        Debug.Log(buffInfo.buffData);
        AttackManager.instance.onAttackEvent+=Bomb;
        if(buffInfo.curStack<=1)
        {
            stickyBomb=Instantiate(stickyBombPrefab);
            stickyBomb.transform.position=buffInfo.target.transform.position;
            stickyBomb.transform.parent=buffInfo.target.transform;
            stickyBomb.GetComponent<Bullet>().InitiatorStates=buffInfo.creator.gameObject.GetComponent<CharacterStates>();
            stickyBomb.GetComponent<WeaponStates>().weaponStates=buffInfo.self.GetComponent<WeaponStates>().weaponStates;
        }
        
        
    }
    private void Bomb(GameObject creator, GameObject target, Bullet bullet)
    {
        
        if(stickyBombBuff.creator==creator&&stickyBombBuff.target==target)
        {
           
            if(bullet.weaponStates.weaponStates.weaponType==WeaponStates_SO.WeaponType.Follow)
            {
                 Debug.Log("爆炸");
                switch (stickyBombBuff.curStack)
                {
                    
                    case 1:
                        stickyBomb.gameObject.transform.localScale= Vector3.one*stickyBomb.GetComponent<WeaponStates>().weaponStates.explosionRange[0];
                        stickyBomb.GetComponent<Bullet>().attackInfo.singleAttackMagnification=stickyBomb.GetComponent<WeaponStates>().weaponStates.attackMultipliers[0];
                        break;
                    case 2:
                        stickyBomb.gameObject.transform.localScale=Vector3.one*stickyBomb.GetComponent<WeaponStates>().weaponStates.explosionRange[1];
                        stickyBomb.GetComponent<Bullet>().attackInfo.singleAttackMagnification=stickyBomb.GetComponent<WeaponStates>().weaponStates.attackMultipliers[1];
                        break;
                    case 3:
                        stickyBomb.gameObject.transform.localScale=Vector3.one*stickyBomb.GetComponent<WeaponStates>().weaponStates.explosionRange[2];
                        stickyBomb.GetComponent<Bullet>().attackInfo.singleAttackMagnification=stickyBomb.GetComponent<WeaponStates>().weaponStates.attackMultipliers[2];
                        break;
                        
                }
               stickyBomb.GetComponent<StickyBomb>().StickyBulletExplode();          
               AttackManager.instance.onAttackEvent-=Bomb;
               stickyBombBuff.target.GetComponent<BuffHandler>().RemoveBuff(stickyBombBuff);
            }
            
        }
       
    }

   
}

