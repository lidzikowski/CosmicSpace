using UnityEngine;
using UnityEngine.UI;

public class StatisticWithoutUpgrade : MonoBehaviour
{
    public Text Statistic;
    public Text Bonus;

    public void Refresh(Upgrades statistic, int bonus)
    {
        Statistic.text = statistic.ToString();
        Bonus.text = bonus.ToString();
    }
}