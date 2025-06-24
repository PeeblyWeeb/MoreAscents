namespace MoreAscents;

public class AscentGimmick
{
    public virtual string GetTitle()
    {
        return "";
    }

    public virtual string GetDescription()
    {
        return "";
    }

    public AscentData.AscentInstanceData _ascentData;
    public bool active;
}