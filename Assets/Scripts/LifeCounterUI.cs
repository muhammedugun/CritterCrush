using UnityEngine;
using UnityEngine.UI;

public class LifeCounterUI : MonoBehaviour
{
    [SerializeField] private Transform _lives;
    [SerializeField] private Sprite _lifeSprite;

    private void Start()
    {
        if (_lives != null)
        {
            int lifeCount = LifeManager.GetHeartCount();
            for (int i = 1; i < lifeCount+1; i++)
            {
                _lives.GetChild(i).GetComponent<Image>().sprite = _lifeSprite;
            }
            
        }
        else
        {
            Debug.LogWarning("Lives atanmamýþ!");
        }
    }
}
