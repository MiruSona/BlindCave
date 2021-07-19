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

    public enum Flower
    {
        Low,
        Middle,
        High
    }

    public enum Block
    {
        Broken,
        Hidden
    }

    //컴포넌트
    private AudioSource audio_source;

    //음악 소스
    private string path = "Sound/Effect/";
    private List<AudioClip> player_se;
    private List<AudioClip> monster_se;
    private List<AudioClip> flower_se;
    private List<AudioClip> block_se;

    //초기화
    void Start () {
        audio_source = GetComponent<AudioSource>();

        player_se = new List<AudioClip>();
        player_se.Add(Resources.Load<AudioClip>(path + "Jump/Jump2"));
        player_se.Add(Resources.Load<AudioClip>(path + "Hurt/Hurt0"));
        player_se.Add(Resources.Load<AudioClip>(path + "Robots/RobotSFX"));

        monster_se = new List<AudioClip>();
        monster_se.Add(Resources.Load<AudioClip>(path + "Hurt/Hurt2"));
        monster_se.Add(Resources.Load<AudioClip>(path + "Robots/Robots3"));

        flower_se = new List<AudioClip>();
        flower_se.Add(Resources.Load<AudioClip>(path + "FlowerSE/FlowerLow"));
        flower_se.Add(Resources.Load<AudioClip>(path + "FlowerSE/FlowerMiddle"));
        flower_se.Add(Resources.Load<AudioClip>(path + "FlowerSE/FlowerHigh"));

        block_se = new List<AudioClip>();
        block_se.Add(Resources.Load<AudioClip>(path + "Xylophone/Xylophone3"));
        block_se.Add(Resources.Load<AudioClip>(path + "Orgel/Oregel12"));
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

    public void Play(Flower _flower)
    {
        switch (_flower)
        {
            case Flower.Low:
                audio_source.PlayOneShot(flower_se[0]);
                break;
            case Flower.Middle:
                audio_source.PlayOneShot(flower_se[1]);
                break;
            case Flower.High:
                audio_source.PlayOneShot(flower_se[2]);
                break;
        }
    }

    public void Play(Block _block)
    {
        switch (_block)
        {
            case Block.Broken:
                audio_source.PlayOneShot(block_se[0]);
                break;
            case Block.Hidden:
                audio_source.PlayOneShot(block_se[1]);
                break;
        }
    }
}
