using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFMonsterSpawn : MonoBehaviour {

    //참조
    public PFMonster monster;

    //체크값
    private const int num_max = 3;
    private int num = 0;

    //타이머
    private Define.Timer spawn_timer;

    //초기화
    void Start()
    {
        int id = gameObject.GetInstanceID();
        transform.FindChild("LeftBoundary").GetComponent<MonsterBoundary>().Init(id);
        transform.FindChild("RightBoundary").GetComponent<MonsterBoundary>().Init(id);

        spawn_timer = new Define.Timer(0, 3f);
    }

    //몬스터 소환
    void Update()
    {
        if (num < num_max)
        {
            if (spawn_timer.AutoTimer())
            {
                PFMonster mon = Instantiate(monster);
                mon.Init(this, transform.position, gameObject.GetInstanceID(), num);
                num++;
            }
        }
    }

    //몬스터 죽음 처리
    public void DieMonster()
    {
        if (num > 0)
            num--;
    }
}
