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

    private SortOrderAdjuster _humanAdjuster;
    private SortOrderAdjuster _flyingAdjuster;

    public bool disabled = false;

    private bool _lastHumanMovementActive;
    private bool _lastFlyingMovementActive;
    private bool _lastHumanAttackDisabled;

    // Start is called before the first frame update
    void Start()
    {
        _humanAdjuster = human.GetComponent<SortOrderAdjuster>();
        _flyingAdjuster = flying.GetComponent<SortOrderAdjuster>();

        SwitchTo(startingCharacter);
    }

    // Update is called once per frame
    void Update()
    {
        if (disabled)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchTo(CharacterTypes.Human);
            flyFollow.SetFollow(true);
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

    public void Disable()
    {
        _lastHumanMovementActive = humanMovement.active;
        _lastFlyingMovementActive = flyingMovement.active;
        _lastHumanAttackDisabled = humanAttack.GetDisable();

        disabled = true;

        flyingMovement.active = false;
        humanMovement.active = false;
        humanAttack.SetDisable(true);
    }

    public void Enable()
    {
        disabled = false;

        flyingMovement.active = _lastFlyingMovementActive;
        humanMovement.active = _lastHumanMovementActive;
        humanAttack.SetDisable(_lastHumanAttackDisabled);
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
                _flyingAdjuster.SetSortOrder(1);
                _humanAdjuster.SetSortOrder(2);
                break;
            case CharacterTypes.Flying:
                flyingMovement.active = true;
                humanMovement.active = false;
                humanAttack.SetDisable(true);
                targetTracker.ChangeTarget(flying.transform);
                _flyingAdjuster.SetSortOrder(2);
                _humanAdjuster.SetSortOrder(1);
                flyFollow.SetFollow(false);
                break;
        }
    }
}
