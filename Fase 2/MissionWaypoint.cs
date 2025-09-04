    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionWaypoint : MonoBehaviour
{
    // ícone do indicador
    public Image img;
    // O alvo (localização, inimigo, etc.)
    public Transform target;
    // Texto da IU para mostrar a distância
    public Text meter;
    // Para ajustar a posição do ícone
    public Vector3 offset;
    // Para ajustar a distancia do alvo
    public int ajeitarDistancia;

    private void Update()
    {
        
        // Limitando o ícone para que ele fique na tela
        // Abaixo cálculos com a suposição de que o ponto de ancoragem do ícone está no meio
        // Posição X mínima: metade da largura do ícone
        float minX = img.GetPixelAdjustedRect().width / 2;
        // Maximum X position: screen width - half of the icon width
        float maxX = Screen.width - minX;

        // Posição Y mínima: metade da altura
        float minY = img.GetPixelAdjustedRect().height / 2;
        // Posição Y máxima: altura da tela - metade da altura do ícone
        float maxY = Screen.height - minY;

        // Variável temporária para armazenar a posição convertida de ponto mundial 3D para ponto de tela 2D
        Vector2 pos = Camera.main.WorldToScreenPoint(target.position + offset);

        // Verifique se o alvo está atrás de nós, para mostrar o ícone apenas quando o alvo estiver na frente
        if (Vector3.Dot((target.position - transform.position), transform.forward) < 0)
        {
            // Verifique se o alvo está no lado esquerdo da tela
            if (pos.x < Screen.width / 2)
            {
                // Coloque-o à direita (já que está atrás do jogador, é o oposto)
                pos.x = maxX;
            }
            else
            {
                // Coloque-o no lado esquerdo
                pos.x = minX;
            }
        }

        // Limita as posições X e Y
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        // Atualiza a posição do marcador
        img.transform.position = pos;
        // Altere o texto do medidor para a distância com a unidade de medidor 'm'
        meter.text = ((int)Vector3.Distance(target.position, transform.position) - ajeitarDistancia).ToString() + "m";
    }
}