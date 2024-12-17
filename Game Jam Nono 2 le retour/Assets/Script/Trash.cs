using UnityEngine;

public class Trash : MonoBehaviour
{
    private PlayerScore ownerPoints;
    public string ownerTag;
    // Set the owner of the trash (when picked up)
    public void SetOwner(PlayerScore playerPoints)
    {
        ownerPoints = playerPoints;
    }

    // Get the PlayerPoints component when we need to award points
    public PlayerScore GetOwnerPoints()
    {
        return ownerPoints;
    }
}