using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    //참조
    private DarkShader dark_shader;
    private GameManager game_manager;
    private SEManager se_manager;
    private GameObject particle, end_particle;
    
    //라이트 관련
    private float size_max = 0.15f;
    private float size_delta = 0.002f;
    private Define.Timer light_timer = new Define.Timer(3f, 3f);

    //스테이지 관련
    private bool end = false;
    private float end_max = 1.5f;
    private float end_delta = 0.018f;
    private Define.Timer end_delay = new Define.Timer(0, 0.7f);

	//초기화
	void Start () {
        dark_shader = DarkShader.instance;
        game_manager = GameManager.instance;
        se_manager = SEManager.instance;
        particle = transform.FindChild("Particle").gameObject;
        end_particle = transform.FindChild("EndParticle").gameObject;
    }
	
	//일정 시간마다 빛나기
	void Update () {
        switch (game_manager.state)
        {
            case GameManager.State.Play:
                //빛나기
                if (light_timer.AutoTimer())
                {
                    se_manager.Play(SEManager.Flower.Low);
                    dark_shader.AddLight(transform.position, size_max, size_delta);
                    particle.SetActive(true);
                }
                break;
            case GameManager.State.Goal:
                //끝났나 체크
                if (!end)
                {
                    se_manager.Play(SEManager.Flower.High);
                    dark_shader.AddLight(transform.position, end_max, end_delta);
                    end_particle.SetActive(true);
                    end = true;
                }
                else
                {
                    //파티클 실행했으면 파티클 꺼질때까지 기다리고
                    if (!end_particle.activeSelf)
                    {
                        //일정 시간 뒤 다음스테이지로
                        if (end_delay.AutoTimer())
                            game_manager.NextStage();
                    }
                }
                break;
        }
        
	}

    void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.CompareTag("Player"))
        {
            game_manager.state = GameManager.State.Goal;
        }
    }
}
