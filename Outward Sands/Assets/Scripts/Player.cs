using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [SerializeField]
    public Image VidaBarra;
    public Image VidaVermelhaBarra;
    public Rigidbody _corpoPlayer;
    public float _Velocidade = 10f;
    public int VidaMax = 11;
    public int VidaAtual;
    public Vector3 VidaEscala;


    // Start is called before the first frame update
    void Start()
    {
        _corpoPlayer = GetComponent<Rigidbody>();
        VidaAtual = VidaMax;
    }

    // Update is called once per frame
    void Update()
    {
        float EsquerdaDireita = Input.GetAxis("Horizontal");
        float FrenteTras = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * EsquerdaDireita * _Velocidade * Time.deltaTime);
        transform.Translate(Vector3.forward * FrenteTras * _Velocidade * Time.deltaTime);
    }


    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {

            Debug.Log("Voc� perdeu 1 de vida");
            VidaAtual--;
        }

        VidaEscala = VidaBarra.rectTransform.localScale;
        VidaEscala.x = (float)VidaAtual / VidaMax;
        VidaBarra.rectTransform.localScale = VidaEscala;

        if (VidaEscala.x <= 0)
        {
            Destroy(gameObject);
        }
    }
}