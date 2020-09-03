using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsController : MonoBehaviour
{
    public static SoundEffectsController instance;

    public AudioClip lumberjackDeath;
    public AudioClip lumberjackSpawn;
    public AudioClip lumberjackAttack;
    public AudioClip badSpiritDeath;
    public AudioClip badSpiritSpawn;
    public AudioClip badSpiritAttack;
    public AudioClip thunder;
    public AudioClip fireOn;
    public AudioClip fireOff;
    public AudioClip spiritGrowth;
    public AudioClip shockWave;
    public AudioClip sapTreeHeal;
    public AudioClip defeat;
    public AudioClip victory;

    public AudioSource sfx;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    #region SFX
    public void MakeLumberjackDeathSound()
    {
        MakeSound(lumberjackDeath);
    }

    public void MakeLumberjackSpawnSound()
    {
        MakeSound(lumberjackSpawn);
    }

    public void MakeLumberjackAttackSound()
    {
        MakeSound(lumberjackAttack);
    }

    public void MakeBadSpiritDeathSound()
    {
        MakeSound(badSpiritDeath);
    }
    public void MakeBadSpiritSpawnSound()
    {
        MakeSound(badSpiritSpawn);
    }
    public void MakeBadSpiritAttackSound()
    {
        MakeSound(badSpiritAttack);
    }
    public void MakeThunderSound()
    {
        MakeSound(thunder);
    }
    public void MakeFireOnSound()
    {
        MakeSound(fireOn);
    }
    public void MakeFireOffSound()
    {
        MakeSound(fireOff);
    }
    public void MakeSpiritGrowthSound()
    {
        MakeSound(spiritGrowth);
    }
    public void MakeShockWaveSound()
    {
        MakeSound(shockWave);
    }
    public void MakeSapTreeHealSound()
    {
        MakeSound(sapTreeHeal);
    }
    public void MakeDefeatSound()
    {
        MakeSound(defeat);
    }
    public void MakeVictorySound()
    {
        MakeSound(victory);
    }

    private void MakeSound(AudioClip originalClip)
    {
        sfx.PlayOneShot(originalClip);
    }
    #endregion

}
