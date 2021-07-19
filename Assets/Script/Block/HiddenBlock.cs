using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenBlock : MonoBehaviour {

    //참조
    private SEManager se_manager;

    //컴포넌트
    private BoxCollider2D box_collider;
    private SpriteRenderer render;

    //값
    private float alpha_delta = 0.01f;

	//초기화
	void Start () {
        se_manager = SEManager.instance;
        box_collider = GetComponent<BoxCollider2D>();
        render = GetComponent<SpriteRenderer>();
    }

    //부딪치면 보이게
    void Update()
    {
        //탐지됬다면 보이게!
        if (!box_collider.isTrigger)
        {
            Color color = render.color;
            if(color.a < 1f)
            {
                color.a += alpha_delta;
                render.color = color;
            }
        }
    }

    //꽃 파티클 부딪치면 트리거 해제
    private void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.CompareTag("Flower"))
        {
            if (box_collider.isTrigger)
            {
                se_manager.Play(SEManager.Block.Hidden);
                box_collider.isTrigger = false;
            }            
        }
    }
}
