using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;

public class Dead : Action
{
    public CharacterStates characterStates;
    
    public NavMeshAgent agent;
    public InstantiationHelper instantiationHelper;
    private GameObject coin;

    public override void OnAwake()
    {
        
    }

    public override TaskStatus OnUpdate()
    {

       
        int coinGet=Random.Range(characterStates.itemsData.coin-characterStates.itemsData.floatingValue,characterStates.itemsData.coin+characterStates.itemsData.floatingValue);
        GameObject.FindWithTag("Player").GetComponent<CharacterStates>().itemsData.coin+=coinGet;
        for(int i=0;i<coinGet;i++)
        {
            coin=instantiationHelper.HelperInstantiate(characterStates.itemsData.coinPrefab);
            coin.transform.position=gameObject.transform.position;

        }
        
        GetComponent<Collider>().enabled=false;
        agent.enabled=false;
        GameManager.Instance.EnermyDead(characterStates);
        instantiationHelper.HelperDestroy(gameObject,0.5f);
        return TaskStatus.Success;
    }
}