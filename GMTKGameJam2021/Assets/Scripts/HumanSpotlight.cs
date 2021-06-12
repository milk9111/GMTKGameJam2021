using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpotlight : MonoBehaviour
{
    public DeathController deathController;

    public Light humanSpotlight;
    public Light flyingSpotlight;

    public float distanceBeforeFade;

    public float deathThreshold = 1f;

    public float fadeRadiusTime;
    public float fadeIntensityTime;

    public float restoreRadiusTime;
    public float restoreIntensityTime;

    private float _startingIntensity;
    private float _startingRadius;

    // Start is called before the first frame update
    void Start()
    {
        _startingIntensity = humanSpotlight.intensity;
        _startingRadius = humanSpotlight.spotAngle;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, flyingSpotlight.transform.position) > distanceBeforeFade)
        {
            float velocity = 0;
            humanSpotlight.intensity = Mathf.SmoothDamp(humanSpotlight.intensity, 0, ref velocity, fadeIntensityTime * Time.smoothDeltaTime);
            humanSpotlight.spotAngle = Mathf.SmoothDamp(humanSpotlight.spotAngle, 0, ref velocity, fadeRadiusTime * Time.smoothDeltaTime);

            if (humanSpotlight.intensity <= deathThreshold)
            {
                deathController.Dead();
            }
        } else
        {
            float velocity = 0;
            humanSpotlight.intensity = Mathf.SmoothDamp(humanSpotlight.intensity, _startingIntensity, ref velocity, restoreIntensityTime * Time.smoothDeltaTime);
            humanSpotlight.spotAngle = Mathf.SmoothDamp(humanSpotlight.spotAngle, _startingRadius, ref velocity, restoreRadiusTime * Time.smoothDeltaTime);
        }
    }
}
