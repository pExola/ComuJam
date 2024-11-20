using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Pode de Gelo")]
public class FeiticoDeGelo : Item
{
    public float Poder;
    public ParticleSystem particula;
    public Transform player;
    
    public override void usarItem()
    {
        var posPlayer = PlayerController.PlayerPos;
        ParticleSystem effect = GameObject.Instantiate(particula,posPlayer.position
            ,posPlayer.rotation,posPlayer);
        effect.Play();
        GameObject.Destroy(effect.gameObject, effect.main.duration); 
        Debug.Log($"{itemName}  usado! poder de {Poder}");
    }


}
