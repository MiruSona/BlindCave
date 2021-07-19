using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSManager : MonoBehaviour {

    //참조
    private GameManager game_manager;

    //컴포넌트
    private AudioSource audio_source;

    //음악 소스
    private string path = "Sound/Background/";
    private List<AudioClip> bgm;

    //초기화
    void Start()
    {
        game_manager = GameManager.instance;

        audio_source = GetComponent<AudioSource>();

        Init();

        Play(game_manager.current_stage);
    }

    //초기화
    private void Init()
    {
        bgm = new List<AudioClip>();
        bgm.Add(Resources.Load<AudioClip>(path + "Title1"));
        bgm.Add(Resources.Load<AudioClip>(path + "Cave2"));
        bgm.Add(Resources.Load<AudioClip>(path + "Ending"));
    }

    //플레이
    private void Play(int _stage)
    {
        switch (_stage)
        {
            case 0:
                audio_source.PlayOneShot(bgm[0]);
                break;
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                audio_source.PlayOneShot(bgm[1]);
                break;
            case 6:
                audio_source.PlayOneShot(bgm[2]);
                break;
        }
    }
}
