Shader "Unlit/StencilDepth"
{
    Properties
    {
        [IntRange] _StencilID ("Stencil ID", Range(0,255)) = 0
    }
    SubShader
    {
        Tags 
        { 
            "RenderType"="Opaque" 
            "RenderPipeline" = "UniversalPipeline"
            "Queue" = "Geometry-1"
        }
        ColorMask 0
        Zwrite Off
        LOD 100

        Pass
        {
            
            Stencil{
                Ref [_StencilID]
                Comp Always
                Pass Replace
                Fail Replace
                ZFail Replace
            }
        }
    }
}

