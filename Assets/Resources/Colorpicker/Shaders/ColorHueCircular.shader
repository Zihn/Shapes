// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_CameraToWorld' with 'unity_CameraToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "ColorPicker/ColorHueCircular" {
	Properties {
	      _XPosition ("x offset rectTransform", Float) = 0.0
	      _YPosition ("y offset rectTransform", Float) = 0.0
	      _Rotation	("Orientation of the shader in rad", Float) = 0.0
	}
	SubShader {
	    Pass {	
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#define M_PI 3.1415926535897932384626433832795		
			uniform float _XPosition;
			uniform float _YPosition;
			uniform float _Rotation;
	
			struct pos_output {
			    float4 pos : SV_POSITION;
			    float4 wp : pos;
			};
			
			pos_output vert(appdata_full v) {
			    pos_output o;
			    o.pos = UnityObjectToClipPos(v.vertex);
			    o.wp = mul(unity_ObjectToWorld, v.vertex);
			    return o;
			}
			
			half4 frag(pos_output o) : COLOR {
				half ang = atan2(o.wp.y - _YPosition, o.wp.x - _XPosition) + _Rotation + M_PI;
				ang = (ang > 0 ? ang : (2*M_PI + ang));
				ang = (ang < 2*M_PI ? ang : ang - 2*M_PI);
				ang = ang / (2*M_PI);
				half p = floor(ang*6);
				half i = ang*6-p;
				half4 c = p == 0 ? half4(1, i, 0, 1) :
						  p == 1 ? half4(1-i, 1, 0, 1) :
						  p == 2 ? half4(0, 1, i, 1) :
						  p == 3 ? half4(0, 1-i, 1, 1) :
						  p == 4 ? half4(i, 0, 1, 1) :
						  p == 5 ? half4(1, 0, 1-i, 1) :
						           half4(1, 0, 0, 1);
			    return c;
			}
			ENDCG
	    }
	}
}

