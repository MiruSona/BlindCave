using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSystem : MonoBehaviour {

    //참조
    private GameManager game_manager;

	//초기화
	void Start () {
        game_manager = GameManager.instance;
    }
	
	//눌렀는지 체크
	void Update () {
        if (Input.anyKeyDown)
            game_manager.NextStage();
	}
}
