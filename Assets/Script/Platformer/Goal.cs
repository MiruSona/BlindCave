using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

    //참조
    private DarkShader dark_shader;
    private GameManager game_manager;
    private SEManager se_manager;
    private GameObject particle, end_particle;

    //컴포넌트


    //라이트 관련
    private float size_max = 0.15f;
    private float size_delta = 0.002f;
    private Define.Timer light_timer = new Define.Timer(3f, 3f);

    //스테이지 관련
    private bool end = false;
    private float end_max = 1.5f;
    private float end_delta = 0.007f;
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
                if (light_timer.AutoTimer())
                {
                    se_manager.Play(SEManager.ETC.Goal);
                    dark_shader.AddLight(transform.position, size_max, size_delta);
                    particle.SetActive(true);
                }
                break;
            case GameManager.State.Goal:
                if (!end)
                {
                    se_manager.Play(SEManager.ETC.Goal);
                    dark_shader.AddLight(transform.position, end_max, end_delta);
                    end_particle.SetActive(true);
                    end = true;
                }
                else
                {
                    if (!end_particle.activeSelf)
                    {
                        if (end_delay.AutoTimer())
                            SceneManager.LoadScene(0);
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
