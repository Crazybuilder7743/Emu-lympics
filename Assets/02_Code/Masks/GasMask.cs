using UnityEngine;

public class GasMask : Mask
{
    private const string GAS_LAYER_NAME = "Gas";
    private const string DEFAULT_LAYER_NAME = "Default";
    public static int ID = 3;
    public override void Activate(Player player)
    {
        player.gameObject.layer = LayerMask.NameToLayer(GAS_LAYER_NAME);
    }

    public override void Deactivate(Player player)
    {
        player.gameObject.layer = LayerMask.NameToLayer(DEFAULT_LAYER_NAME);
    }

    public override int GetID()
    {
        return ID;  
    }
}
