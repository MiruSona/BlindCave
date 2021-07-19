using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkShader : SingleTon<DarkShader>{

    public Material material;

    private const int light_num = 20;
    private Vector2[] pos = new Vector2[light_num];
    private float[] size = new float[light_num];
    private float[] size_max = new float[light_num];
    private float[] size_delta = new float[light_num]; //0.004f;
    private bool[] size_up = new bool[light_num];
    private Vector4[] lights = new Vector4[light_num];
    
    //다크 쉐이더
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        for (int i = 0; i < light_num; i++)
        {
            if (lights[i].w == 1)
            {
                lights[i] = Camera.main.WorldToViewportPoint(pos[i]);
                lights[i].w = 1;
                if (size_up[i])
                {
                    if (size[i] < size_max[i])
                        size[i] += size_delta[i];
                    else
                        size_up[i] = false;
                }
                else
                {
                    if (size[i] > size_delta[i])
                        size[i] -= size_delta[i] / 2;
                    else
                    {
                        size[i] = 0;
                        lights[i].w = 0;
                    }
                }
                lights[i].z = size[i];
            }
        }
        material.SetVectorArray("_MechLights", lights);

        Graphics.Blit(source, destination, material);
    }

    void Awake()
    {
        //화면 비율
        material.SetFloat("_AspectRatio", (float)Screen.width / (float)Screen.height);
    }

    //라이트 추가
    public void AddLight(Vector2 _pos, float _size_max, float _size_delta)
    {
        int empty_index = -1;
        for (int i = 0; i < light_num; i++)
        {
            if (lights[i].w == 0)
            {
                empty_index = i;
                break;
            }
        }

        if (empty_index != -1)
        {
            pos[empty_index] = _pos;
            //보정
            pos[empty_index].y *= -1f;
            size_max[empty_index] = _size_max;
            size_delta[empty_index] = _size_delta;
            size_up[empty_index] = true;
            lights[empty_index].w = 1;
        }
    }
}
