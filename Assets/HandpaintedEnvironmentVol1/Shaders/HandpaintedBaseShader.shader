// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "HandpaintedVol1/HandpaintedBaseShader"
{
    Properties
    {
        _ColorTint("Color Tint", Color) = (1,1,1,0)
        _Albedo("Albedo", 2D) = "white" {}
        [Normal]_Normal("Normal", 2D) = "bump" {}
        _NormalIntensity("Normal Intensity", Range(-2, 2)) = 1
        _Metallic("Metallic", 2D) = "white" {}
        _MetallicIntensity("Metallic Intensity", Range(0, 2)) = 1
        _Roughness("Roughness", 2D) = "white" {}
        _RoughnessIntensity("Roughness Intensity", Range(-2, 2)) = 1
        _Emissive("Emissive", 2D) = "white" {}
        _EmissiveIntensity("Emissive Intensity", Range(0, 10)) = 0
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            Name "Unlit"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // === SRP Batcher compatibility ===
            CBUFFER_START(UnityPerMaterial)
                float4 _ColorTint;
                float _NormalIntensity;
                float _MetallicIntensity;
                float _RoughnessIntensity;
                float _EmissiveIntensity;
            CBUFFER_END

            TEXTURE2D(_Albedo);       SAMPLER(sampler_Albedo);
            TEXTURE2D(_Normal);       SAMPLER(sampler_Normal);
            TEXTURE2D(_Metallic);     SAMPLER(sampler_Metallic);
            TEXTURE2D(_Roughness);    SAMPLER(sampler_Roughness);
            TEXTURE2D(_Emissive);     SAMPLER(sampler_Emissive);

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float3 albedo = SAMPLE_TEXTURE2D(_Albedo, sampler_Albedo, IN.uv).rgb * _ColorTint.rgb;
                float3 emissive = SAMPLE_TEXTURE2D(_Emissive, sampler_Emissive, IN.uv).rgb * _EmissiveIntensity;

                // Unlit final color (Emissive only)
                half3 color = albedo + emissive;

                return half4(color, 1.0);
            }

            ENDHLSL
        }
    }

    FallBack "Hidden/InternalErrorShader"
}
/*ASEBEGIN
Version=17700
418;73;2563;1733;1531.278;983.3434;1;True;True
Node;AmplifyShaderEditor.SamplerNode;6;-668.5,663;Inherit;True;Property;_Roughness;Roughness;6;0;Create;True;0;0;False;0;-1;None;56a95642565f5224da482eb9ab92b282;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;16;-986.7707,1212.24;Inherit;True;Property;_Emissive;Emissive;8;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;5;-860.5,310;Inherit;True;Property;_Metallic;Metallic;4;0;Create;True;0;0;False;0;-1;None;0945608d5a414bf4395bab61c1b145a2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;9;-841.5,515.3948;Float;False;Property;_MetallicIntensity;Metallic Intensity;5;0;Create;True;0;0;False;0;1;0.592;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-698.6713,1348.734;Float;False;Property;_EmissiveIntensity;Emissive Intensity;9;0;Create;True;0;0;False;0;0;0;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;3;-839.5,10;Inherit;True;Property;_Normal;Normal;2;1;[Normal];Create;True;0;0;False;0;-1;None;f1d626aacc218d3449b5c35f6aec611d;True;0;True;bump;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;7;-329.5,692;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-649.5,876;Float;False;Property;_RoughnessIntensity;Roughness Intensity;7;0;Create;True;0;0;False;0;1;1;-2;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;14;-776.5,-159;Float;False;Property;_ColorTint;Color Tint;0;0;Create;True;0;0;False;0;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-866.5,-358;Inherit;True;Property;_Albedo;Albedo;1;0;Create;True;0;0;False;0;-1;None;8f13b261cd2ed414b815eda38cbe11c8;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;11;-825.5,211;Float;False;Property;_NormalIntensity;Normal Intensity;3;0;Create;True;0;0;False;0;1;1;-2;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-341.3326,1217.767;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-391.5,-253;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-331.5,347;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-126.5,692;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.UnpackScaleNormalNode;10;-456.8988,15.51579;Inherit;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;38.54789,-259.1736;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;HandpaintedVol1/HandpaintedBaseShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;7;0;6;1
WireConnection;18;0;16;0
WireConnection;18;1;17;0
WireConnection;15;0;1;0
WireConnection;15;1;14;0
WireConnection;8;0;5;1
WireConnection;8;1;9;0
WireConnection;12;0;7;0
WireConnection;12;1;13;0
WireConnection;10;0;3;0
WireConnection;10;1;11;0
WireConnection;0;0;15;0
WireConnection;0;1;10;0
WireConnection;0;2;18;0
WireConnection;0;3;8;0
WireConnection;0;4;12;0
ASEEND*/
//CHKSM=AC558A9E1C6E1F3A4645F78D48690B3072DE15C7