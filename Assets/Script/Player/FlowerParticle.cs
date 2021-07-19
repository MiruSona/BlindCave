using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerParticle : MonoBehaviour {

    //참조
    private CircleCollider2D circle_collider;

    //값
    private float size_init = 0.1f;
    private float size_max = 3.4f;
    private float size_delta = 0.06f;

	//초기화
	void Awake () {
        circle_collider = GetComponent<CircleCollider2D>();
    }

    //켜질때 초기화
    private void OnEnable()
    {
        circle_collider.radius = size_init;
    }
    
    void Update () {
        //점점 컬리더 커지기
        if (circle_collider.radius < size_max)
            circle_collider.radius += size_delta;
	}
}
