Shader "Carpets"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_MaskClipValue( "Mask Clip Value", Float ) = 0.26
		_Color("Color", Color) = (0,0,0,0)
		_ColorIntensity("Color Intensity", Range( 0 , 1)) = 0
		_Albedo("Albedo", 2D) = "white" {}
		_AoSmoothnesMetalic("Ao Smoothnes Metalic", 2D) = "white" {}
		_SmoothIntensity("Smooth Intensity", Range( 0 , 2)) = 0
		_Opacity("Opacity", 2D) = "white" {}
		_OacityIntensity("Oacity Intensity", Range( 0 , 1)) = 0
		_Normal("Normal", 2D) = "bump" {}
		_NormalIntensity("Normal Intensity", Range( 0 , 2)) = 0
		_NormalDetail("Normal Detail", 2D) = "bump" {}
		_NormalDetailIntensity("Normal Detail Intensity", Range( 0 , 2)) = 0
		_OrnamentBase("Ornament Base", 2D) = "white" {}
		_OrnamentBaseColor("Ornament Base Color", Color) = (0,0,0,0)
		_OrnamentBaseIntensity("Ornament Base Intensity", Range( 0 , 1)) = 0
		_OrnamentEdgeColor("Ornament Edge Color", Color) = (0,0,0,0)
		_OrnamentEdgeIntensity("Ornament Edge Intensity", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _NormalIntensity;
		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform float _NormalDetailIntensity;
		uniform sampler2D _NormalDetail;
		uniform float4 _NormalDetail_ST;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float4 _Color;
		uniform float _ColorIntensity;
		uniform float4 _OrnamentEdgeColor;
		uniform sampler2D _AoSmoothnesMetalic;
		uniform float4 _AoSmoothnesMetalic_ST;
		uniform float _OrnamentEdgeIntensity;
		uniform float4 _OrnamentBaseColor;
		uniform sampler2D _OrnamentBase;
		uniform float4 _OrnamentBase_ST;
		uniform float _OrnamentBaseIntensity;
		uniform float _SmoothIntensity;
		uniform sampler2D _Opacity;
		uniform float4 _Opacity_ST;
		uniform float _OacityIntensity;
		uniform float _MaskClipValue = 0.26;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			float2 uv_NormalDetail = i.uv_texcoord * _NormalDetail_ST.xy + _NormalDetail_ST.zw;
			o.Normal = BlendNormals( UnpackScaleNormal( tex2D( _Normal, uv_Normal ) ,_NormalIntensity ) , UnpackScaleNormal( tex2D( _NormalDetail, uv_NormalDetail ) ,_NormalDetailIntensity ) );
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float4 tex2DNode2 = tex2D( _Albedo, uv_Albedo );
			float2 uv_AoSmoothnesMetalic = i.uv_texcoord * _AoSmoothnesMetalic_ST.xy + _AoSmoothnesMetalic_ST.zw;
			float4 tex2DNode22 = tex2D( _AoSmoothnesMetalic, uv_AoSmoothnesMetalic );
			float4 temp_cast_0 = (0.0).xxxx;
			float2 uv_OrnamentBase = i.uv_texcoord * _OrnamentBase_ST.xy + _OrnamentBase_ST.zw;
			o.Albedo = lerp( lerp( lerp( tex2DNode2 , ( tex2DNode2 * _Color ) , _ColorIntensity ) , _OrnamentEdgeColor , lerp( 0.0 , ( 1.0 - tex2DNode22.a ) , _OrnamentEdgeIntensity ) ) , _OrnamentBaseColor , lerp( temp_cast_0 , ( tex2D( _OrnamentBase, uv_OrnamentBase ) * tex2DNode22.a ) , _OrnamentBaseIntensity ).r ).rgb;
			o.Metallic = tex2DNode22.b;
			o.Smoothness = lerp( 0.0 , tex2DNode22.g , _SmoothIntensity );
			o.Occlusion = tex2DNode22.r;
			o.Alpha = 1;
			float2 uv_Opacity = i.uv_texcoord * _Opacity_ST.xy + _Opacity_ST.zw;
			clip( pow( tex2D( _Opacity, uv_Opacity ).r , _OacityIntensity ) - _MaskClipValue );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
