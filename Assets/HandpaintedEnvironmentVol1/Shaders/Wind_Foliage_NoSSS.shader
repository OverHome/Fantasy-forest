// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "HandpaintedVol1/WindFoliage_NoSSS"
{
    Properties
    {
        _Cutoff( "Mask Clip Value", Float ) = 0.5
        _AlbedoTint("AlbedoTint", Color) = (1,1,1,0)
        _MainTex("_MainTex", 2D) = "white" {}
        _AlbedoMask("AlbedoMask", 2D) = "white" {}
        _LeafSpeed("Leaf Speed", Range( 0 , 1)) = 0.3
        _LeafWindStrength("Leaf Wind Strength", Range( 0 , 0.5)) = 0.05
        _WindSpeed("Wind Speed", Range( 0 , 1)) = 0.3
        _WindStrength("Wind Strength", Range( 0 , 1)) = 0.3
        _WindMultiplier("Wind Multiplier", Float) = 2
        [HideInInspector] _texcoord( "", 2D ) = "white" {}
        [HideInInspector] __dirty( "", Int ) = 1
    }

    SubShader
    {
        Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" }
        Cull Off
        CGPROGRAM
        #include "UnityShaderVariables.cginc"
        #pragma target 3.0
        #pragma surface surf Lambert keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
        struct Input
        {
            float3 worldPos;
            float2 uv_texcoord;
        };

        uniform float _LeafSpeed;
        uniform float _LeafWindStrength;
        uniform half _WindMultiplier;
        uniform half _WindSpeed;
        uniform float _WindStrength;
        uniform float4 _AlbedoTint;
        uniform sampler2D _MainTex;
        uniform float4 _MainTex_ST;
        uniform sampler2D _AlbedoMask;
        uniform float4 _AlbedoMask_ST;
        uniform float _Cutoff = 0.5;

        float3 RotateAroundAxis( float3 center, float3 original, float3 u, float angle )
        {
            original -= center;
            float C = cos( angle );
            float S = sin( angle );
            float t = 1 - C;
            float m00 = t * u.x * u.x + C;
            float m01 = t * u.x * u.y - S * u.z;
            float m02 = t * u.x * u.z + S * u.y;
            float m10 = t * u.x * u.y + S * u.z;
            float m11 = t * u.y * u.y + C;
            float m12 = t * u.y * u.z - S * u.x;
            float m20 = t * u.x * u.z - S * u.y;
            float m21 = t * u.y * u.z + S * u.x;
            float m22 = t * u.z * u.z + C;
            float3x3 finalMatrix = float3x3( m00, m01, m02, m10, m11, m12, m20, m21, m22 );
            return mul( finalMatrix, original ) + center;
        }

        void vertexDataFunc( inout appdata_full v, out Input o )
        {
            UNITY_INITIALIZE_OUTPUT( Input, o );
            float3 ase_vertex3Pos = v.vertex.xyz;
            float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
            float2 appendResult74 = (float2(0.0 , ( v.texcoord.xy.y * cos( ( ( ase_vertex3Pos + ( ( ase_worldPos * 0.3 ) + ( _Time.y * _LeafSpeed ) ) ) / 0.1 ) ) ).x));
            float temp_output_38_0 = ( ( ( ase_worldPos.x + ase_worldPos.z ) * 0.01 ) + ( _Time.y * _WindSpeed ) );
            float temp_output_61_0 = ( ( ( ase_vertex3Pos.y * ( ( ( sin( ( temp_output_38_0 * 4.0 ) ) + sin( ( temp_output_38_0 * 15.0 ) ) ) - cos( ( temp_output_38_0 * 5.0 ) ) ) * 0.1 ) ) * _WindStrength ));
            float4 appendResult65 = (float4(temp_output_61_0 , 0.0 , temp_output_61_0 , 0.0));
            float4 break70 = mul( appendResult65, unity_ObjectToWorld );
            float3 appendResult76 = (float3(break70.x , 0.0 , break70.z));
            float3 rotatedValue78 = RotateAroundAxis( float3( 0,0,0 ), appendResult76, float3( 0,0,0 ), 0.0 );
            v.vertex.xyz += ( float3( ( v.color.g * ( appendResult74 * _LeafWindStrength ) ) ,  0.0 ) + ( ( v.color.r * _WindMultiplier ) * rotatedValue78 ) );
        }

        void surf( Input i , inout SurfaceOutput o )
        {
            float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
            float4 tex2DNode14 = tex2D( _MainTex, uv_MainTex );
            float2 uv_AlbedoMask = i.uv_texcoord * _AlbedoMask_ST.xy + _AlbedoMask_ST.zw;
            float4 lerpResult88 = lerp( ( _AlbedoTint * tex2DNode14 ) , tex2DNode14 , ( 1.0 - tex2D( _AlbedoMask, uv_AlbedoMask ) ));
            o.Albedo = lerpResult88.rgb;
            o.Alpha = 1;
            clip( tex2DNode14.a - _Cutoff );
        }

        ENDCG
    }
    Fallback "Diffuse"
    CustomEditor "ASEMaterialInspector"
}
