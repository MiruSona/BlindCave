using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvas : MonoBehaviour {

    //참조
    private PlayerData player_data;
    private Image life;

	//초기화
	void Start () {
        player_data = DataManager.instance.player_data;
        life = transform.FindChild("Life").GetComponent<Image>();
    }
	
	//라이프에 따라 표시
	void Update () {
        life.fillAmount = player_data.hp / player_data.hp_max;
	}
}
