using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSpotlight : MonoBehaviour
{
    public DeathController deathController;

    public Light flyingSpotlight;

    public bool dead = false;
    public bool invincible;

    public float onHitIntensityDecrement;
    public float onHitRadiusDecrement;

    public float secondsBeforeHeal;
    public float healIntensityTime;
    public float healRadiusTime;

    public float deathThreshold = 5;

    private float _startingIntensity;
    private float _startingRadius;

    private float _targetIntensity;
    private float _targetRadius;
    
    private Coroutine _healCountdown;

    // Start is called before the first frame update
    void Start()
    {
        _startingIntensity = flyingSpotlight.intensity;
        _startingRadius = flyingSpotlight.spotAngle;
        _targetIntensity = flyingSpotlight.intensity;
        _targetRadius = flyingSpotlight.spotAngle;
    }

    void FixedUpdate()
    {
        float velocity = 0;
        flyingSpotlight.intensity = Mathf.SmoothDamp(flyingSpotlight.intensity, _targetIntensity, ref velocity, healIntensityTime * Time.smoothDeltaTime);
        flyingSpotlight.spotAngle = Mathf.SmoothDamp(flyingSpotlight.spotAngle, _targetRadius, ref velocity, healRadiusTime * Time.smoothDeltaTime);
    }

    public void Hit()
    {
        if (invincible)
        {
            return;
        }

        if (_healCountdown != null)
        {
            StopCoroutine(_healCountdown);
        }

        flyingSpotlight.intensity -= onHitIntensityDecrement;
        flyingSpotlight.spotAngle -= onHitRadiusDecrement;

        _targetIntensity = flyingSpotlight.intensity;
        _targetRadius = flyingSpotlight.spotAngle;

        if (_targetIntensity <= deathThreshold)
        {
            deathController.Dead();
            return;
        }

        _healCountdown = StartCoroutine(HealCountdown());
    }

    IEnumerator HealCountdown()
    {
        yield return new WaitForSeconds(secondsBeforeHeal);
        _targetIntensity = _startingIntensity;
        _targetRadius = _startingRadius;
    }
}
