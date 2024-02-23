using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : ScriptableObject
{

    public int numFlowers;
    public int numBushes;

    public int bushesSmelled;
    public int flowersSmelled;

    public int getNumFlowers()
    {
        return numFlowers;
    }

    public int getNumBushes()
    {
        return numBushes;
    }

    public void updateNumFlowers(int numFlowers)
    {
        this.numFlowers = numFlowers;
    }

    public void updateNumBushes(int numBushes)
    {
        this.numBushes = numBushes;
    }

    public void updateFlowersSmelled(int flowersSmelled)
    {
        this.flowersSmelled = flowersSmelled;
    }

    public void updateBushesSmelled(int bushesSmelled)
    {
        this.bushesSmelled = bushesSmelled;
    }

    public int getSmellScore() {
        return flowersSmelled - bushesSmelled;
    }
}
