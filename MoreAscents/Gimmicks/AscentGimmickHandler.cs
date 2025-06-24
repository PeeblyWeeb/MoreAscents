using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace MoreAscents;

public class AscentGimmickHandler
{
    public static List<AscentGimmick> gimmicks = new();

    public static void MarkGimmickAsActive(AscentData.AscentInstanceData instanceData)
    {
        foreach (AscentGimmick gimmick in gimmicks)
        {
            gimmick.active = gimmick._ascentData == instanceData;
            if (gimmick.active)
            {
                Plugin.Logger.LogInfo($"marked gimmick {gimmick.GetType().Name} as active");
            }
        }
    }

    public static AscentGimmick GetGimmick<T>() where T : AscentGimmick
    {
        foreach (AscentGimmick gimmick in gimmicks)
        {
            if (gimmick.GetType() == typeof(T))
                return gimmick;
        }
        return null;
    }
    
    public static bool IsGimmickActive<T>() where T : AscentGimmick
    {
        foreach (AscentGimmick gimmick in gimmicks)
        {
            if (gimmick.GetType() == typeof(T))
                return gimmick.active;
        }
        return false;
    }
    
    public static void RegisterAscent<T>(List<AscentData.AscentInstanceData> ascents) where T : AscentGimmick
    {
        AscentGimmick gimmick = Activator.CreateInstance<T>();
        
        AscentData.AscentInstanceData newData = new();
        newData.title = gimmick.GetTitle();
        newData.description = gimmick.GetDescription();
        newData.titleReward = "A pat on the back";
        ascents.Add(newData);

        gimmick._ascentData = newData;
        gimmicks.Add(gimmick);
    }
}