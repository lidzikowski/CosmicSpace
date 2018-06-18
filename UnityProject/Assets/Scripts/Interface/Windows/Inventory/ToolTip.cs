using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    public Text Name;
    public Text Type;
    public Text Quality;
    public Text Bonus;
    public Text RequiredLevel;
    public Text UpgradeLevel;
    public Text Value;
    public Text Description;

    public void Refresh(Item item)
    {
        Name.text = item.Name.ToString();
        Type.text = item.Type.ToString();
        Quality.text = item.Quality.ToString();
        Bonus.text = item.Bonus.ToString();
        RequiredLevel.text = item.RequiredLevel.ToString();
        UpgradeLevel.text = item.UpgradeLevel.ToString();
        Value.text = item.Value.ToString();
        Description.text = item.Description;
    }
}