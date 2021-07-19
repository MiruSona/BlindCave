using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlower : MonoBehaviour {

    //참조
    private GameManager game_manager;
    private DarkShader dark_shader;
    private ParticleManager flower_particle;
    private SEManager se_manager;

    //컴포넌트
    private SpriteRenderer leaf_render, stem_render;

    //꽃 관련
    private const int flower_num = 8;
    private int flower_index = 1;
    private Sprite[] sprites = new Sprite[flower_num];
    private float alpha_delta = 0.01f;

    //빛
    private float light_max = 0.45f;
    private float light_delta = 0.006f;
    private Define.Timer light_timer = new Define.Timer(0f, 3f);

	//초기화
	void Awake () {
        game_manager = GameManager.instance;
        dark_shader = DarkShader.instance;
        se_manager = SEManager.instance;
        ParticleManager[] temp = FindObjectsOfType<ParticleManager>();
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].name == "FlowerParticles")
                flower_particle = temp[i];
        }

        leaf_render = transform.FindChild("FlowerLeaf").GetComponent<SpriteRenderer>();
        stem_render = transform.FindChild("FlowerStem").GetComponent<SpriteRenderer>();

        for (int i = 0; i < flower_num; i++)
            sprites[i] = Resources.Load<Sprite>("Sprite/Flower/FlowerLeaf/FlowerLeaf" + i);
    }

    //외부 초기화
    public void Init()
    {
        light_timer.time = 0f;
        flower_index = 1;
        leaf_render.sprite = sprites[flower_index];
        leaf_render.color = Color.white;
        stem_render.color = Color.white;
    }

    void Update () {
        //플레이 상태 아니면 중지
        if (game_manager.state != GameManager.State.Play)
            return;

        //꽃잎 없으면 꽃 사라짐
        if (flower_index == flower_num)
        {
            Color color = leaf_render.color;
            //점점 사라짐
            if(color.a > 0f)
            {
                color.a -= alpha_delta;
                leaf_render.color = color;
                stem_render.color = color;
            }
            else
                gameObject.SetActive(false);
        }
        //꽃잎 남아있다면
        else if (flower_index < flower_num)
        {
            //타이머 될때마다 빛나기! + 꽃잎 사라짐
            if (light_timer.AutoTimer())
            {
                se_manager.Play(SEManager.Flower.Middle);
                leaf_render.sprite = sprites[flower_index];
                dark_shader.AddLight(transform.position, light_max, light_delta);
                flower_particle.PlayParticle(transform.position);
                flower_index++;
            }
        }
	}
}
