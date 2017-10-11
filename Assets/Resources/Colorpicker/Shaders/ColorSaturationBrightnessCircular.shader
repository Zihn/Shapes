// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "ColorPicker/ColorSaturationBrightnessCircular" {
	Properties {
	    _Color ("Main Color", Color) = (0.75,0.15,0.56,1)
	    _XPosition ("x offset rectTransform", Float) = 0.0
	    _YPosition ("y offset rectTransform", Float) = 0.0
	    _Width ("Width rectTransform", Float) = 0.0
	    _Height ("Height rectTransform", Float) = 0.0
	}
	SubShader {
	    Pass {	
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			uniform float _XPosition;
			uniform float _YPosition;
			uniform float _Width;
			uniform float _Height;
			
			float4 _Color;
			
			// vertex input: position, UV
			struct appdata {
			    float4 vertex : POSITION;
			    float4 texcoord : TEXCOORD0;
			};
	
			struct pos_output {
			    float4 pos : SV_POSITION;
//			    float4 uv : TEXCOORD0;
			    float4 wp : pos;
			};
			
			pos_output vert(appdata v) {
			    pos_output o;
			    o.pos = UnityObjectToClipPos(v.vertex);
//			    o.uv = float4(v.texcoord.xy, 0, 0);
			    o.wp = mul(unity_ObjectToWorld, v.vertex);
			    return o;
			}
			
			half4 frag(pos_output o) : COLOR {
				float y = (o.wp.y - _XPosition)/_Height;
				float x = (o.wp.x - _YPosition)/_Width;
				half4 c = y + (_Color - 1)*x;
			    return c;			
			}
			ENDCG
	    }
	}
}
