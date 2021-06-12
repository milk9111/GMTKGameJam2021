using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    public FlyingSpotlight flyingSpotlight;
    public HumanSpotlight humanSpotlight;
    public CharacterSwitch characterSwitch;

    public bool dead;

    public void Dead()
    {
        dead = true;
        characterSwitch.disabled = true;
        flyingSpotlight.gameObject.SetActive(false);
        humanSpotlight.gameObject.SetActive(false);
    }
}
