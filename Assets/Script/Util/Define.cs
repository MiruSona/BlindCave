using UnityEngine;
using System.Collections;

public static class Define {
    
    //타이머
    public class Timer {

        public float time = 0;
        public float limit = 0;
        public bool end = false;

        public Timer(float _time, float _limit) {
            time = _time;
            limit = _limit;
        }

        //시간 자동 체크
        public bool AutoTimer() {
            end = false;
            if (time < limit)
                time += Time.deltaTime;
            else {
                time = 0;
                end = true;
            }
            return end;
        }

        //시간만++
        public void AddTimer()
        {
            if (time < limit)
                time += Time.deltaTime;
        }

        //시간초기화
        public void InitTimer()
        {
            time = 0;
        }

        //시간 됬는지 체크
        public bool CheckTimer()
        {
            end = false;
            if (time >= limit)
            {
                end = true;
            }
            return end;
        }
    }
}
