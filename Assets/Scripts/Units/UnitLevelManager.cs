using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit Config")]
public class UnitLevelManager : ScriptableObject
{
    // -- Serialize Fields --
    [SerializeField]
    UnitFamily[] unitFamilies;

    // -- Behavior --
    public void Upgrade(Unit unit, UnitType type)
    {
        UnitFamily family = unitFamilies[(int) type];
        int nextLevel = Mathf.Max((unit.level + 1) % family.members.Length, unit.level);
        GameObject upgrade = family.members[nextLevel];

        // TODO: Implement what happends after upgrade

    }
}
