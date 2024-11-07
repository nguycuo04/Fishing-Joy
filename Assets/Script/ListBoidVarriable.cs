using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/List GameObject Varriable")]
public class ListBoidVarriable : ScriptableObject
{
    public List <BoidMovement> boidMovements = new List<BoidMovement>();
}
