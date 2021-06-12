using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterTypes
{
    Human,
    Flying
}

public class CharacterSwitch : MonoBehaviour
{
    public CharacterTypes startingCharacter;

    public CharacterTypes currentCharacter;

    public GameObject human;
    public GameObject flying;

    public HumanMovement humanMovement;
    public FlyingMovement flyingMovement;
    public FlyFollow flyFollow;
    public HumanAttack humanAttack;

    public TargetTracker targetTracker;

    private SpriteRenderer _humanRenderer;
    private SpriteRenderer _flyingRenderer;

    public bool disabled = false;

    // Start is called before the first frame update
    void Start()
    {
        _humanRenderer = human.GetComponent<SpriteRenderer>();
        _flyingRenderer = flying.GetComponent<SpriteRenderer>();

        SwitchTo(startingCharacter);
    }

    // Update is called once per frame
    void Update()
    {
        if (disabled)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            flyFollow.SetFollow(true);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentCharacter == CharacterTypes.Flying)
            {
                SwitchTo(CharacterTypes.Human);
            } else
            {
                SwitchTo(CharacterTypes.Flying);
            }
        }
    }

    private void SwitchTo(CharacterTypes characterType)
    {
        currentCharacter = characterType;
        switch(characterType)
        {
            case CharacterTypes.Human:
                flyingMovement.active = false;
                humanMovement.active = true;
                humanAttack.SetDisable(false);
                targetTracker.ChangeTarget(human.transform);
                _flyingRenderer.sortingOrder = 1;
                _humanRenderer.sortingOrder = 2;
                break;
            case CharacterTypes.Flying:
                flyingMovement.active = true;
                humanMovement.active = false;
                humanAttack.SetDisable(true);
                targetTracker.ChangeTarget(flying.transform);
                _flyingRenderer.sortingOrder = 2;
                _humanRenderer.sortingOrder = 1;
                flyFollow.SetFollow(false);
                break;
        }
    }
}
