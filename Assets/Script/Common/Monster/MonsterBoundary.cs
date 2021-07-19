using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBoundary : MonoBehaviour {

    //인덱스
    private int id = 0;
    
    public void Init(int _id)
    {
        id = _id;
    }

    public int GetId()
    {
        return id;
    }
}
