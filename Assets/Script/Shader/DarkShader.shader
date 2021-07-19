Shader "Custom/DarkShader" {	
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
	}

	SubShader{
		Pass{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;

			int index = 0;
			float4 _MechLights[20];
			float _AspectRatio;

			float4 frag(v2f_img i) : COLOR{
				float4 c = tex2D(_MainTex, i.uv);
				float2 ratio = float2(1, 1 / _AspectRatio);
				float delta = 0;
				float ray = 0;
				int index = 0;

				for (index = 0; index < 20; index++) {
					ray = length((_MechLights[index].xy - i.uv.xy) * ratio);
					delta += smoothstep(_MechLights[index].z, 0, ray) * _MechLights[index].w;
				}

				c.rgb *= delta * 0.6f;
				return c;
			}
			ENDCG
		}
	}
}
