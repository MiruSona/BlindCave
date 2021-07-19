using UnityEngine;
using System.Collections;

public class BrokenBlock : MonoBehaviour {

    //참조
    private SEManager se_manager;

    //컴포넌트
    private BoxCollider2D box_collider;
    private SpriteRenderer sprite_renderer;

    //알파값 관련
    private const float alpha_value = 0.01f;

    //타이머
    private Define.Timer restore_timer = new Define.Timer(0, 3f);

    //상태
    private enum State
    {
        Normal,
        Broken,
        Create
    }
    private State state = State.Normal;

    //초기화
    void Start()
    {
        se_manager = SEManager.instance;

        //컴포넌트
        box_collider = GetComponent<BoxCollider2D>();
        sprite_renderer = GetComponent<SpriteRenderer>();
    }
    
    //페이드 인/아웃 및 컬리더 On/Off
    void Update()
    {
        switch (state)
        {
            //부서질 시
            case State.Broken:

                //충돌판정 끄기
                box_collider.enabled = false;
                //알파값--
                if (sprite_renderer.color.a > 0)
                {
                    Color color = sprite_renderer.color;
                    color.a -= alpha_value;
                    sprite_renderer.color = color;
                }
                else
                    sprite_renderer.color = new Color(1, 1, 1, 0);

                //투명일때 회복시간 다되면 복구
                if (sprite_renderer.color.a == 0f)
                {
                    if(restore_timer.AutoTimer())
                        state = State.Create;
                }
                break;

            //생성 될 시
            case State.Create:
                //알파값++
                if (sprite_renderer.color.a < 1.0f)
                {
                    Color color = sprite_renderer.color;
                    color.a += alpha_value;
                    sprite_renderer.color = color;
                }
                else
                    sprite_renderer.color = Color.white;

                if (sprite_renderer.color.a == 1.0f)
                {
                    state = State.Normal;
                    if (!box_collider.enabled)
                        box_collider.enabled = true;
                }
                break;
        }
    }

    //주인공과 충돌 시 부서지기
    void OnCollisionEnter2D(Collision2D _col)
    {
        if (_col.collider.CompareTag("Player"))
        {
            if(state == State.Normal)
            {
                se_manager.Play(SEManager.Block.Broken);
                state = State.Broken;
            }
        }
    }
}
