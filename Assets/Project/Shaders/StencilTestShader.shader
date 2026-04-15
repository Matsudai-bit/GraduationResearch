Shader "Custom/StencilTestShader"
{
    Properties
  {
    _Tex ("Tex", 2D) = "white" {}
    // 3 は Equal（マスク部分のみ）、8 は Always（常に表示）を意味します
        [Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp ("Stencil Test", Float) = 3

  }
CGINCLUDE

// float4 paint(float2 uv)
// {
//     return ;
// }

ENDCG
  SubShader
  {
   
    Pass
    {
        ZTest Always    // 巨大な球体でも、手前のオブジェクトを上書きしないようにする
        Stencil {
		Ref 1
        Comp [_StencilComp]            
		}
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag

      #include "UnityCG.cginc"

      // 構造体の定義
      struct appdata // vert関数の入力
      {
        float4 vertex : POSITION;
        float2 texcoord : TEXCOORD0;
      };
        
      struct fin // vert関数の出力からfrag関数の入力へ
      {
        float4 vertex : SV_POSITION;
        float2 texcoord : TEXCOORD0;
      };

      sampler2D _Tex;

      // float4 vert(float4 vertex : POSITION) : SV_POSITION から↓に変更
      fin vert(appdata v) // 構造体を使用した入出力
      {
        fin o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.texcoord = v.texcoord;
        return o;
      }

      float4 frag(fin IN) : SV_TARGET // 構造体finを使用した入力
      {
          float4 textureColor = tex2D(_Tex, IN.texcoord);
          return textureColor;
      }
      ENDCG
    }
  }
}