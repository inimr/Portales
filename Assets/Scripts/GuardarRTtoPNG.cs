using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardarRTtoPNG : MonoBehaviour
{
    public RenderTexture rt;

    // Use this for initialization
    public void Awake () {
        byte[] bytes = toTexture2D(rt).EncodeToPNG();
        System.IO.File.WriteAllBytes("C:/Users/zsanchez/Desktop/Proyecto Portal/Unity/Assets/Materials&Textures/Hierba/Captura.png", bytes);
    }
    Texture2D toTexture2D(RenderTexture rTex){
        Texture2D tex = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
}
