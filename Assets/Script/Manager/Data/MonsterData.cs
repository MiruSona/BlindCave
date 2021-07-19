using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData {

    //스탯
    public float hp_max = 30f;
    public float hp = 30f;

    public float atk = 20f;

    //물리
    public float speed = 3f;
    public readonly float LEFT = -1f, RIGHT = 1f, UP = 1f, DOWN = -1f;
    public float facing = 1f;
    public Vector2 direction = Vector2.zero;

    //상태
    public enum State
    {
        Create,
        Alive,
        Attacked,
        Die
    }
    public State state = State.Create;
    
    //움직임
    public bool attack = false;
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

        if (hp - _value > 0)
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
}
