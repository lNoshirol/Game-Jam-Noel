using TMPro;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public TextMeshProUGUI text;

    private PlayerSetUp ownerPoints;
    public string ownerTag;
    // Set the owner of the trash (when picked up)
    public void SetOwner(PlayerSetUp playerPoints)
    {
        ownerPoints = playerPoints;
    }

    // Get the PlayerPoints component when we need to award points
    public PlayerSetUp GetOwnerPoints()
    {
        return ownerPoints;
    }
}