using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFMonster : MonoBehaviour {

    //참조
    private PFMonsterSpawn monster_spawn;
    private SEManager se_manager;

    //데이터
    [HideInInspector]
    public MonsterData monster_data;

    //컴포넌트
    private Rigidbody2D rb2d;
    private SpriteRenderer render;
    private Collider2D col;

    //타이머
    private Define.Timer attacked_timer;
    private Define.Timer blink_timer;

    //피격
    private Color origin_color;
    private Color attacked_color;
    private float alpha_delta = 0.02f;
    private float push_back = 1.5f;

    //값
    private int spawn_id = 0;
    private int depth = 0;
    private float color_delta = 0.5f;
    private Vector2 init_pos;

    //외부 초기화
    public void Init(PFMonsterSpawn _monster_spawn, Vector2 _init_pos, int _spawn_id, int _depth)
    {
        monster_spawn = _monster_spawn;
        init_pos = _init_pos;
        spawn_id = _spawn_id;
        depth = _depth;
    }

    //초기화
    void Start()
    {
        se_manager = SEManager.instance;

        monster_data = new MonsterData();
        rb2d = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        transform.position = init_pos;
        render.sortingOrder = -depth;

        attacked_timer = new Define.Timer(0, 1.5f);
        blink_timer = new Define.Timer(0, 0.15f);

        attacked_color = new Color(1f, 1f, 1f, 0.3f);

        //초기 색 설정(깊이따라, 투명도 0으로)
        Color color = render.color;
        if (depth != 0)
            color *= 1.0f / ((float)depth + color_delta);
        origin_color = color;   //바뀐색으로 설정
        color.a = 0;            //처음 생성시 투명하게
        render.color = color;

        //초기 방향 설정(랜덤)
        int random = Random.Range(0, 2);
        if (random == 0)
            monster_data.facing = monster_data.LEFT;
        else
            monster_data.facing = monster_data.RIGHT;
    }

    //업데이트
    void Update()
    {
        #region 상태처리
        switch (monster_data.state)
        {
            case MonsterData.State.Create:
                Create();
                return;
            case MonsterData.State.Alive:
                //움직임 처리
                Move();
                break;
            case MonsterData.State.Attacked:
                MoveStop();
                Attacked();
                break;
            case MonsterData.State.Die:
                MoveStop();
                Die();
                return;
        }
        #endregion
    }

    #region 충돌처리
    private void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.name == "LeftBoundary")
        {
            MonsterBoundary mb = _col.GetComponent<MonsterBoundary>();
            if (mb.GetId() == spawn_id)
                monster_data.facing = monster_data.RIGHT;
        }
        if (_col.name == "RightBoundary")
        {
            MonsterBoundary mb = _col.GetComponent<MonsterBoundary>();
            if (mb.GetId() == spawn_id)
                monster_data.facing = monster_data.LEFT;
        }

        if (_col.CompareTag("Ground"))
        {
            //땅인거 표시
            monster_data.ground = true;
            //중력값 0으로
            rb2d.gravityScale = 0;
            //현재속도 받기
            Vector2 velocity = rb2d.velocity;
            //0값 대입
            velocity.y = 0f;
            rb2d.velocity = velocity;
        }
    }

    private void OnTriggerExit2D(Collider2D _col)
    {
        if (_col.CompareTag("Ground"))
        {
            //중력값 2로
            rb2d.gravityScale = 1;
        }
    }
    #endregion

    #region 움직임
    //멈춤
    private void MoveStop()
    {
        //피격중이면서 땅이 아니면 실행X(날아가게 하기 위해))
        if (monster_data.state == MonsterData.State.Attacked && !monster_data.ground)
            return;

        //현재속도 받기
        Vector2 velocity = rb2d.velocity;
        //0값 대입
        velocity.x = 0f;
        rb2d.velocity = velocity;
    }

    //움직임
    private void Move()
    {
        //현재속도 받기
        Vector2 velocity = rb2d.velocity;
        //속도 * 방향값 대입
        velocity.x = monster_data.speed * monster_data.facing;
        rb2d.velocity = velocity;
    }
    #endregion

    //피격
    private void Attacked()
    {
        //맨처음 뒤로 밀리게
        if (attacked_timer.time == 0)
        {
            se_manager.Play(SEManager.Monster.Attacked);
            monster_data.ground = false;
            Vector2 force = new Vector2(-monster_data.facing, 1f) * push_back;
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
        //다되면 원상태로
        else
        {
            render.color = origin_color;
            monster_data.state = MonsterData.State.Alive;
        }
    }

    //생성
    private void Create()
    {
        Color color = render.color;
        if (render.color.a < 1f)
        {
            color.a += alpha_delta;
            render.color = color;
        }
        else
        {
            monster_data.state = MonsterData.State.Alive;
        }

    }

    //죽음
    private void Die()
    {
        Color color = render.color;
        //처음 한번 실행
        if (render.color.a == 1f)
        {
            se_manager.Play(SEManager.Monster.Die);
        }

        if (render.color.a > 0f)
        {
            col.enabled = false;
            color.a -= alpha_delta;
            render.color = color;
        }
        else
        {
            monster_spawn.DieMonster();
            Destroy(gameObject);
        }
    }
}
