using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicControl : MonoBehaviour
{
    public AudioSource audiosingle;
    public AudioSource audioattack;
    public AudioSource audiowalk;
    public List<AudioClip> musicClips=new List<AudioClip>();
    
    // Start is called before the first frame update
    void Start()
    {
        GameEventSystem.instance.onMusicPlay+=audioplay;
        
    }
     void OnDisable()
     {
        
     }
    // Update is called once per frame
    void Update()
    {
        
    }
    public AudioClip FindClip(string name)
    {
        for(int i=0;i<musicClips.Count;i++)
        {
            if(name==musicClips[i].name)
            {
                return musicClips[i];
                
            }
           
        }
         
                return null;
    }
    public void audioplay(string _musicname)
    {
        switch(_musicname)
        {
            case "follow":
             audioattack.clip =FindClip("follow-trace");
             audioattack.Play();
            break;
            case "trace":
             audioattack.clip =FindClip("follow-trace");
             audioattack.volume=0.8f;
             audioattack.pitch=1.2f;
             audioattack.Play();
            break;
            case "sticky":
            audioattack.clip =FindClip(_musicname);
             audioattack.Play();
            break;
            case "pick":
            audiosingle.clip =FindClip(_musicname);
             audiosingle.Play();
             break;
             case "run":
            audiowalk.clip =FindClip(_musicname);
             audiowalk.Play();
            break;
            case "explosion":
            audiosingle.clip =FindClip(_musicname);
             audiosingle.Play();
            break;
            case "newroom":
            audiosingle.clip =FindClip(_musicname);
             audiosingle.Play();
            break;
            case "playerhurt":
            audiosingle.clip =FindClip(_musicname);
             audiosingle.Play();
            break;
            case "choosereward":
            audiosingle.clip =FindClip(_musicname);
             audiosingle.Play();
            break;

        }

    }
    
}
