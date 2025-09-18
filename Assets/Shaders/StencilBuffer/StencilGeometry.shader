// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/StencilGeometry"
{
    Properties
    {
        [IntRange] _StencilID ("Stencil ID", Range(0,255)) = 0
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "Queue" = "Geometry-50"
        }
        ColorMask 0
        ZWrite On

 
        Pass
        {        Stencil
            {
                Ref[_StencilMask]
                Comp always
                Pass replace
                //ZFail replace
            }
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
        
        // Writes to a single-component texture (TextureFormat.Depth)
        Pass
        {
            Tags
            {
                "RenderType" = "Opaque"
                "Queue" = "Geometry-25"
            }
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
    
            #include "UnityCG.cginc"
    
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
    
            struct v2f
            {
                fixed2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
    
            sampler2D    _MainTex;
    
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
    
            #if !defined(SHADER_API_D3D9) && !defined(SHADER_API_D3D11_9X)
            fixed frag(v2f i) : SV_Depth
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                return 0;
            }
            #else
            void frag(v2f i, out float4 dummycol:COLOR, out float depth : DEPTH)
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                dummycol = col;
    
                depth = 0;
            }
            #endif
            ENDCG
        }
    }
}
