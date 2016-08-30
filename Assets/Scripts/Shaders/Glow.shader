Shader "Custom/Glow" {
	Properties {
		[Toggle] _IsGlow("is Glow",Int)=0
		_ColorTint ("Color Tint", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BumpMap("Normal Map",2D) = "bump"{}
		_RimColor("Rim Color",Color) = (1,1,1,1)
		_RimPower("Rim Power",Range(1.0,6.0))=3.0
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		


		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		struct Input 
		{
			int IsGlow;
			float4 color:Color;
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float3 viewDir;
		};
		int _IsGlow;
		float4 _ColorTint;
		sampler2D _MainTex;
		sampler2D _BumpMap;
		float4 _RimColor;
		float _RimPower;

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			IN.color = _ColorTint;
			IN.IsGlow = _IsGlow;
			
				// Albedo comes from a texture tinted by color
				
				if (IN.IsGlow == 0)
				{
					
					o.Albedo = tex2D(_MainTex, IN.uv_MainTex);
					o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
					half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
					o.Emission = (0, 0, 0, 0);
				}
				else
				{

					o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb*IN.color;
					o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
					half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
					o.Emission = _RimColor.rgb *pow(rim, _RimPower);
				}
				//o.Emission = _RimColor.rgb *pow(rim, _RimPower);
			
			
		}
		ENDCG
	}
	FallBack "Diffuse"
}
