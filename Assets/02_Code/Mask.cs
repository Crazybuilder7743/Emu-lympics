using System.Threading.Tasks;
using UnityEngine;

public abstract class Mask
{
    private const int ITERATION_RATE_MS = 100;
    public float maxEnergie;
    public float activationEnergieCost;
    public float currentEnergie;
    public float rechargeRateperS;
    public bool needToRecharge;
    private bool charging;
    public virtual void Activate()
    {
        if (needToRecharge)
        {
            return;
        }
        currentEnergie -= activationEnergieCost;
        needToRecharge = currentEnergie <= 0;
        currentEnergie = needToRecharge ? 0 : currentEnergie;
        if (!charging) 
        {
            Charge();
        }
    }

    public virtual async void Charge()
    {
        charging = true;
        while (currentEnergie < maxEnergie)
        {
            currentEnergie += rechargeRateperS * (ITERATION_RATE_MS / 1000);
            currentEnergie = currentEnergie > maxEnergie ? maxEnergie : currentEnergie;
            await Task.Delay(ITERATION_RATE_MS);
        }
        charging = false;
    }

    public abstract int GetID();
}
