using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class CambiarLuzEntorno : MonoBehaviour
{
    private float valorDia;
    private float remapValorDia;
    private Vector3 solArriba;
    [SerializeField] Color SkyColorDia;
    [SerializeField] Color EquatorColorDia;
    [SerializeField] Color GroundColorDia;
    [SerializeField] Color SkyColorNoche;
    [SerializeField] Color EquatorColorNoche;
    [SerializeField] Color GroundColorNoche;
    [SerializeField] Volume volumenDia;
    [SerializeField] Volume volumenNoche;
    [SerializeField] Light luzNoche;
    [SerializeField] Light luzDia;
    [SerializeField] float instensidadLuzDia;
    [SerializeField] float instensidadLuzNoche;
    // Start is called before the first frame update
    void Start()
    {
        solArriba = Vector3.Normalize(Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            valorDia = Vector3.Dot(transform.forward, solArriba);
            remapValorDia = (valorDia + 1)/2;
            LerpColor();
            CambiarLuz();
            CambiarVolumen();
        }
        
    }
    void LerpColor(){
        RenderSettings.ambientSkyColor = Color.Lerp(SkyColorDia, SkyColorNoche, valorDia);
        RenderSettings.ambientEquatorColor = Color.Lerp(EquatorColorDia, EquatorColorNoche, valorDia);
        RenderSettings.ambientGroundColor = Color.Lerp(GroundColorDia, GroundColorNoche, valorDia);
    }
    
    void CambiarLuz(){
        luzNoche.intensity = Mathf.SmoothStep(0, instensidadLuzNoche, valorDia);
        luzDia.intensity = Mathf.SmoothStep(instensidadLuzDia, 0, valorDia);
    }
    void CambiarVolumen(){
        volumenNoche.weight = Mathf.SmoothStep(0, 1, valorDia);
        volumenDia.weight = Mathf.SmoothStep(1, 0, valorDia);
    }
}

