using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour {

    //참조
    private PlayerData player_data;

    //데이터
    private ThornData thorn_data;

    //초기화
	void Start () {
        player_data = DataManager.instance.player_data;
        thorn_data = new ThornData();
    }
    
    private void OnTriggerStay2D(Collider2D _col)
    {
        if (_col.CompareTag("Player"))
        {
            player_data.SubHP(thorn_data.atk);
        }
    }
}
