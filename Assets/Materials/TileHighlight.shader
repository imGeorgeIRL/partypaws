Shader"Custom/TileHighlight"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1, 1, 1, 1)
        _OutlineWidth("Outline Width", Range(0, 0.1)) = 0.01 // Adjust the outline width
        _OutlineColor("Outline Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags { "Queue" = "Transparent" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
#include "UnityCG.cginc"

struct appdata_t
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
float4 _Color;
half _OutlineWidth;
float4 _OutlineColor;

v2f vert(appdata_t v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = v.uv;
    return o;
}

half4 frag(v2f i) : SV_Target
{
    half4 texColor = tex2D(_MainTex, i.uv);
    half4 outline = texColor;
    half4 baseColor = texColor * _Color;

                // Add the outline effect
    half outlineMask = 1.0 - step(_OutlineWidth, texColor.a);
    outline.rgb = _OutlineColor.rgb;
    outline.a = outlineMask;

    return lerp(baseColor, outline, outline.a);
}
            ENDCG
        }
    }
}
