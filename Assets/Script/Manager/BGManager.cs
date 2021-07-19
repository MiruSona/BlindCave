using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGManager : MonoBehaviour {

    //참조
    private PlayerData player_data;
    private Renderer render;

    //값
    private float move_delta = 0.0002f;

	//초기화
	void Start () {
        player_data = DataManager.instance.player_data;
        render = transform.FindChild("BGImage").GetComponent<Renderer>();
	}
	
	void Update () {
        //플레이어 움직임에 따라 배경도 움직이기
        if (player_data.move)
        {
            if(player_data.facing == player_data.RIGHT)
                render.material.mainTextureOffset += Vector2.right * move_delta;
            else
                render.material.mainTextureOffset -= Vector2.right * move_delta;
        }
	}
}
