using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LevelController : BaseSceneController
{
    // Start is called before the first frame update
    string fileCuento;
    public Text txtBody;
    private float lineasXSec = 1;
    private float factorVel = 1;
    public float palabrasXLinea = 5;
    public float alLinea = 87.6f;
    public float palabrasXminuto = 300;
    public float enenigoVelocidad = 250;
    private Vector3 posVEctor;
    private float bodyHeight, trayectoTotal, trayectoActual;
    private int lineasTotal;
    private bool finJuego = false;
    public Transform finMeta;
    public Transform jugador;
    public Rigidbody2D enemigo;
    [HideInInspector]
    public bool atrapoAJugador = false;
    private float jugadorDistancia , posInicial, enemigoDistancia;
    private Vector2 posJugador;
    private Vector2 enemigoVectotVel;
    const int VELConst = 580;
    public int incVel = 25;
    public Text txtVel;
    public UICanvasCuento uiCanvas;

    protected  override void LocalAwake()
    {
        fileCuento = "Cuentos/PeterPan";
        TextAsset textFile = (TextAsset)Resources.Load(fileCuento, typeof(TextAsset));
        MemoryStream txtSream = GetStream(textFile.text);
        StreamReader reader = new StreamReader(txtSream ,System.Text.Encoding.UTF8);
        posVEctor = new Vector3(0, 0, 0);
        txtBody.text = reader.ReadToEnd();
        enemigoVectotVel = new Vector2();


    }

    public void IncVel(int incDesc)
    {
        if ((palabrasXminuto >= 1500 && incDesc ==1)  ||( palabrasXminuto <= 100 && incDesc == -1)) return;
        palabrasXminuto += incDesc * incVel;
        PlayClick();
    }

    private void Start()
    {
        Canvas.ForceUpdateCanvases();
        lineasTotal = txtBody.cachedTextGenerator.lines.Count;
        bodyHeight = ((RectTransform)txtBody.transform).rect.height;
        trayectoTotal = lineasTotal * alLinea + bodyHeight;
        trayectoActual = 0;
        posJugador = jugador.position;
        posInicial = posJugador.x;
        jugadorDistancia = finMeta.position.x - posJugador.x;
        enemigoDistancia = finMeta.position.x - enemigo.transform.position.x;
    }
    private MemoryStream GetStream(string text)
    {
        MemoryStream stream = new MemoryStream();
        StreamWriter writer = new StreamWriter(stream);
        writer.Write(text);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
        // Update is called once per frame
        void Update()
    {
        if (!finJuego)
        {
            factorVel = palabrasXminuto / (palabrasXLinea * 60);
            posVEctor.y = alLinea;
            float vel = lineasXSec * factorVel *  Time.deltaTime;
            trayectoActual += alLinea * vel;
            txtBody.transform.localPosition += posVEctor * vel;
            Debug.Log(trayectoActual + " de " + trayectoTotal);
            txtVel.text = palabrasXminuto.ToString() + " palabras por minuto";
            if (!atrapoAJugador)
            {
                posJugador.x = posInicial + (trayectoActual / trayectoTotal) * jugadorDistancia;
                jugador.position = posJugador;
                enemigoVectotVel.x = enemigoDistancia / (VELConst / enenigoVelocidad) * 0.01f;
                enemigo.velocity = enemigoVectotVel;
            }
            else
            {
                enemigo.velocity = Vector2.zero;
            }
           

            if (trayectoActual > trayectoTotal)
            {
                finJuego = true;
                enemigo.velocity = Vector2.zero;
                StartCoroutine(ShowFinalPanel());
                Debug.Log(finJuego);
            }
        }

    }

    IEnumerator   ShowFinalPanel()
    {
        yield return new WaitForSeconds(1);
        uiCanvas.ShowPanel(1);
        if (atrapoAJugador)
        {
            uiCanvas.ShowPanel(3);
        }
        else
        {
            uiCanvas.ShowPanel(2);
        }

    }
}
