Shader "CellularDisc/InvertedNormalMapEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NormalMap("Normal Map", 2D) = "white" {}
        
        _MainColour("Main Colour", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
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
                float2 NormalUV = float2(input.uv.x + _Time.x * 2, input.uv.y + _Time.y * 2);
                float2 NormalTex = tex2D(_NormalMap, input.uv/ NormalUV).xy * 2;
                
                

                fixed4 col = tex2D(_MainTex, input.uv + NormalTex);   
                col *= _MainColour;

               // NormalTex.rgb = 1 - col.rgb; 

                return col;
            }
            ENDCG
        }
    }
}
