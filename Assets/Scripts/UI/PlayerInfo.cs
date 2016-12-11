using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour {

    public UIInGameInfo IngameInfo;
    public static int DeadCounter = 0;
    public Text Position;
    public Text Name;
    public Text Health;
    public Image Background;
    bool isDead = false;
    
    public void Initialize(string name, float health, Color backgroundColor)
    {
        UpdateInformation(health);
        Name.text = name;
        Background.color = backgroundColor;
    }

    public void UpdateInformation(float newHealth)
    {
        if (isDead) return;
        if(newHealth <= 0)
        {
            PlayerDeath();
            Health.text = "0%";
            Position.text = "#" + (4 - DeadCounter).ToString();
            ++DeadCounter;
            isDead = true;
            IngameInfo.AddToDeadPlayers(this);
        }
        else
        {
            Health.text = newHealth.ToString() + "%";
        }

    }

    public void PlayerDeath()
    {
        Background.color = new Color(Background.color.r, Background.color.g, Background.color.b, 0.02f);
    }
}
