using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleFlower : MonoBehaviour {

    //참조
    private DarkShader dark_shader;
    private GameObject particle;
    private SEManager se_manager;

    //타이머
    private Define.Timer echo_timer = new Define.Timer(2f, 2f);

    //빛관련
    private float size_max = 1.5f;
    private float size_delta = 0.01f;

    //초기화
    void Start () {
        dark_shader = DarkShader.instance;
        se_manager = SEManager.instance;
        particle = transform.FindChild("Particle").gameObject;
	}
	
	void FixedUpdate() {
        //파티클 체크
        if (!particle.activeSelf)
        {
            //타이머 될때마다 빛 켜기
            if (echo_timer.AutoTimer())
            {
                se_manager.Play(SEManager.Flower.High);
                dark_shader.AddLight(transform.position, size_max, size_delta);
                particle.SetActive(true);
            }
        } 
	}
}
