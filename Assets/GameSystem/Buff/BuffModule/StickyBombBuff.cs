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
        }
        
        
    }
    private void Bomb(GameObject creator, GameObject target, Bullet bullet)
    {
        
        if(stickyBombBuff.creator==creator&&stickyBombBuff.target==target)
        {
           
            if(bullet.bulletType==Bullet.BulletType.Default)
            {
                 Debug.Log("爆炸");
                switch (stickyBombBuff.curStack)
                {
                    
                    case 1:
                        stickyBomb.gameObject.transform.localScale=new Vector3(0.8f,0.8f,0.8f);
                        stickyBomb.GetComponent<Bullet>().attackInfo.singleAttackMagnification=0f;
                        break;
                    case 2:
                        stickyBomb.gameObject.transform.localScale=new Vector3(2.2f,2.2f,2.2f);
                        stickyBomb.GetComponent<Bullet>().attackInfo.singleAttackMagnification=1f;
                        break;
                    case 3:
                        stickyBomb.gameObject.transform.localScale=new Vector3(3.5f,3.5f,3.5f);
                        stickyBomb.GetComponent<Bullet>().attackInfo.singleAttackMagnification=2f;
                        break;
                        
                }
               stickyBomb.GetComponent<StickyBomb>().StickyBulletExplode();          
               AttackManager.instance.onAttackEvent-=Bomb;
               stickyBombBuff.target.GetComponent<BuffHandler>().RemoveBuff(stickyBombBuff);
            }
            
        }
       
    }

   
}

