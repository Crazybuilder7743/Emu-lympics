using System.Threading.Tasks;
using UnityEngine;

public class SupermanMask : Mask
{
    private const int INTERGRATION_STEPS_MS = 50;
    public static int ID = 5;
    private float lastUseTime = float.MinValue;
    private float timeTillOverChargeSec = 5f;
    private float damagePerS = 2f;
    private bool overcharging;
    private Player ownPlayer;
    public override void Activate(Player player)
    {
        float overchargeAmount = Time.time - lastUseTime;
        if(overchargeAmount >= timeTillOverChargeSec) 
        { 
            lastUseTime = Time.time;
        }

        ownPlayer = player;
        overcharging = true;
        Overheat();
    }

    private async void Overheat() 
    {
        ownPlayer.laserVFX.gameObject?.SetActive(true);
        ownPlayer.laserVFX?.Play();
        while (overcharging) 
        {
            float overchargeAmount = Time.time - lastUseTime;
            if (overchargeAmount >= timeTillOverChargeSec) 
            {
                ownPlayer.healthSystem.TakeDamage(damagePerS * Time.deltaTime);
            }
            ownPlayer.laserVFX.SetFloat("OverchargeAmount",Mathf.Clamp01( overchargeAmount/timeTillOverChargeSec));

            await Task.Delay(INTERGRATION_STEPS_MS);
        }
    
    }

    public override void Deactivate(Player player)
    {
        overcharging = false;
        ownPlayer.laserVFX.gameObject?.SetActive(false);
        ownPlayer.laserVFX?.Stop();  
    }



    public override int GetID()
    {
        return ID;
    }
}
