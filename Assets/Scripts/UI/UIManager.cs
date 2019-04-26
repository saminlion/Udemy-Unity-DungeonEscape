using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                throw new UnityException("UI Manager is null");
            }

            return _instance;
        }
    }

    public Text playerGemCountText;

    public Image selectionImage;

    public Text gemCountText;

    public Image[] livesRemainingImages;

    private void Awake()
    {
        _instance = this;

        selectionImage.enabled = false;
    }

    public void OpenShop(int gemCount)
    {
        playerGemCountText.text = "" + gemCount + "G";
    }

    public void UpdateShopSelection(int yPos)
    {
        selectionImage.enabled = true;
        selectionImage.rectTransform.anchoredPosition = new Vector2(selectionImage.rectTransform.anchoredPosition.x, yPos);
    }

    public void UpdateGemCount(int count)
    {
        gemCountText.text = "" + count;
    }

    public void UpdateLife(int livesrRemaining)
    {
        //loop through lives;
        //i == livesrRemaining
        //hide that one
        for (int i = 0; i < livesRemainingImages.Length; i++)
        {
            if (i == livesrRemaining)
            {
                livesRemainingImages[i].enabled = false;
            }
        }
    }
}
