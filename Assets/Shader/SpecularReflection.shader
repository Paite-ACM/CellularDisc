Shader "Lewis/SpecularReflection"
{
    Properties
    {
        // mode "white"
        _MainTex ("Texture", 2D) = "white" {}
        // mode "black"
        _SpecularTex ("Specular Texture", 2D) = "Black" {}
        _SpecularInt ("Specular Intensity", Range(0, 1)) = 1
        _SpecularPow ("Specular Power", Range(1, 128)) = 64

        // toonshading port
        _Color ("Colour", Color) = (1, 1, 1, 1)
        _LightInt ("Light Intensity", Range(0, 1)) = 1
        _LightThreshold ("Light Threshold", float) = 1
    }
    SubShader
    {
        Tags 
        { 
            "RenderType"="Opaque" 
            "LightMode"="ForwardBase"
        }
        LOD 100

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"

            struct meshdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal_world : TEXCOORD1;
                float3 vertex_world : TEXCOORD2;
                float3 normal : NORMAL;
            };
            
           

            // connection variables
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _SpecularTex;
            float _SpecularInt;
            float _SpecularPow;
            float4 _Color;
            float _LightInt;
            float _LightThreshold;

             // specular
            float3 SpecularShading(float3 colourRefl, float specularInt, float3 normal, float3 lightDir, float3 viewDir, float specularPow)
            {
                // halfway
                float3 h = normalize(lightDir + viewDir);

                return colourRefl * specularInt * pow(max(0, dot(normal, h)), specularPow);
            }

            // apply toon shading
            float ToonShading(float dotProduct)
            {
                float threshold = dotProduct;
                
                if (threshold > _LightThreshold)
                {
                    threshold = _LightThreshold + 0.1;
                }
                return threshold;

            }

            float3 LambertShading(float3 colorRefl, float lightInt, float3 normal, float3 lightDir)
            {
                float product = dot(normal, lightDir);
                float maxedProduct = max(0, product);

                // apply toon shading
                float toonProduct = ToonShading(maxedProduct);

                float3 result = colorRefl * lightInt * toonProduct;


                return result;
            }

            v2f vert (meshdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal_world = UnityObjectToWorldNormal(v.normal);
                o.vertex_world = mul(unity_ObjectToWorld, v.vertex);
                o.normal = v.normal;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                // calculating light direction
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.vertex_world);
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                // implement normals in world space
                float3 normal = i.normal_world;
                // reflection colour
                float3 colourRefl = _Color.rgb;
                float3 specCol = tex2D(_SpecularTex, i.uv) * colourRefl;

                // final calcluation for specular
                float3 specular = SpecularShading(specCol, _SpecularInt, normal, lightDir, viewDir, _SpecularPow);

                // lambert shading
                float3 colorRefl = _Color.rgb;
                float3 lightSource = normalize(_WorldSpaceLightPos0.xyz);
                float3 diffuse = LambertShading(colorRefl, _LightInt, i.normal, lightSource);
                diffuse = floor(diffuse * _LightThreshold) / _LightThreshold;
                // add specularity to the texture
                col.rgb += specular;
                col.rgb *= diffuse;
                return col;
            }
            ENDHLSL
        }
    }
}
