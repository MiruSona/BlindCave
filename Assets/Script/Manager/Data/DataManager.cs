using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingleTon<DataManager> {

    [HideInInspector]
    public PlayerData player_data;

	//초기화
	void Awake () {
        player_data = new PlayerData();
    }
}
