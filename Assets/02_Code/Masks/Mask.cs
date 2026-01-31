using System.Threading.Tasks;
using UnityEngine;

public abstract class Mask
{

    public abstract void Activate(Player player);
    public abstract void Deactivate(Player player);
    public abstract int GetID();
}
