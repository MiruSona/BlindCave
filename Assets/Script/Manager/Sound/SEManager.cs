using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : SingleTon<SEManager> {

    //종류
    public enum Player
    {
        Jump, Attacked, Die
    }

    public enum Monster
    {
        Attacked, Die
    }

    public enum ETC
    {
        Goal
    }

    //컴포넌트
    private AudioSource audio_source;

    //음악 소스
    private string path = "Sound/Effect/";
    private List<AudioClip> player_se;
    private List<AudioClip> monster_se;
    private List<AudioClip> etc_se;

    //초기화
    void Start () {
        audio_source = GetComponent<AudioSource>();

        player_se = new List<AudioClip>();
        player_se.Add(Resources.Load<AudioClip>(path + "Jump/Jump0"));
        player_se.Add(Resources.Load<AudioClip>(path + "Hurt/Hurt0"));
        player_se.Add(Resources.Load<AudioClip>(path + "Robots/RobotSFX"));

        monster_se = new List<AudioClip>();
        monster_se.Add(Resources.Load<AudioClip>(path + "Hurt/Hurt2"));
        monster_se.Add(Resources.Load<AudioClip>(path + "Robots/Robots3"));

        etc_se = new List<AudioClip>();
        etc_se.Add(Resources.Load<AudioClip>(path + "Boing"));
    }

    //사운드 처리
    public void Play(Player _player)
    {
        switch (_player)
        {
            case Player.Jump:
                audio_source.PlayOneShot(player_se[0]);
                break;
            case Player.Attacked:
                audio_source.PlayOneShot(player_se[1]);
                break;
            case Player.Die:
                audio_source.PlayOneShot(player_se[2]);
                break;
        }
    }

    public void Play(Monster _monster)
    {
        switch (_monster)
        {
            case Monster.Attacked:
                audio_source.PlayOneShot(monster_se[0]);
                break;
            case Monster.Die:
                audio_source.PlayOneShot(monster_se[1]);
                break;
        }
    }

    public void Play(ETC _etc)
    {
        switch (_etc)
        {
            case ETC.Goal:
                audio_source.PlayOneShot(etc_se[0]);
                break;
        }
    }
}
