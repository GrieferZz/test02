using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using TMPro;
public class BuffBarUI : MonoBehaviour
{
    public BuffHandler buffHandler;
    public GameObject buffIconPrefab;
    public GameObject buffHolder;
    GameObject buffIcon;
    public List<BuffInfo>buffIcons=new List<BuffInfo>();
    // Start is called before the first frame update
    void Start()
    {
        buffHandler=GetComponent<BuffHandler>();
        GameEventSystem.instance.onBuffUIUpdate+=BuffUIUpdate;
    }
    void OnDisable()
    {
         GameEventSystem.instance.onBuffUIUpdate-=BuffUIUpdate;

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void BuffUIUpdate()
    {
        if(buffHandler!=null&&buffHolder!=null)
        {
             
            for(int i=0;i<buffHolder.transform.childCount;i++)
            {
                Destroy(buffHolder.transform.GetChild(i).gameObject);
                buffIcons.Clear();
            }
        }
        if(buffHandler!=null&&buffHolder!=null)
        {
              
             foreach(var buff in buffHandler.buffList)
        {
            if(!buffIcons.Contains(buff))
            {
                buffIcons.Add(buff);
                buffIcon=Instantiate(buffIconPrefab,buffHolder.transform);
                buffIcon.transform.parent=buffHolder.transform;
                buffIcon.GetComponent<Image>().sprite=buff.buffData.icon;
                if(buff.buffData.maxStack!=1)
                {
                    buffIcon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text=buff.curStack.ToString();

                }
                else
                {
                    buffIcon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text="";
                }
            }
            

        }

        }
       
    }
}
