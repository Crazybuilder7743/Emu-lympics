using UnityEngine;

public class GasMask : Mask
{
    private const string GAS_LAYER_NAME = "Gas";
    private const string DEFAULT_LAYER_NAME = "Default";
    public static int ID = 3;
    public override void Activate(Player player)
    {
        player.healthSystem.immuneToGas = true;
    }

    public override void Deactivate(Player player)
    {
        player.healthSystem.immuneToGas = false;
    }

    public override int GetID()
    {
        return ID;  
    }
}
