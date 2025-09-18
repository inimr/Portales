struct appdata
            {
                float4 positionOS   : POSITION;
                float4 tangentOS: TANGENT;
                float3 normalOS : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct vertOut
            {
                float4 positionCS  : SV_POSITION;
                float2 waterUV:TEXCOORD0;
                float4 screenPosition: TEXCOORD1;
                float3 normalWS: NORMAL;
                float3 tangentWS : TEXCOORD3;
                float3 biTangent : TEXCOORD2;
                float3 positionWS: TEXCOORD4;
                float2 foamUV:TEXCOORD5;
                float2 UV:TEXCOORD6;
                half4 R: TEXCOORD7;
            };

 
            sampler2D _CameraOpaqueTexture;
            CBUFFER_START(UnityPerMaterial)
                
                sampler2D _MainTex;
                float _TextureBlend;
                float4 _MainTex_ST;
                half4 _BaseColor;
                half4 _FoamColor;
                half4 _SurfaceColor;
                half4 _BottomColor;
                float _Depth;
                float _WaveSpeed;
                float _WaveScale;
                float _WaveStrength;
                float _FoamAmount;
                float _FoamCutoff;
                float _FoamSpeed;
                float _FoamScale;
                float _SpecularIntensity;
                float _Gloss;
                float _Smoothness;
                float _NoiseNormalStrength;
                float _WaterShadow;
                float _TransparenciaMultiply;
                float _TransparenciaAdd;
                float _Bias;
                float _Scale;
                float _Power;
            CBUFFER_END

            vertOut vert(appdata v)
            {
                vertOut o;

                _MainTex_ST.zw += _Time.y*_WaveSpeed;
                _MainTex_ST.xy *= _WaveScale;
                o.waterUV = TRANSFORM_TEX(v.uv, _MainTex);
                _MainTex_ST.zw += _Time.y*_FoamSpeed;
                _MainTex_ST.xy *= _FoamScale;
                o.foamUV = TRANSFORM_TEX(v.uv, _MainTex);

                o.UV = v.uv;

                float waterGradientNoise;
                Unity_GradientNoise_float(o.waterUV, 1, waterGradientNoise);
                v.positionOS.y += _WaveStrength*(2*waterGradientNoise-1);

                o.positionWS = GetVertexPositionInputs(v.positionOS.xyz).positionWS;
                o.normalWS = GetVertexNormalInputs(v.normalOS).normalWS;//conver OS normal to WS normal
                o.tangentWS = GetVertexNormalInputs(v.normalOS,v.tangentOS).tangentWS;
                o.biTangent = cross(o.normalWS, o.tangentWS)
                              * (v.tangentOS.w) 
                              * (unity_WorldTransformParams.w);

                o.positionCS = TransformObjectToHClip(v.positionOS.xyz);
                o.screenPosition = ComputeScreenPos(o.positionCS);

            
            
                float3 I = normalize(o.positionWS - _WorldSpaceCameraPos.xyz);
                o.R = _Bias + _Scale * pow(1.0 + dot(I, o.normalWS), _Power);

                return o;
            }

            float DepthFade (float rawDepth,float strength, float4 screenPosition){
                float sceneEyeDepth = LinearEyeDepth(rawDepth, _ZBufferParams);
                float depthFade = sceneEyeDepth;
                depthFade -= screenPosition.a;
                depthFade /= strength;
                depthFade = saturate(depthFade);
                return depthFade;
            }

            //float3 _LightDirection;
            half4 frag(vertOut i, FRONT_FACE_TYPE frontFace : FRONT_FACE_SEMANTIC) : SV_Target//get front face of object
            {
                
                
                float2 screenSpaceUV = i.screenPosition.xy/i.screenPosition.w;
                
                float rawDepth = SampleSceneDepth(screenSpaceUV);
                float depthFade = DepthFade(rawDepth,_Depth, i.screenPosition);
                float4 waterDepthCol = lerp(_BottomColor,_SurfaceColor,1-depthFade);



                float waterGradientNoise;
                Unity_GradientNoise_float(i.waterUV, 1, waterGradientNoise);

                float3 gradientNoiseNormal;
                float3x3 tangentMatrix = float3x3(i.tangentWS, i.biTangent,i.normalWS);
                Unity_NormalFromHeight_Tangent_float(waterGradientNoise, 0.1,i.positionWS,tangentMatrix,gradientNoiseNormal);
                gradientNoiseNormal *= _NoiseNormalStrength;

                gradientNoiseNormal += i.screenPosition.xyz ;
                float4 gradientNoiseScreenPos = float4(gradientNoiseNormal,i.screenPosition.w );
                float4 waterDistortionCol = tex2Dproj(_CameraOpaqueTexture,gradientNoiseScreenPos);
                float4 lerpColorSolido = lerp(waterDistortionCol, waterDepthCol, depthFade);

            //return lerpColorSolido;
            //return float4(i.R.xxx, 1);
                //float NdotV = dot(mul((float3x3)unity_CameraToWorld, float3(0,0,1)), gradientNoiseNormal);
                //return saturate(1-NdotV);

                float foamDepthFade = DepthFade(rawDepth,_FoamAmount, i.screenPosition);
                foamDepthFade *= _FoamCutoff;

                float foamGradientNoise;
                Unity_GradientNoise_float(i.foamUV, 1, foamGradientNoise);

                float foamCutoff = step(foamDepthFade, foamGradientNoise);
                foamCutoff *= _FoamColor.a;

                float4 foamColor = lerp(waterDepthCol, _FoamColor, foamCutoff);


                //float4 mainTex = tex2D(_MainTex,i.waterUV);
                float4 finalCol = lerp(lerpColorSolido, foamColor, foamColor.a);
                //finalCol = lerp(mainTex,finalCol,_TextureBlend);

                float fadeHorizonte = saturate(1-length(i.UV * 2 - 1)* _TransparenciaMultiply + _TransparenciaAdd);
                // return float4(fadeHorizonte,fadeHorizonte,fadeHorizonte,fadeHorizonte);

                float3 gradientNoiseNormalWS;
                Unity_NormalFromHeight_World_float(waterGradientNoise,0.1,i.positionWS,tangentMatrix,gradientNoiseNormalWS);
                
                finalCol = float4(finalCol.rgb, saturate(finalCol.a  + i.R.x)* fadeHorizonte );
                //finalCol = float4(i.R.xxx, saturate(finalCol.a  + i.R.x)* fadeHorizonte );
                return finalCol;
            }