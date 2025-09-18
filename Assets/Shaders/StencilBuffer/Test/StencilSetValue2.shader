// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Stencils/StencilSetValue2"
{
    Properties
    {
        _StencilMask("Stencil Mask", Int) = 0
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "Queue" = "Geometry-50"
        }
        ColorMask 0
        Stencil
        {
            Ref[_StencilMask]
            Comp always
            Pass replace
            //ZFail replace
        }
 
        Pass
        {
            Blend Zero One
            Ztest Always
            
            Stencil{
                Ref [_StencilID]
                Comp Always
                Pass Replace
                Fail Replace
                ZFail Replace
            }
            Name "DepthNormalsOnly"
            Tags
            {
                "LightMode" = "DepthNormalsOnly"
            }

            HLSLPROGRAM

            // -------------------------------------
            // Shader Stages
            #pragma vertex DepthNormalsVertex
            #pragma fragment DepthNormalsFragment


            // -------------------------------------
            // Universal Pipeline keywords
            #pragma multi_compile_fragment _ _GBUFFER_NORMALS_OCT // forward-only variant
            #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/RenderingLayers.hlsl"

            #include "Packages/com.unity.render-pipelines.universal/Shaders/UnlitDepthNormalsPass.hlsl"
            ENDHLSL
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
            struct appdata
            {
                float4 vertex : POSITION;
            };
 
            struct v2f
            {
                float4 pos : SV_POSITION;
            };
 
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
 
            half4 frag(v2f i) : COLOR
            {
                return half4(1,1,0,1);
            }
            
            ENDCG
        }
    }
}