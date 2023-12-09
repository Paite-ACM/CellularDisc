Shader "CellularDisc/AnimatedNormalMapWithGaussianBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NormalMap("Normal Map", 2D) = "white" {}
        
        _MainColour("Main Colour", Color) = (1,1,1,1)
        _NormalColour("Normal Colour", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            

            #include "UnityCG.cginc"

            struct meshdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;               
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainColour;
            sampler2D _NormalMap;
            float4 _NormalColour;

            v2f vert (meshdata v)
            {
                v2f output;
                output.vertex = UnityObjectToClipPos(v.vertex);
                output.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return output;
            }

            fixed4 frag (v2f input) : SV_Target
            {
                // sample the texture
                float2 uv = input.uv;
                float NormalTexx = sin(uv.x + _Time.x * 2);
                float NormalTexy = cos(uv.y + _Time.y * 2);
                
                float2 NormalUV = float2(NormalTexx, NormalTexy);
                float2 NormalTex = tex2D(_NormalMap, input.uv * NormalUV).xy * 2;
                
                

                
                // fixed4 col = tex2D(_MainTex, input.uv + NormalTex);   
                

                NormalTex *=_MainColour;

                float blur = 0.05;

                float4 sum = float4(0,0,0,0);
                sum += tex2D(_MainTex, float2(uv.x -5 * blur, uv.y -5 * blur)* 0.05 + NormalTex);
                sum += tex2D(_MainTex, float2(uv.x -4 * blur, uv.y -4 * blur)* 0.05 + NormalTex);
                sum += tex2D(_MainTex, float2(uv.x -3* blur, uv.y -3 * blur)* 0.09 + NormalTex);
                sum += tex2D(_MainTex, float2(uv.x -2* blur, uv.y -2 * blur)* 0.01 + NormalTex);
                sum += tex2D(_MainTex, float2(uv.x -1* blur, uv.y -1 * blur)* 0.01 + NormalTex);
                sum += tex2D(_MainTex, uv)* 0.5;
                sum += tex2D(_MainTex, float2(uv.x +7 * blur, uv.y + 7 * blur)* 0.05 + NormalTex);
                sum += tex2D(_MainTex, float2(uv.x +6 * blur, uv.y + 6 * blur)* 0.05 + NormalTex);
                sum += tex2D(_MainTex, float2(uv.x +5 * blur, uv.y + 5 * blur)* 0.05 + NormalTex);
                sum += tex2D(_MainTex, float2(uv.x +4 * blur, uv.y + 4 * blur)* 0.05 + NormalTex);
                sum += tex2D(_MainTex, float2(uv.x +3* blur, uv.y +4 * blur)* 0.05 + NormalTex);
                sum += tex2D(_MainTex, float2(uv.x +2* blur, uv.y +2 * blur)* 0.05 + NormalTex);
                sum += tex2D(_MainTex, float2(uv.x +1 * blur, uv.y + 1 * blur)* 0.05 + NormalTex);

            
                
                 

                    //return col;   
                    
                    return sum;                 
            }
            ENDHLSL
        }

        Pass
        {
            Name "DepthOnly"
            Tags{"LightMode" = "DepthOnly"}

            ZWrite On
            ColorMask 0
            Cull[_Cull]

            HLSLPROGRAM
            #pragma exclude_renderers gles gles3 glcore
            #pragma target 4.5

            #pragma vertex DepthOnlyVertex
            #pragma fragment DepthOnlyFragment

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            #pragma multi_compile _ DOTS_INSTANCING_ON

            #include "Packages/com.unity.render-pipelines.universal/Shaders/LitInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/DepthOnlyPass.hlsl"
            ENDHLSL
        } 

        Pass
        {
            Name "DepthNormals"
            Tags{"LightMode" = "DepthNormals"}

            ZWrite On
            Cull[_Cull]

            HLSLPROGRAM
            #pragma exclude_renderers gles gles3 glcore
            #pragma target 4.5

            #pragma vertex DepthNormalsVertex
            #pragma fragment DepthNormalsFragment

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local _NORMALMAP
            #pragma shader_feature_local _PARALLAXMAP
            #pragma shader_feature_local _ _DETAIL_MULX2 _DETAIL_SCALED
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            #pragma multi_compile _ DOTS_INSTANCING_ON

            #include "Packages/com.unity.render-pipelines.universal/Shaders/LitInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/LitDepthNormalsPass.hlsl"
            ENDHLSL
        }
    }
}
