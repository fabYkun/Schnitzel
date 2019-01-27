// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprites/Synesthesie"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		_Brightness("Sinusoid Tint", Color) = (0.8,0.8,0.8,1)
		_ResolutionX("Resolution X", Float) = 100
		_ResolutionY("Resolution Y", Float) = 100
		_Sides("Shape Sides", Range(3,15)) = 3
		_Frequency("Sinusoid Frequency", Range(3,30)) = 15
		_Speed("Sinusoid Speed", Range(5,35)) = 20
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}

			Cull Off
			Lighting Off
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			Pass
			{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile _ PIXELSNAP_ON
				#include "UnityCG.cginc"

				struct appdata_t
				{
					float4 vertex   : POSITION;
					float4 color    : COLOR;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					fixed4 color : COLOR;
					float2 texcoord  : TEXCOORD0;
				};

				fixed4 _Color;

				v2f vert(appdata_t IN)
				{
					v2f OUT;
					OUT.vertex = UnityObjectToClipPos(IN.vertex);
					OUT.texcoord = IN.texcoord;
					OUT.color = IN.color * _Color;
					#ifdef PIXELSNAP_ON
					OUT.vertex = UnityPixelSnap(OUT.vertex);
					#endif

					return OUT;
				}

				sampler2D _MainTex;
				sampler2D _AlphaTex;
				float _AlphaSplitEnabled;
				fixed4 _Brightness;
				float _ResolutionX;
				float _ResolutionY;
				float _Sides;
				float _Frequency;
				float _Speed;

				float sinusoid(float2 v1, float2 v2) 
				{
					return sin(dot(normalize(v1), normalize(v2)) * _Frequency + _Time * _Speed) / 15.;
				}

				float shape(float2 coords, float do_sinusoid, float width, float precision)
				{
					int N = floor(_Sides);
					float angle = atan2(coords.x, coords.y) + 3.1415926535897;
					float radius = 6.28318530718 / N;

					float distance_field = length(coords);
					distance_field += sinusoid(-coords, float2(0,1)) * step(.5, do_sinusoid);
					distance_field -= sinusoid(-coords, float2(1,0)) * step(.5, do_sinusoid);

					float d = cos(floor(.5 + angle / radius) * radius - angle) * distance_field;
					return smoothstep(0.4 - width, 0.40 + precision, d) - smoothstep(.4 + width, .4 + width + precision, d);
				}

				fixed4 frag(v2f IN) : SV_Target
				{
					float2 coords = IN.texcoord / float2(_ResolutionX, _ResolutionY);
					coords.x *= _ResolutionX / _ResolutionY;
					coords = coords * 2. - 1.;

					float mask_static = shape(coords,0,0.025,0.01);
					float mask_static_out_1 = 1-shape(coords*1.005, 0, 0.005, 0.001);
					float mask_static_out_2 = 1-shape(coords*0.935, 0, 0.005, 0.001);
					float mask_moving = shape(coords, 1, 0.035, 0.001);
					float mask_moving_out_1 = 1-shape(coords*1.025, 1, 0.005, 0.001);
					float mask_moving_out_2 = 1-shape(coords*0.945, 1, 0.005, 0.001);

					
					float3 color1 = lerp(float3(0.15, 0.15, 0.15), _Color, mask_moving_out_1 * mask_moving_out_2);
					float3 color2 = lerp(float3(0.15, 0.15, 0.15), _Brightness, mask_static_out_1 * mask_static_out_2);
					float3 color = lerp(color1, color2, mask_static);

					float4 c = float4(color, mask_static + mask_moving);
					return c;

				}
			ENDCG
			}
		}
}