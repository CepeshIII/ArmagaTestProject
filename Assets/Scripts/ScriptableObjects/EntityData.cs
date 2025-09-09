using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(fileName = "EntityData", menuName = "Scriptable Objects/EntityData")]
public class EntityData : ScriptableObject
{
    public Tile tile;
    public string entityName;
    public string entityId;
}
