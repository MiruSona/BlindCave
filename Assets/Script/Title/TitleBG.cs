using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBG : MonoBehaviour {

    //참조
    private Renderer render;

    //값
    private float move_delta = 0.001f;
    private int dir_index = 0;
    private Vector2[] direction = new Vector2[4];

    //타이머
    private Define.Timer change_timer = new Define.Timer(0f, 10f);

    //초기화
    void Start()
    {
        render = transform.FindChild("BGImage").GetComponent<Renderer>();
        direction[0] = new Vector2(1, 1);
        direction[1] = new Vector2(-1, 1);
        direction[2] = new Vector2(1, -1);
        direction[3] = new Vector2(-1, -1);
    }

    void FixedUpdate()
    {
        //타이머 마다 방향 바꾸기
        if (change_timer.AutoTimer())
        {
            int random = Random.Range(0, 4);
            dir_index = random;
        }
            
        //배경 움직이기
        render.material.mainTextureOffset += direction[dir_index] * move_delta;
    }
}
