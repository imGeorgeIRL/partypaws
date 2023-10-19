Shader"Custom/TileHighlight"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1, 1, 1, 1)
        _HighlightIntensity("Highlight Intensity", Range(0, 1)) = 0.5
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
half _HighlightIntensity; // Intensity of the highlight

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
    half4 highlight = texColor * _Color;
    highlight.rgb += (_HighlightIntensity - 0.5) * 2; // Modify the intensity
    return lerp(texColor, highlight, texColor.a);
}
            ENDCG
        }
    }
}
