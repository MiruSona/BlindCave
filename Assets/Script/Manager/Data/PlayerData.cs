using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData {

    //스탯
    public readonly float hp_max = 30f;
    public float hp = 30f;

    public readonly float mp_max = 100f;
    public float mp = 100f;

    public readonly float atk = 10f;

    //물리
    public readonly float speed = 3.5f;
    public readonly float jump_power = 7.2f;
    public readonly float LEFT = -1f, RIGHT = 1f, UP = 1f, DOWN = -1f;
    public float facing = 1f;
    public Vector2 direction = Vector2.zero;

    //상태
    public enum State
    {
        Alive,
        Attacked,
        Die
    }
    public State state = State.Alive;

    //움직임
    public bool move = false;
    public bool ground = false;

    //------------ HP ------------
    public void AddHP(float _value)
    {
        if (state == State.Die)
            return;

        if (hp + _value < hp_max)
            hp += _value;
        else
            hp = hp_max;
    }

    public void SubHP(float _value)
    {
        if (state != State.Alive)
            return;

        if(hp - _value > 0)
        {
            hp -= _value;
            state = State.Attacked;
        }
        else
        {
            hp = 0;
            state = State.Die;
        }
    }

    //------------ MP ------------
    public void AddMP(float _value)
    {
        if (mp + _value < mp_max)
            mp += _value;
        else
            mp = mp_max;
    }

    public void SubMP(float _value)
    {
        if (mp - _value > 0)
            mp -= _value;
        else
            mp = 0;
    }

    public void Init()
    {
        state = State.Alive;
        hp = hp_max;        
        facing = 1f;
        move = false;
        ground = false;
    }

}
