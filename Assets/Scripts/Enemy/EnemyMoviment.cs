using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMoviment : MonoBehaviour
{
    [SerializeField] private Transform[] pontosDoCaminho;

    private int pontoAtual;
    [SerializeField] private float velocidadeDeMovimento;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MovimentarInimigo()
    {
        transform.position = Vector2.MoveTowards(transform.position, pontosDoCaminho[pontoAtual].position, velocidadeDeMovimento * Time.deltaTime);

        if (transform.position == pontosDoCaminho[pontoAtual].position) 
        {
            pontoAtual += 1;

            if (pontoAtual >= pontosDoCaminho.Length)
            {
                pontoAtual = 0;
            }
        }
        
    }





}
