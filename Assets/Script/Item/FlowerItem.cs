using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerItem : MonoBehaviour {

    //컴포넌트
    private BoxCollider2D box_collider;
    private SpriteRenderer render;

    //값
    private bool hit = false;
    private float alpha_delta = 0.01f;

	//초기화
	void Start () {
        box_collider = GetComponent<BoxCollider2D>();
        render = GetComponent<SpriteRenderer>();
	}
	
	//부딪치면 사라짐!
	void Update () {
        if (hit)
        {
            //사라짐
            Color color = render.color;
            if (color.a > 0f)
            {
                color.a -= alpha_delta;
                render.color = color;
            }
            else
                gameObject.SetActive(false);
        }
	}

    void OnTriggerEnter2D(Collider2D _col)
    {
        //플레이어 부딪치면 부딪쳤다 표시
        if (_col.CompareTag("Player"))
        {
            hit = true;
            //충돌X
            box_collider.enabled = false;
        } 
    }
}
