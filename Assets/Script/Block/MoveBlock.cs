using UnityEngine;
using System.Collections;

public class MoveBlock : MonoBehaviour {

    //참조
    private GameObject body;
    private Transform[] points;

    //컴포넌트
    private LineRenderer[] line_renderers;

    //값
    private float speed = 2.0f;
    private bool move_right = true;

	//초기화
	void Start () {
        body = transform.FindChild("Body").gameObject;
        points = transform.FindChild("MovePoint").GetComponentsInChildren<Transform>();
        line_renderers = transform.FindChild("MovePoint").GetComponentsInChildren<LineRenderer>();
        DrawLines();
    }
	
	void Update () {
        //움직이는 방향 설정
        if (CheckVector3(body.transform.localPosition, points[0].localPosition))
            move_right = true;
        if (CheckVector3(body.transform.localPosition, points[points.Length - 1].localPosition))
            move_right = false;

        //정방향이면 정방향으로, 아니면 역방향으로 움직
        if (move_right)
            MoveRight();
        else
            MoveReverse();
    }

    private bool CheckVector3(Vector3 _target1, Vector3 _target2)
    {
        bool send_bool = false;
        Vector3 value = new Vector3(0.01f, 0.01f, 0.01f);
        Vector3 min = _target2 - value;
        Vector3 max = _target2 + value;

        if (min.x <= _target1.x && _target1.x <= max.x)
        {
            if (min.y <= _target1.y && _target1.y <= max.y)
                send_bool = true;
        }

        return send_bool;
    }

    //정방향 움직
    private void MoveRight()
    {
        for(int i = 0; i < points.Length; i++)
        {
            if (i < points.Length - 1)
            {
                if (CheckVector3(body.transform.localPosition, points[i].localPosition))
                    iTween.MoveTo(body, iTween.Hash("position", points[i + 1].position, "easetype", iTween.EaseType.linear, "speed", speed));
            }
        }
    }

    //역방향 움직
    private void MoveReverse()
    {
        for (int i = 0; i < points.Length; i++)
        {
            if (i > 0)
            {
                if (CheckVector3(body.transform.localPosition, points[i].localPosition))
                    iTween.MoveTo(body, iTween.Hash("position", points[i - 1].position, "easetype", iTween.EaseType.linear, "speed", speed));
            }
        }
    }

    //선 그리기
    private void DrawLines()
    {
        for (int i = 0; i < points.Length; i++)
        {
            if (i < points.Length - 1)
            {
                line_renderers[i].sortingLayerName = "Background";
                line_renderers[i].SetPosition(0, points[i].position);
                line_renderers[i].SetPosition(1, points[i + 1].position);
            }
        }
    }
}
