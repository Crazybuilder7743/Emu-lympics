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
    public override void Activate(Player player)
    {
        float overchargeAmount = Time.time - lastUseTime;
        if(overchargeAmount >= timeTillOverChargeSec) 
        { 
            lastUseTime = Time.time;
        }

        overcharging = true;
        Overheat(player);
    }

    private async void Overheat(Player player) 
    {
        player.laserVFX.gameObject?.SetActive(true);
        player.laserVFX?.Play();
        while (overcharging) 
        {
            float overchargeAmount = Time.time - lastUseTime;
            if (overchargeAmount >= timeTillOverChargeSec) 
            {
                player.healthSystem.TakeDamage(damagePerS * Time.deltaTime);
            }
            player.laserVFX.SetFloat("OverchargeAmount",Mathf.Clamp01( overchargeAmount/timeTillOverChargeSec));

            await Task.Delay(INTERGRATION_STEPS_MS);
        }
    
    }

    public override void Deactivate(Player player)
    {
        overcharging = false;
        player.laserVFX.gameObject?.SetActive(false);
        player.laserVFX?.Stop();  
    }



    public override int GetID()
    {
        return ID;
    }
}
