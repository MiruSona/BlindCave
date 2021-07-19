using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFPlayer : MonoBehaviour {

    //참조
    private GameManager game_manager;
    private SEManager se_manager;
    private DarkShader dark_shader;
    private ParticleManager light_particle, attacked_particle;
    private PlayerFlower player_flower;

    //데이터
    private PlayerData player_data;

    //컴포넌트
    private Rigidbody2D rb2d;
    private SpriteRenderer render;
    private TrailRenderer trail;
    private Animator anim; 

    //타이머
    private Define.Timer attacked_timer;
    private Define.Timer blink_timer;

    //피격
    private Color origin_color;
    private Color attacked_color;
    private float die_delta = 0.02f;

    //빛
    private float light_max = 0.3f;
    private float light_delta = 0.005f;

    //조절값
    private float scale = 1.5f;
    private float jump_back = 0.5f;
    private float push_back = 3f;

    //초기화
    void Start()
    {
        game_manager = GameManager.instance;
        se_manager = SEManager.instance;
        dark_shader = DarkShader.instance;
        ParticleManager[] temp = FindObjectsOfType<ParticleManager>();
        for(int i = 0; i < temp.Length; i++)
        {
            if (temp[i].name == "LightParticles")
                light_particle = temp[i];
            if (temp[i].name == "AttackedParticles")
                attacked_particle = temp[i];
        }
        player_flower = transform.FindChild("Flower").GetComponent<PlayerFlower>();

        player_data = DataManager.instance.player_data;
        rb2d = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        trail = GetComponent<TrailRenderer>();
        anim = GetComponent<Animator>();

        attacked_timer = new Define.Timer(0, 1.5f);
        blink_timer = new Define.Timer(0, 0.15f);

        trail.sortingOrder = -2;
        origin_color = render.color;
        attacked_color = new Color(1f, 1f, 1f, 0.3f);
    }

    //업데이트
    void Update()
    {
        //플레이 상태 아니면 중지
        if (game_manager.state != GameManager.State.Play)
        {
            rb2d.velocity = Vector2.zero;
            rb2d.gravityScale = 0;
            return;
        }

        #region 상태처리
        switch (player_data.state)
        {
            case PlayerData.State.Alive:

                break;
            case PlayerData.State.Attacked:
                Attacked();
                break;
            case PlayerData.State.Die:
                MoveStop();
                Die();
                return;
        }
        #endregion

        //애니메이션 처리
        if (rb2d.velocity.y >= 0)
            anim.SetBool("Jump", true);
        else
            anim.SetBool("Jump", false);

        //움직임 처리
        InputKey();
    }

    //입력 처리
    private void InputKey()
    {
        //좌,우 움직임
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            player_data.facing = player_data.LEFT;
            Move();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            player_data.facing = player_data.RIGHT;
            Move();
        }

        //아무것도 안누르면 멈춘다!
        if (!Input.anyKey)
            MoveStop();
    }    

    #region 충돌처리
    private void OnCollisionEnter2D(Collision2D _col)
    {
        //죽는 곳일 경우
        if (_col.collider.CompareTag("DeadZone"))
            player_data.SubHP(player_data.hp_max);

        if (_col.collider.CompareTag("Ground"))
        {
            if (player_data.state != PlayerData.State.Die)
            {
                //빛나게
                dark_shader.AddLight(transform.position, light_max, light_delta);
                light_particle.PlayParticle(transform.position);
                //땅표시
                player_data.ground = true;
                //점프!
                Jump();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D _col)
    {
        if (_col.collider.CompareTag("Ground"))
            player_data.ground = false;
    }

    private void OnTriggerEnter2D(Collider2D _col)
    {
        //꽃일 경우
        if (_col.CompareTag("Item"))
        {
            if (_col.name.Contains("BlueFlower"))
            {
                //꽃 켜짐
                if (!player_flower.gameObject.activeSelf)
                    player_flower.gameObject.SetActive(true);
                //초기화
                player_flower.Init();
            }
        }

        //몬스터일 경우
        if (_col.CompareTag("Monster"))
        {
            MonsterData md = _col.GetComponent<PFMonster>().monster_data;
            //내려가는 중이면 공격!
            if (rb2d.velocity.y < 0f)
            {
                if (md.state == MonsterData.State.Alive)
                {
                    //공격!
                    md.SubHP(player_data.atk);

                    //현재속도 받기
                    Vector2 velocity = rb2d.velocity;
                    //점프!
                    velocity.y = player_data.jump_power * jump_back;
                    rb2d.velocity = velocity;

                    //움직임 표시
                    player_data.move = true;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D _col)
    {
        //몬스터일 경우
        if (_col.CompareTag("Monster"))
        {
            MonsterData md = _col.GetComponent<PFMonster>().monster_data;
            //내려가는 중이 아니면 공격당함!
            if (rb2d.velocity.y >= 0f && md.state == MonsterData.State.Alive)
                player_data.SubHP(md.atk);
        }
    }
    #endregion

    #region 움직임
    //멈춤
    private void MoveStop()
    {
        //안움직임 표시
        player_data.move = false;
        //현재속도 받기
        Vector2 velocity = rb2d.velocity;
        //0값 대입
        velocity.x = 0f;
        rb2d.velocity = velocity;
    }

    //플랫포머 움직임
    private void Move()
    {
        //움직임 표시
        player_data.move = true;
        //현재속도 받기
        Vector2 velocity = rb2d.velocity;
        //속도 * 방향값 대입
        velocity.x = player_data.speed * player_data.facing;
        rb2d.velocity = velocity;

        //좌우 변경
        transform.localScale = new Vector3(player_data.facing, 1, 1) * scale;
    }

    //플랫포머 점프
    private void Jump()
    {
        //땅인지 체크
        if (player_data.ground)
        {
            se_manager.Play(SEManager.Player.Jump);
            //현재속도 받기
            Vector2 velocity = rb2d.velocity;
            //점프!
            velocity.y = player_data.jump_power;
            rb2d.velocity = velocity;

            //움직임 표시
            player_data.move = true;
        }
    }
    
    #endregion

    //피격
    private void Attacked()
    {
        //맨처음 뒤로 밀리게
        if (attacked_timer.time == 0)
        {
            dark_shader.AddLight(transform.position, light_max, light_delta);
            attacked_particle.PlayParticle(transform.position);
            se_manager.Play(SEManager.Player.Attacked);
            player_data.ground = false;
            Vector2 force = new Vector2(-player_data.facing, 2f) * push_back;
            rb2d.velocity = force;
        }

        //깜빡이게
        if (!attacked_timer.AutoTimer())
        {
            if (blink_timer.AutoTimer())
            {
                if (render.color == origin_color)
                    render.color = attacked_color;
                else
                    render.color = origin_color;
            }
        }
        //다 깜빡이면 돌아옴
        else
        {
            render.color = origin_color;
            player_data.state = PlayerData.State.Alive;
        }
    }

    //죽음
    private void Die()
    {
        Color color = render.color;
        //처음 한번 실행
        if (render.color.a == 1f)
        {
            dark_shader.AddLight(transform.position, light_max, light_delta);
            attacked_particle.PlayParticle(transform.position);
            se_manager.Play(SEManager.Player.Die);
        }

        //완전 투명해질때까지--
        if (render.color.a > 0f)
        {
            color.a -= die_delta;
            render.color = color;
        }
        else
        {
            gameObject.SetActive(false);
            game_manager.state = GameManager.State.Die;
        }
    }
}
