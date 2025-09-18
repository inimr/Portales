Shader "Custom/StencilSetValue"
{
    Properties
    {
        [IntRange] _StencilMask ("Stencil ID", Range(0,10)) = 0
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "IgnoreProjector" = "True"
            "UniversalMaterialType" = "Unlit"
            "Queue" = "Geometry-50"
            "RenderPipeline" = "UniversalPipeline"
        }
        Stencil
        {
            Ref[_StencilMask]
            Comp always
            Pass replace
            ZFailFront keep
            ZFailBack keep
        }
        // Don't draw in the RGBA channels; just the depth buffer
        ColorMask 0
        ZWrite On
        ZTest LEqual
        // Do nothing specific in the pass:
        Pass {}

    }
}
