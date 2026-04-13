Shader "Custom/VerySmallShader"
{
  SubShader
  {
    Pass
    {
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag

      #include "UnityCG.cginc"
        Properties
      {
        _Tex ("Tex", 2D) = "" {}
      }
      //CGINCLUDE

      
      // \‘¢‘Ì‚Ì’è‹`
      struct appdata // vertŠÖ”‚Ì“ü—Í
      {
        float4 vertex : POSITION;
        float2 texcoord : TEXCOORD0;
      };
        
      struct fin // vertŠÖ”‚Ìo—Í‚©‚çfragŠÖ”‚Ì“ü—Í‚Ö
      {
        float4 vertex : SV_POSITION;
        float2 texcoord : TEXCOORD0;
      };

      fin vert(appdata v )
      {
        fin o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.texcoord = v.texcoord;
        return o;
      }

      float4 frag(fin vertex ) : SV_TARGET
      {
        return float4(1, 0, 0, 1);
      }
      

      ENDCG
    }
  }
}
