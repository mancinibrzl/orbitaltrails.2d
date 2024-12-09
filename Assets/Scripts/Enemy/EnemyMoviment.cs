using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMoviment : MonoBehaviour
{
    [Header("Caminho do Inimigo")]
    [SerializeField] private Transform[] pontosDoCaminho;
    private int pontoAtual;

    [Header("Movimento do Inimigo")]
    [SerializeField] private float velocidadeDeMovimento;
    private float ultimaPosicaoX;

    private void Start()
    {
        pontoAtual = 0;
        transform.position = pontosDoCaminho[pontoAtual].position;
    }

    void Update()
    {
        MovimentarInimigo();
        EspelharNaHorizontal();
    }

    private void MovimentarInimigo()
    {
        transform.position = Vector2.MoveTowards(transform.position, pontosDoCaminho[pontoAtual].position, velocidadeDeMovimento * Time.deltaTime);

        if (transform.position == pontosDoCaminho[pontoAtual].position) 
        {
            pontoAtual += 1;

            ultimaPosicaoX = transform.localPosition.x;

            if (pontoAtual >= pontosDoCaminho.Length)
            {
                pontoAtual = 0;
            }
        }
        
    }

    private void EspelharNaHorizontal()
    {
        if (transform.localPosition.x < ultimaPosicaoX)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (transform.localPosition.x > ultimaPosicaoX)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }




}
