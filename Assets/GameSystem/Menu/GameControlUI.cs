using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class GameControlUI : MonoBehaviour
{
    public GameObject pause;
    public GameObject pausePanel;
    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Pause()
    {
        pausePanel.SetActive(true);
        pausePanel.GetComponent<Animation>().Play("PausePanel");
        StartCoroutine(Play(pausePanel.GetComponent<Animation>(),"PausePanel",false,null));
        Time.timeScale = 0;
        
    }
    public void GameBack()
    {
        
        pausePanel.GetComponent<Animation>().Play("PausePanelClose");
        StartCoroutine(Play(pausePanel.GetComponent<Animation>(),"PausePanelClose",false,() =>
    {
        // 关闭组件
        pausePanel.SetActive(false);

        // 恢复暂停
        Time.timeScale = 1;
    }));
        


    }
    /*private IEnumerator WaitForAnimation(string anim,GameObject item,System.Action onComplete)
    {
        
        // 等待动画播放完成
        yield return new WaitForSeconds( pausePanel.GetComponent<Animation>()[anim].length);
        
        if(onComplete != null)
			{
				onComplete();
                Debug.Log("生效");
			} 
        
    }*/
    private IEnumerator Play( Animation animation, string clipName, bool useTimeScale,System.Action onComplete )
	{
		if(!useTimeScale)
		{
			AnimationState _currState = animation[clipName];
			bool isPlaying = true;
			float _startTime = 0F;
			float _progressTime = 0F;
			float _timeAtLastFrame = 0F;
			float _timeAtCurrentFrame = 0F;
			float deltaTime = 0F;
			animation.Play(clipName);
			_timeAtLastFrame = Time.realtimeSinceStartup;
			while (isPlaying) 
			{
				_timeAtCurrentFrame = Time.realtimeSinceStartup;
				deltaTime = _timeAtCurrentFrame - _timeAtLastFrame;
				_timeAtLastFrame = _timeAtCurrentFrame; 
				
				_progressTime += deltaTime;
				_currState.normalizedTime = _progressTime / _currState.length; 
				animation.Sample ();
				if (_progressTime >= _currState.length) 
				{
					if(_currState.wrapMode != WrapMode.Loop)
					{
						isPlaying = false;
					}
					else
					{
						_progressTime = 0.0f;
					}
				}
				yield return new WaitForEndOfFrame();
			}
			yield return null;
			if(onComplete != null)
			{
				onComplete();
			} 
		}
		else
		{
			animation.Play(clipName);
		}
	}

}
