using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsBall : MonoBehaviour
{
    public AudioSource src;
    public AudioClip _ballHit, _ballHitGoal, _ballHitPowerup, _ballHitWall;

    public void PlayBallHitSFX()
    {
        src.clip = _ballHit;
        src.Play();
    }
    public void PlayBallHitGoalSFX()
    {
        src.clip = _ballHitGoal;
        src.Play();
    }

    public void PlayPowerUpHitSFX()
    {
        src.clip = _ballHitPowerup;
        src.Play();
    }
    public void PlayHitWallSDX()
    {
        src.clip = _ballHitWall;
        src.Play();
    }
    public void PLayPowerDownHitSFX()
    {

    }
}
