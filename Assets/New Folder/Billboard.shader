﻿Shader "Unlit/Billboard"
{
	Properties{
		_MainTex("Texture Image", 2D) = "white"
	}
		SubShader{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		Pass{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

		uniform sampler2D _MainTex;

	struct vertexInput {
		float4 vertex : POSITION;
		float4 tex : TEXCOORD0;
	};
	struct vertexOutput {
		float4 pos : SV_POSITION;
		float4 tex : TEXCOORD0;
	};

	vertexOutput vert(vertexInput input)
	{
		vertexOutput output;
		output.pos = mul(UNITY_MATRIX_P,
			mul(UNITY_MATRIX_MV, float4(0.0, 0.0, 0.0, 1.0))
			+ float4(input.vertex.x, input.vertex.y, 0.0, 0.0));

		output.tex = input.tex;

		return output;
	}

	float4 frag(vertexOutput input) : COLOR
	{
		float4 ret = tex2D(_MainTex, float2(input.tex.xy));
	ret.a = 0.5;
	return ret;
	}

		ENDCG
	}
	}
}
