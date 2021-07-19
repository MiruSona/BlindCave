using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTon<GameManager> {

    //게임 상태
    public enum State
    {
        Play,
        Die,
        Goal
    }
    [HideInInspector]
    public State state = State.Play;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
