using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingleTon<GameManager> {
    
    //게임 상태
    public enum State
    {
        Play,
        Goal,
        Die
    }
    [HideInInspector]
    public State state = State.Play;

    //스테이지
    [HideInInspector]
    public int current_stage = 0;
    private const int end_stage = 5;

    //죽을 경우
    private Define.Timer die_delay = new Define.Timer(0, 3.5f);

    //오브젝트 파괴 안되게
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    //죽으면 스테이지 재시작
    void Update()
    {
        //종료!
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if(state == State.Die)
        {
            if (die_delay.AutoTimer())
                RestartStage();
        }
    }

    //초기화
    private void Init()
    {
        DataManager.instance.player_data.Init();
        state = State.Play;
    }

    //다음 스테이지
    public void NextStage()
    {
        if (current_stage < end_stage)
            current_stage++;
        else
            current_stage = 0;
        
        SceneManager.LoadScene(current_stage);
        Init();
    }

    //스테이지 재시작
    public void RestartStage()
    {
        SceneManager.LoadScene(current_stage);
        Init();
    }
}
