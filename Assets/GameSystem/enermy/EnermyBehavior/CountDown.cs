using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UI;
public class CountDown : Action
{
   public float waitTime = 3.0f; // 等待时间，单位为秒
    private float startTime;
    public SharedFloat remainingTime; // 剩余时间
    public NavMeshAgent agent;
    public InstantiationHelper instantiationHelper;
    public GameObject interactionPrefab;
    public GameObject interactionPoint; 
    public GameObject interaction;
    Transform cm;
    private int lastWholeSecond;

    // 在行为开始时调用，记录开始时间
    public override void OnStart()
    {
        startTime = Time.time;
        
        cm=Camera.main.transform;
        foreach(Canvas canvas in instantiationHelper.HelperFindObjectsOfType<Canvas>())
        {
            if(canvas.renderMode==RenderMode.WorldSpace)
            {
                interaction=instantiationHelper.HelperInstantiatePrefab(interactionPrefab,canvas.transform);
                instantiationHelper.AddItems(interaction);
                //HealthUIbar.gameObject.SetActive(false);
            }

        }
    }
    

    // 在每一帧调用，检查是否经过了指定的等待时间
    public override TaskStatus OnUpdate()
    {
        if(agent.enabled==false)
        {
             instantiationHelper.HelperDestroy(interaction,0f);
              return TaskStatus.Failure;
        }

        agent.speed=0;
        float elapsedTime = Time.time - startTime;
        remainingTime.Value = Mathf.Max(0, waitTime - elapsedTime);
        int currentWholeSecond = Mathf.CeilToInt(remainingTime.Value);
         if(interaction!=null)
        {
            interaction.transform.position=interactionPoint.transform.position;
            interaction.transform.forward=cm.forward;

            interaction.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text= Mathf.Ceil(remainingTime.Value).ToString();
        }
         
            {
                interaction.GetComponent<Image>().fillAmount = 1 - (elapsedTime % 1); // 每秒跳转一次
                lastWholeSecond = currentWholeSecond; // 更新上一次的整秒时间
            }
        
        if (elapsedTime >= waitTime)
        {
            instantiationHelper.HelperDestroy(interaction,0f);
            return TaskStatus.Success; // 等待时间结束，返回成功
        }
            
            
        return TaskStatus.Running; // 等待时间未结束，继续运行
    }
}
