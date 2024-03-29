using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SurfaceMenuBehaviour : MonoBehaviour
{
    [SerializeField] GameObject leftButton, rightButton, totalScore, jumpButton, equipmentButton, openShopButton, talkButton, equipmentScreen, equipmentBack, equipmentLeft, equipmentRight, equipmentFlipperButton, equipmentFlipperIcon, shopScreen, shopBack, shopLeft, shopRight, shopBuy, shopHatIcon, equipmentHatIcon, settingsScreen, settingsBack, quitButton, muteButton, muteButtonIcon, guideButton, liscencesButton, creditsButton, guideScreen, guideBackButton, creditsScreen, creditsBackButton, liscencesScreen, liscencesBackButton;
    [SerializeField] TextMeshProUGUI scoreText, highScoreText, hatNameText, shopHatNameText, shopBuyText, shopPriceText, shopDescription;
    [SerializeField] Sprite musicButtonOn, musicButtonOff;
    [SerializeField] AudioMixer audioMix;
    UISounds sound;
    RectTransform leftButtonT, rightButtonT, totalScoreT, jumpButtonT, equipmentButtonT, openShopButtonT, talkButtonT, equipmentScreenT, shopScreenT, settingsScreenT, guideScreenT, creditsScreenT, liscencesScreenT;
    Button leftButtonB, rightButtonB, totalScoreB, jumpButtonB, equipmentButtonB, openShopButtonB, talkButtonB, equipmentBackB, equipmentLeftB, equipmentRightB, equipmentFlipperButtonB, shopBackB, shopLeftB, shopRightB, shopBuyB, settingsBackB, quitButtonB, muteButtonB, guideButtonB, liscencesButtonB, creditsButtonB, guideBackButtonB, creditsBackButtonB, liscencesBackButtonB;
    RectTransform hpBar, pauseButton, paperCount;
    Image equipmentFlipperIconI, shopHatIconI, equipmentHatIconI, muteButtonIconI;
    Rigidbody2D playerRigidbody;
    SpriteRenderer playerSprite, armSprite, hatSprite;
    Animator playerAnimator, armAnimator, hatAnimator;
    bool sequenceStopped = false;
    bool flipperEquipped, musicMuted;
    float sequence, exponentialSequence = 0f;



    const float maxVolume = -8f;



    int menuAnimId = 0;
    int currentMenu = 0;

    int shopSelectedHat = 0;
    
    void Start()
    {
        GameManager.totalScore += GameManager.score;
        if (GameManager.score > GameManager.highScore) GameManager.highScore = GameManager.score;
        GameManager.Save();
        scoreText.text = GameManager.totalScore + "";
        highScoreText.text = GameManager.highScore + "";
        GameManager.score = 0;

        sound = GameObject.Find("UISounds").GetComponent<UISounds>();

        leftButtonT = leftButton.GetComponent<RectTransform>();
        rightButtonT = rightButton.GetComponent<RectTransform>();
        totalScoreT = totalScore.GetComponent<RectTransform>();
        jumpButtonT = jumpButton.GetComponent<RectTransform>();
        equipmentButtonT = equipmentButton.GetComponent<RectTransform>();
        openShopButtonT = openShopButton.GetComponent<RectTransform>();
        talkButtonT = talkButton.GetComponent<RectTransform>();
        equipmentScreenT = equipmentScreen.GetComponent<RectTransform>();
        shopScreenT = shopScreen.GetComponent<RectTransform>();
        settingsScreenT = settingsScreen.GetComponent<RectTransform>();
        guideScreenT = guideScreen.GetComponent<RectTransform>();
        creditsScreenT = creditsScreen.GetComponent<RectTransform>();
        liscencesScreenT = liscencesScreen.GetComponent<RectTransform>();

        equipmentFlipperIconI = equipmentFlipperIcon.GetComponent<Image>();
        flipperEquipped = (GameManager.unlocks & 0b_1_0000) == 0b_1_0000;
        equipmentFlipperIconI.color = flipperEquipped ? Color.white : new Color(0.14f, 0.117f, 0.18f);
        shopHatIconI = shopHatIcon.GetComponent<Image>();
        equipmentHatIconI = equipmentHatIcon.GetComponent<Image>();
        muteButtonIconI = muteButtonIcon.GetComponent<Image>();
        musicMuted = (GameManager.unlocks & 0b_100_0000_0000) == 0b_100_0000_0000;
        muteButtonIconI.sprite = musicMuted ? musicButtonOff : musicButtonOn;
        audioMix.SetFloat("Music", musicMuted ? -80f : maxVolume);

        leftButtonB = leftButton.GetComponent<Button>();
        rightButtonB = rightButton.GetComponent<Button>();
        jumpButtonB = jumpButton.GetComponent<Button>();
        equipmentButtonB = equipmentButton.GetComponent<Button>();
        openShopButtonB = openShopButton.GetComponent<Button>();
        talkButtonB = talkButton.GetComponent<Button>();
        equipmentBackB = equipmentBack.GetComponent<Button>();
        equipmentLeftB = equipmentLeft.GetComponent<Button>();
        equipmentRightB = equipmentRight.GetComponent<Button>();
        equipmentFlipperButtonB = equipmentFlipperButton.GetComponent<Button>();
        shopBackB = shopBack.GetComponent<Button>();
        shopLeftB = shopLeft.GetComponent<Button>();
        shopRightB = shopRight.GetComponent<Button>();
        shopBuyB = shopBuy.GetComponent<Button>();
        settingsBackB = settingsBack.GetComponent<Button>();
        quitButtonB = quitButton.GetComponent<Button>();
        muteButtonB = muteButton.GetComponent<Button>();
        guideButtonB = guideButton.GetComponent<Button>();
        liscencesButtonB = liscencesButton.GetComponent<Button>();
        creditsButtonB = creditsButton.GetComponent<Button>();
        guideBackButtonB = guideBackButton.GetComponent<Button>();
        creditsBackButtonB = creditsBackButton.GetComponent<Button>();
        liscencesBackButtonB = liscencesBackButton.GetComponent<Button>();

        leftButtonB.interactable = false;
        rightButtonB.interactable = false;
        jumpButtonB.interactable = false;
        equipmentButtonB.interactable = false;
        openShopButtonB.interactable = false;
        talkButtonB.interactable = false;
        equipmentBackB.interactable = false;
        equipmentLeftB.interactable = false;
        equipmentRightB.interactable = false;
        equipmentFlipperButtonB.interactable = false;
        shopBackB.interactable = false;
        shopLeftB.interactable = false;
        shopRightB.interactable = false;
        shopBuyB.interactable = false;
        settingsBackB.interactable = false;
        quitButtonB.interactable = false;
        muteButtonB.interactable = false;
        guideButtonB.interactable = false;
        liscencesButtonB.interactable = false;
        creditsButtonB.interactable = false;
        liscencesBackButtonB.interactable = false;

        leftButtonT.anchoredPosition = new Vector2(-1000, 0);
        rightButtonT.anchoredPosition = new Vector2(1000, 0);
        totalScoreT.anchoredPosition = new Vector2(0, 1000);

        jumpButtonT.anchoredPosition = new Vector2(0, -1000);
        equipmentButtonT.anchoredPosition = new Vector2(0, -1000);

        openShopButtonT.anchoredPosition = new Vector2(0, -1000);
        talkButtonT.anchoredPosition = new Vector2(0, -1000);

        equipmentScreenT.anchoredPosition = new Vector2(-1000, 0);
        shopScreenT.anchoredPosition = new Vector2(-1000, 0);
        settingsScreenT.anchoredPosition = new Vector2(1000, 0);
        guideScreenT.anchoredPosition = new Vector2(-1000, 0);
        creditsScreenT.anchoredPosition = new Vector2(-1000, 0);
        liscencesScreenT.anchoredPosition = new Vector2(-1000, 0);

        hpBar = GameObject.Find("HPBar").GetComponent<RectTransform>();
        pauseButton = GameObject.Find("PauseButton").GetComponent<RectTransform>();
        paperCount = GameObject.Find("PaperCount").GetComponent<RectTransform>();

        hpBar.anchoredPosition = new Vector2(1000, -135);
        pauseButton.anchoredPosition = new Vector2(-1000, -60);
        paperCount.anchoredPosition = new Vector2(1000, -60);
        
        GameObject player = GameObject.FindWithTag("Player");
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerSprite = player.GetComponent<SpriteRenderer>();
        armSprite = player.transform.Find("Arms").gameObject.GetComponent<SpriteRenderer>();
        hatSprite = player.transform.Find("Hat").gameObject.GetComponent<SpriteRenderer>();
        playerAnimator = player.GetComponent<Animator>();
        armAnimator = player.transform.Find("Arms").gameObject.GetComponent<Animator>();
        hatAnimator = player.transform.Find("Hat").gameObject.GetComponent<Animator>();

        playerAnimator.Play("Idle");
        armAnimator.Play("Idle");
        hatAnimator.Play("Idle");

        RefreshShopScreen();
        RefreshEquipmentScreen();
        //sequence = 12.8f;
        sequence = 11f;
    }

    
    void FixedUpdate()
    {
        if (!sequenceStopped) MenuAnimation();
    }

    public void StartGame()
    {
        sound.PlaySound("Click");
        DoMenuAnim(1);
    }

    public void PressLeft()
    {
        sound.PlaySound("Click");
        if (currentMenu == 0)
        {
            DoMenuAnim(2);
        }
    }

    public void PressRight()
    {
        sound.PlaySound("Click");
        if (currentMenu == 0)
        {
            DoMenuAnim(8);
            jumpButtonB.interactable = false;
            equipmentButtonB.interactable = false;
        }
        else if (currentMenu == 1)
        {
            DoMenuAnim(3);
        }
    }

    public void PressCloseSettings()
    {
        sound.PlaySound("Click");
        settingsBackB.interactable = false;
        muteButtonB.interactable = false;
        quitButtonB.interactable = false;
        guideButtonB.interactable = false;
        liscencesButtonB.interactable = false;
        creditsButtonB.interactable = false;
        DoMenuAnim(9);
    }
    public void PressQuit()
    {
        sound.PlaySound("Click");
        Application.Quit();
    }
    public void PressOpenShop()
    {
        sound.PlaySound("Click");
        openShopButtonB.interactable = false;
        talkButtonB.interactable = false;
        leftButtonB.interactable = false;
        rightButtonB.interactable = false;
        equipmentButtonB.interactable = false;
        DoMenuAnim(6);
    }

    public void PressCloseShop()
    {
        sound.PlaySound("Click");
        shopBackB.interactable = false;
        shopLeftB.interactable = false;
        shopRightB.interactable = false;
        shopBuyB.interactable = false;
        DoMenuAnim(7);
    }

    public void PressLeftShop()
    {
        sound.PlaySound("Click");
        shopSelectedHat -= 1;
        if (shopSelectedHat == -1) shopSelectedHat = 4;
        RefreshShopScreen();
    }

    public void PressRightShop()
    {
        sound.PlaySound("Click");
        shopSelectedHat += 1;
        if (shopSelectedHat == 5) shopSelectedHat = 0;
        RefreshShopScreen();
    }

    public void PressBuyShop()
    {
        sound.PlaySound("Click");
        int[] prices = new int[] {5000, 30000, 50000, 100000, 999999};
        int[] hatIds = new int[] {0b_10_0000, 0b_100_0000, 0b_1000_0000, 0b_1_0000_0000, 0b_10_0000_0000};
        if (GameManager.totalScore >= prices[shopSelectedHat])
        {
            GameManager.totalScore -= prices[shopSelectedHat];
            GameManager.unlocks |= hatIds[shopSelectedHat];
            GameManager.equippedHat = shopSelectedHat + 1;
            RefreshEquipmentScreen();
            RefreshShopScreen();
            scoreText.text = GameManager.totalScore + "";
        }
    }

    void RefreshShopScreen()
    {
        if (shopSelectedHat == 0)
        {
            shopHatNameText.text = "Keltanokka";
            shopDescription.text = "Aloittelijoille, saat vähemmän vahinkoa ja pisteitä.";
            shopHatIconI.sprite = Resources.LoadAll<Sprite>("yellow_cap")[5];
            if ((GameManager.unlocks & 0b_10_0000) == 0b_10_0000)
            {
                shopBuyText.text = "Ostettu";
                shopPriceText.text = "";
                shopBuyB.interactable = false;
            }
            else
            {
                shopBuyText.text = "Osta";
                shopPriceText.text = "5 000";
                shopBuyB.interactable = true;
            }
        }
        else if (shopSelectedHat == 1)
        {
            shopHatNameText.text = "Liekkipipo";
            shopDescription.text = "Näiden käyttäjät olivat kuulemma erittäin nopeita.";
            shopHatIconI.sprite = Resources.LoadAll<Sprite>("firebeanie")[5];
            if ((GameManager.unlocks & 0b_100_0000) == 0b_100_0000)
            {
                shopBuyText.text = "Ostettu";
                shopPriceText.text = "";
                shopBuyB.interactable = false;
            }
            else
            {
                shopBuyText.text = "Osta";
                shopPriceText.text = "30 000";
                shopBuyB.interactable = true;
            }
        }
        else if (shopSelectedHat == 2)
        {
            shopHatNameText.text = "Kompassi";
            shopDescription.text = "Osoittaa tien aarteiden luo.";
            shopHatIconI.sprite = Resources.LoadAll<Sprite>("compass_hat")[5];
            if ((GameManager.unlocks & 0b_1000_0000) == 0b_1000_0000)
            {
                shopBuyText.text = "Ostettu";
                shopPriceText.text = "";
                shopBuyB.interactable = false;
            }
            else
            {
                shopBuyText.text = "Osta";
                shopPriceText.text = "50 000";
                shopBuyB.interactable = true;
            }
        }
        else if (shopSelectedHat == 3)
        {
            shopHatNameText.text = "Kovislakki";
            shopDescription.text = "Elämä on tuskaa, mutta saat paljon pisteitä.";
            shopHatIconI.sprite = Resources.LoadAll<Sprite>("hardcore_hat")[5];
            if ((GameManager.unlocks & 0b_1_0000_0000) == 0b_1_0000_0000)
            {
                shopBuyText.text = "Ostettu";
                shopPriceText.text = "";
                shopBuyB.interactable = false;
            }
            else
            {
                shopBuyText.text = "Osta";
                shopPriceText.text = "100 000";
                shopBuyB.interactable = true;
            }
        }
        else if (shopSelectedHat == 4)
        {
            shopHatNameText.text = "Kultapytty";
            shopDescription.text = "Paras sijoitus ikinä.";
            shopHatIconI.sprite = Resources.LoadAll<Sprite>("golden_useless_hat")[5];
            if ((GameManager.unlocks & 0b_10_0000_0000) == 0b_10_0000_0000)
            {
                shopBuyText.text = "Ostettu";
                shopPriceText.text = "";
                shopBuyB.interactable = false;
            }
            else
            {
                shopBuyText.text = "Osta";
                shopPriceText.text = "999 999";
                shopBuyB.interactable = true;
            }
        }
    }

    public void PressEquipment()
    {
        sound.PlaySound("Click");
        leftButtonB.interactable = false;
        rightButtonB.interactable = false;
        jumpButtonB.interactable = false;
        equipmentButtonB.interactable = false;
        openShopButtonB.interactable = false;
        talkButtonB.interactable = false;
        equipmentBackB.interactable = false;
        equipmentLeftB.interactable = false;
        equipmentRightB.interactable = false;
        equipmentFlipperButtonB.interactable = false;
        DoMenuAnim(4);
    }

    void RefreshEquipmentScreen()
    {
        GameManager.damageMultiplier = 1f;
        GameManager.healingMultiplier = 1f;
        GameManager.scoreMultiplier = 1f;
        GameManager.speedMultiplier = 1f;
        GameManager.healBetweenLevels = true;
        GameObject.Find("HatParticles").GetComponent<ParticleSystem>().Stop();

        if (GameManager.equippedHat == 0)
        {
            hatNameText.text = "Ei hattua";
            equipmentHatIconI.sprite = Resources.LoadAll<Sprite>("no_hat")[5];
        }
        else if (GameManager.equippedHat == 1)
        {
            hatNameText.text = "Keltanokka";
            equipmentHatIconI.sprite = Resources.LoadAll<Sprite>("yellow_cap")[5];
            GameManager.damageMultiplier = 0.5f;
            GameManager.healingMultiplier = 2f;
            GameManager.scoreMultiplier = 0.5f;
        }
        else if (GameManager.equippedHat == 2)
        {
            hatNameText.text = "Liekkipipo";
            equipmentHatIconI.sprite = Resources.LoadAll<Sprite>("firebeanie")[5];
            GameManager.speedMultiplier = 1.5f;
        }
        else if (GameManager.equippedHat == 3)
        {
            hatNameText.text = "Kompassi";
            equipmentHatIconI.sprite = Resources.LoadAll<Sprite>("compass_hat")[5];
        }
        else if (GameManager.equippedHat == 4)
        {
            hatNameText.text = "Kovislakki";
            equipmentHatIconI.sprite = Resources.LoadAll<Sprite>("hardcore_hat")[5];
            GameManager.damageMultiplier = 2f;
            GameManager.scoreMultiplier = 2f;
            GameManager.healBetweenLevels = false;
        }
        else if (GameManager.equippedHat == 5)
        {
            hatNameText.text = "Kultapytty";
            equipmentHatIconI.sprite = Resources.LoadAll<Sprite>("golden_useless_hat")[5];
            GameObject.Find("HatParticles").GetComponent<ParticleSystem>().Play();
        }
        GameObject.FindWithTag("Player").transform.Find("Hat").gameObject.GetComponent<HatSetter>().SetHatType();
        GameObject.FindWithTag("Player").transform.Find("Hat").gameObject.GetComponent<HatSetter>().SetHat(5);
        
        GameManager.Save();
    }

    public void PressCloseEquipment()
    {
        sound.PlaySound("Click");
        DoMenuAnim(5);
    }

    public void PressLeftEquipment()
    {
        sound.PlaySound("Click");
        int testIfNewHats = GameManager.equippedHat;
        int newHat = 0;
        if ((GameManager.unlocks & 0b_10_0000) == 0b_10_0000 && GameManager.equippedHat > 1) newHat = 1;
        if ((GameManager.unlocks & 0b_100_0000) == 0b_100_0000 && GameManager.equippedHat > 2) newHat = 2;
        if ((GameManager.unlocks & 0b_1000_0000) == 0b_1000_0000 && GameManager.equippedHat > 3) newHat = 3;
        if ((GameManager.unlocks & 0b_1_0000_0000) == 0b_1_0000_0000 && GameManager.equippedHat > 4) newHat = 4;
        if ((GameManager.unlocks & 0b_10_0000_0000) == 0b_10_0000_0000 && GameManager.equippedHat > 5) newHat = 5;
        if (testIfNewHats == newHat)
        {
            if ((GameManager.unlocks & 0b_10_0000_0000) == 0b_10_0000_0000) newHat = 5;
            else if ((GameManager.unlocks & 0b_1_0000_0000) == 0b_1_0000_0000) newHat = 4;
            else if ((GameManager.unlocks & 0b_1000_0000) == 0b_1000_0000) newHat = 3;
            else if ((GameManager.unlocks & 0b_100_0000) == 0b_100_0000) newHat = 2;
            else if ((GameManager.unlocks & 0b_10_0000) == 0b_10_0000) newHat = 1;
            else newHat = 0;
        }
        GameManager.equippedHat = newHat;
        RefreshEquipmentScreen();
    }

    public void PressRightEquipment()
    {
        sound.PlaySound("Click");
        int testIfNewHats = GameManager.equippedHat;
        int newHat = GameManager.equippedHat;
        if ((GameManager.unlocks & 0b_10_0000_0000) == 0b_10_0000_0000 && GameManager.equippedHat < 5) newHat = 5;
        if ((GameManager.unlocks & 0b_1_0000_0000) == 0b_1_0000_0000 && GameManager.equippedHat < 4) newHat = 4;
        if ((GameManager.unlocks & 0b_1000_0000) == 0b_1000_0000 && GameManager.equippedHat < 3) newHat = 3;
        if ((GameManager.unlocks & 0b_100_0000) == 0b_100_0000 && GameManager.equippedHat < 2) newHat = 2;
        if ((GameManager.unlocks & 0b_10_0000) == 0b_10_0000 && GameManager.equippedHat < 1) newHat = 1;
        if (testIfNewHats == newHat) newHat = 0;
        GameManager.equippedHat = newHat;
        RefreshEquipmentScreen();
    }
    
    public void PressFlipper()
    {
        sound.PlaySound("Click");
        if (flipperEquipped)
        {
            flipperEquipped = false;
            equipmentFlipperIconI.color = new Color(0.14f, 0.117f, 0.18f);
            GameManager.unlocks ^= 0b_1_0000;
        }
        else
        {
            flipperEquipped = true;
            equipmentFlipperIconI.color = Color.white;
            GameManager.unlocks |= 0b_1_0000;
        }
        GameManager.Save();
    }

    public void PressMute()
    {
        sound.PlaySound("Click");
        if (musicMuted)
        {
            musicMuted = false;
            muteButtonIconI.sprite = musicButtonOn;
            GameManager.unlocks ^= 0b_100_0000_0000;
            audioMix.SetFloat("Music", maxVolume);
        }
        else
        {
            musicMuted = true;
            muteButtonIconI.sprite = musicButtonOff;
            GameManager.unlocks |= 0b_100_0000_0000;
            audioMix.SetFloat("Music", -80f);
        }
        GameManager.Save();
    }

    public void PressGuide()
    {
        sound.PlaySound("Click");
        settingsBackB.interactable = false;
        muteButtonB.interactable = false;
        quitButtonB.interactable = false;
        guideButtonB.interactable = false;
        liscencesButtonB.interactable = false;
        creditsButtonB.interactable = false;
        sequenceStopped = false;
        DoMenuAnim(10);
    }

    public void PressCloseGuide()
    {
        sound.PlaySound("Click");
        guideBackButtonB.interactable = false;
        DoMenuAnim(11);
    }

    public void PressCredits()
    {
        sound.PlaySound("Click");
        settingsBackB.interactable = false;
        muteButtonB.interactable = false;
        quitButtonB.interactable = false;
        guideButtonB.interactable = false;
        liscencesButtonB.interactable = false;
        creditsButtonB.interactable = false;
        sequenceStopped = false;
        DoMenuAnim(14);
    }

    public void PressCloseCredits()
    {
        sound.PlaySound("Click");
        creditsBackButtonB.interactable = false;
        DoMenuAnim(15);
    }

    public void PressLiscences()
    {
        sound.PlaySound("Click");
        settingsBackB.interactable = false;
        muteButtonB.interactable = false;
        quitButtonB.interactable = false;
        guideButtonB.interactable = false;
        liscencesButtonB.interactable = false;
        creditsButtonB.interactable = false;
        sequenceStopped = false;
        DoMenuAnim(12);
    }

    public void PressCloseLiscences()
    {
        sound.PlaySound("Click");
        liscencesBackButtonB.interactable = false;
        DoMenuAnim(13);
    }

    void MenuAnimation()
    {
        if (menuAnimId == 0)
        {
            MenuIn();
        }
        else if (menuAnimId == 1)
        {
            StartGameLoop();
        }
        else if (menuAnimId == 2)
        {
            GoToBearMenu();
        }
        else if (menuAnimId == 3)
        {
            GoToHole();
        }
        else if (menuAnimId == 4)
        {
            OpenEquipmentMenu();
        }
        else if (menuAnimId == 5)
        {
            CloseEquipmentMenu();
        }
        else if (menuAnimId == 6)
        {
            OpenShopMenu();
        }
        else if (menuAnimId == 7)
        {
            CloseShopMenu();
        }
        else if (menuAnimId == 8)
        {
            OpenTutorialAndSettings();
        }
        else if (menuAnimId == 9)
        {
            CloseTutorialAndSettings();
        }
        else if (menuAnimId == 10)
        {
            OpenGuide();
        }
        else if (menuAnimId == 11)
        {
            CloseGuide();
        }
        else if (menuAnimId == 12)
        {
            OpenLiscences();
        }
        else if (menuAnimId == 13)
        {
            CloseLiscences();
        }
        else if (menuAnimId == 14)
        {
            OpenCredits();
        }
        else if (menuAnimId == 15)
        {
            CloseCredits();
        }
    }

    void DoMenuAnim(int id)
    {
        if (id != 0) sequence = 0f;
        exponentialSequence = 0f;
        menuAnimId = id;
        sequenceStopped = false;
        leftButtonB.interactable = false;
        rightButtonB.interactable = false;
        if (id == 1 || id == 2 || id == 3)
        {
            playerAnimator.Play("Walk");
            armAnimator.Play("Walk");
            hatAnimator.Play("Walk");
        }
        if (id == 1 || id == 2)
        {
            equipmentButtonB.interactable = false;
            jumpButtonB.interactable = false;
        }
        if (id == 3)
        {
            equipmentButtonB.interactable = false;
            openShopButtonB.interactable = false;
            talkButtonB.interactable = false;
        }
    }

    void MenuIn()
    {
        sequence += 2 * Time.deltaTime;
        exponentialSequence = Mathf.Pow(2, sequence);
        leftButtonT.anchoredPosition = new Vector2(-1000 + exponentialSequence / 10 + 80, 0);
        rightButtonT.anchoredPosition = new Vector2(1000 - exponentialSequence / 10 - 80, 0);
        totalScoreT.anchoredPosition = new Vector2(0, 1000 - exponentialSequence / 10 - 100);
        jumpButtonT.anchoredPosition = new Vector2(0, -1000 + exponentialSequence / 10 + 200);
        equipmentButtonT.anchoredPosition = new Vector2(0, -1000 + exponentialSequence / 10 + 50);

        if (exponentialSequence > 10000f)
        {
            sound.PlaySound("Swipe");
            leftButtonT.anchoredPosition = new Vector2(80, 0);
            rightButtonT.anchoredPosition = new Vector2(-80, 0);
            totalScoreT.anchoredPosition = new Vector2(0, -100);
            jumpButtonT.anchoredPosition = new Vector2(0, 200);
            equipmentButtonT.anchoredPosition = new Vector2(0, 50);
            leftButtonB.interactable = true;
            rightButtonB.interactable = true;
            jumpButtonB.interactable = true;
            equipmentButtonB.interactable = true;
            sequenceStopped = true;
        }
    }
    
    void OpenShopMenu()
    {
        sequence += 50 * Time.deltaTime;
        exponentialSequence = Mathf.Pow(2, sequence);
        shopScreenT.anchoredPosition = new Vector2(-1000 + exponentialSequence / 10, 0);

        if (exponentialSequence > 10000f)
        {
            sound.PlaySound("Swipe");
            shopScreenT.anchoredPosition = new Vector2(0, 0);
            shopBackB.interactable = true;
            shopLeftB.interactable = true;
            shopRightB.interactable = true;
            RefreshShopScreen();
            sequenceStopped = true;
        }
    }

    void CloseShopMenu()
    {
        sequence += 50 * Time.deltaTime;
        exponentialSequence = Mathf.Pow(2, sequence);
        shopScreenT.anchoredPosition = new Vector2(0 + exponentialSequence / 10, 0);

        if (exponentialSequence > 10000f)
        {
            sound.PlaySound("Swipe");
            equipmentButtonB.interactable = true;
            openShopButtonB.interactable = true;
            talkButtonB.interactable = true;
            //leftButtonB.interactable = true;
            rightButtonB.interactable = true;
            sequenceStopped = true;
        }
    }

    void OpenTutorialAndSettings()
    {
        sequence += 50 * Time.deltaTime;
        exponentialSequence = Mathf.Pow(2, sequence);
        settingsScreenT.anchoredPosition = new Vector2(1000 - exponentialSequence / 10, 0);

        if (exponentialSequence > 10000f)
        {
            sound.PlaySound("Swipe");
            settingsScreenT.anchoredPosition = new Vector2(0, 0);
            settingsBackB.interactable = true;
            muteButtonB.interactable = true;
            quitButtonB.interactable = true;
            guideButtonB.interactable = true;
            liscencesButtonB.interactable = true;
            creditsButtonB.interactable = true;
            sequenceStopped = true;
        }
    }

    void CloseTutorialAndSettings()
    {
        sequence += 50 * Time.deltaTime;
        exponentialSequence = Mathf.Pow(2, sequence);
        settingsScreenT.anchoredPosition = new Vector2(0 + exponentialSequence / 10, 0);

        if (exponentialSequence > 10000f)
        {
            sound.PlaySound("Swipe");
            equipmentButtonB.interactable = true;
            jumpButtonB.interactable = true;
            leftButtonB.interactable = true;
            rightButtonB.interactable = true;
            sequenceStopped = true;
        }
    }

    void OpenGuide()
    {
        sequence += 50 * Time.deltaTime;
        exponentialSequence = Mathf.Pow(2, sequence);
        guideScreenT.anchoredPosition = new Vector2(-1000 + exponentialSequence / 10, 0);

        if (exponentialSequence > 10000f)
        {
            sound.PlaySound("Swipe");
            guideScreenT.anchoredPosition = new Vector2(0, 0);
            guideBackButtonB.interactable = true;
            sequenceStopped = true;
        }
    }

    void CloseGuide()
    {
        sequence += 50 * Time.deltaTime;
        exponentialSequence = Mathf.Pow(2, sequence);
        guideScreenT.anchoredPosition = new Vector2(0 + exponentialSequence / 10, 0);

        if (exponentialSequence > 10000f)
        {
            sound.PlaySound("Swipe");
            settingsBackB.interactable = true;
            muteButtonB.interactable = true;
            quitButtonB.interactable = true;
            guideButtonB.interactable = true;
            liscencesButtonB.interactable = true;
            creditsButtonB.interactable = true;
            sequenceStopped = true;
        }
    }

    void OpenLiscences()
    {
        sequence += 50 * Time.deltaTime;
        exponentialSequence = Mathf.Pow(2, sequence);
        liscencesScreenT.anchoredPosition = new Vector2(-1000 + exponentialSequence / 10, 0);

        if (exponentialSequence > 10000f)
        {
            sound.PlaySound("Swipe");
            liscencesScreenT.anchoredPosition = new Vector2(0, 0);
            liscencesBackButtonB.interactable = true;
            sequenceStopped = true;
        }
    }

    void CloseLiscences()
    {
        sequence += 50 * Time.deltaTime;
        exponentialSequence = Mathf.Pow(2, sequence);
        liscencesScreenT.anchoredPosition = new Vector2(0 + exponentialSequence / 10, 0);

        if (exponentialSequence > 10000f)
        {
            sound.PlaySound("Swipe");
            settingsBackB.interactable = true;
            muteButtonB.interactable = true;
            quitButtonB.interactable = true;
            guideButtonB.interactable = true;
            liscencesButtonB.interactable = true;
            creditsButtonB.interactable = true;
            sequenceStopped = true;
        }
    }

    void OpenCredits()
    {
        sequence += 50 * Time.deltaTime;
        exponentialSequence = Mathf.Pow(2, sequence);
        creditsScreenT.anchoredPosition = new Vector2(-1000 + exponentialSequence / 10, 0);

        if (exponentialSequence > 10000f)
        {
            sound.PlaySound("Swipe");
            creditsScreenT.anchoredPosition = new Vector2(0, 0);
            creditsBackButtonB.interactable = true;
            sequenceStopped = true;
        }
    }

    void CloseCredits()
    {
        sequence += 50 * Time.deltaTime;
        exponentialSequence = Mathf.Pow(2, sequence);
        creditsScreenT.anchoredPosition = new Vector2(0 + exponentialSequence / 10, 0);

        if (exponentialSequence > 10000f)
        {
            sound.PlaySound("Swipe");
            settingsBackB.interactable = true;
            muteButtonB.interactable = true;
            quitButtonB.interactable = true;
            guideButtonB.interactable = true;
            liscencesButtonB.interactable = true;
            creditsButtonB.interactable = true;
            sequenceStopped = true;
        }
    }

    void OpenEquipmentMenu()
    {
        sequence += 50 * Time.deltaTime;
        exponentialSequence = Mathf.Pow(2, sequence);
        equipmentScreenT.anchoredPosition = new Vector2(-1000 + exponentialSequence / 10, 0);

        if (exponentialSequence > 10000f)
        {
            sound.PlaySound("Swipe");
            equipmentScreenT.anchoredPosition = new Vector2(0, 0);
            equipmentBackB.interactable = true;
            equipmentLeftB.interactable = true;
            equipmentRightB.interactable = true;
            equipmentFlipperButtonB.interactable = (GameManager.unlocks & 0b_1000) == 0b_1000;
            sequenceStopped = true;
        }
    }

    void CloseEquipmentMenu()
    {
        sequence += 50 * Time.deltaTime;
        exponentialSequence = Mathf.Pow(2, sequence);
        equipmentScreenT.anchoredPosition = new Vector2(0 + exponentialSequence / 10, 0);

        if (exponentialSequence > 10000f)
        {
            sound.PlaySound("Swipe");
            equipmentButtonB.interactable = true;
            if (currentMenu == 0)
            {
                jumpButtonB.interactable = true;
                leftButtonB.interactable = true;
                rightButtonB.interactable = true;
            }
            if (currentMenu == 1)
            {
                //leftButtonB.interactable = true;
                rightButtonB.interactable = true;
                talkButtonB.interactable = true;
                openShopButtonB.interactable = true;
            }
            sequenceStopped = true;
        }
    }

    bool playerJumped = false;
    bool playerJumped2 = false;

    void GoToBearMenu()
    {
        sequence += 10 * Time.deltaTime;
        if (exponentialSequence < 10000f)
        {
            exponentialSequence = Mathf.Pow(2, sequence);
            jumpButtonT.anchoredPosition = new Vector2(0, -exponentialSequence / 10 + 200);

            openShopButtonT.anchoredPosition = new Vector2(-1000 + exponentialSequence / 10, 260);
            talkButtonT.anchoredPosition = new Vector2(1000 - exponentialSequence / 10, 150);

            if (exponentialSequence > 10000f)
            {
                sound.PlaySound("Swipe");
                openShopButtonT.anchoredPosition = new Vector2(0, 260);
                talkButtonT.anchoredPosition = new Vector2(0, 150);
            }
        }
        if (sequence < 22f)
        {
            playerRigidbody.MovePosition(Vector2.MoveTowards(playerRigidbody.position, new Vector2(-10.65f, playerRigidbody.position.y), 5f * Time.deltaTime));
            playerSprite.flipX = currentMenu == 0 ? true : false;
            armSprite.flipX = currentMenu == 0 ? true : false;
            hatSprite.flipX = currentMenu == 0 ? true : false;
        }
        if ((playerRigidbody.position.x < -10.64f && playerSprite.flipX) || 
            (playerRigidbody.position.x > -10.66f && !playerSprite.flipX))
        {
            playerAnimator.Play("Idle");
            armAnimator.Play("Idle");
            hatAnimator.Play("Idle");
        }    
        if (sequence > 13.5f)
        {
            currentMenu = 1;
            playerAnimator.Play("Idle");
            armAnimator.Play("Idle");
            hatAnimator.Play("Idle");
            //leftButtonB.interactable = true;
            rightButtonB.interactable = true;
            equipmentButtonB.interactable = true;
            openShopButtonB.interactable = true;
            talkButtonB.interactable = true;
            sequenceStopped = true;
        }
    }
    public void Talk() 
    {
        sound.PlaySound("Click");
        GameManager.OpenDialogue();
    }

    void GoToHole()
    {
        sequence += 10 * Time.deltaTime;
        if (exponentialSequence < 10000f)
        {
            exponentialSequence = Mathf.Pow(2, sequence);

            jumpButtonT.anchoredPosition = new Vector2(0, -1000 + exponentialSequence / 10 + 200);

            openShopButtonT.anchoredPosition = new Vector2(-exponentialSequence / 10, 260);
            talkButtonT.anchoredPosition = new Vector2(exponentialSequence / 10, 150);

            if (exponentialSequence > 10000f)
            {
                sound.PlaySound("Swipe");
                jumpButtonT.anchoredPosition = new Vector2(0, 200);
            }
        }
        if (sequence < 22f)
        {
            playerRigidbody.MovePosition(Vector2.MoveTowards(playerRigidbody.position, new Vector2(-4.5f, playerRigidbody.position.y), 5f * Time.deltaTime));
            playerSprite.flipX = false;
            armSprite.flipX = false;
            hatSprite.flipX = false;
        }
        if (playerRigidbody.position.x > -4.51f)
        {
            playerAnimator.Play("Idle");
            armAnimator.Play("Idle");
            hatAnimator.Play("Idle");
        }
        if (sequence > 13.5f)
        {
            currentMenu = 0;
            playerAnimator.Play("Idle");
            armAnimator.Play("Idle");
            hatAnimator.Play("Idle");
            jumpButtonT.anchoredPosition = new Vector2(0, 200);
            leftButtonB.interactable = true;
            rightButtonB.interactable = true;
            equipmentButtonB.interactable = true;
            jumpButtonB.interactable = true;
            sequenceStopped = true;
        }
    }

    void StartGameLoop()
    {
        sequence += 10 * Time.deltaTime;
        if (exponentialSequence < 10000f)
        {
            exponentialSequence = Mathf.Pow(2, sequence);
            leftButtonT.anchoredPosition = new Vector2(-exponentialSequence / 10 + 80, 0);
            rightButtonT.anchoredPosition = new Vector2(exponentialSequence / 10 - 80, 0);
            totalScoreT.anchoredPosition = new Vector2(0, exponentialSequence / 10 - 100);
            jumpButtonT.anchoredPosition = new Vector2(0, -exponentialSequence / 10 + 200);
            equipmentButtonT.anchoredPosition = new Vector2(0, -exponentialSequence / 10 + 50);
        }

        if (sequence < 10f && playerRigidbody.position.x < -1.99f)
        {
            playerRigidbody.MovePosition(Vector2.MoveTowards(playerRigidbody.position, new Vector2(-2f, playerRigidbody.position.y), 2.5f * Time.deltaTime));
            playerSprite.flipX = false;
            armSprite.flipX = false;
            hatSprite.flipX = false;
        }
        if (playerRigidbody.position.x > -2.01f && !playerJumped)
        {
            playerAnimator.Play("Idle");
            armAnimator.Play("Idle");
            hatAnimator.Play("Idle");
        }
        if (sequence > 10f && !playerJumped)
        {
            playerJumped = true;
            playerAnimator.Play("Jump");
            armAnimator.Play("Jump");
            hatAnimator.Play("Jump");
            GameManager.timeBonus = 1100f;
        }
        if (sequence > 42f && !playerJumped2)
        {
            playerRigidbody.AddForce(new Vector2(3f, 6.5f), ForceMode2D.Impulse);
            GameManager.PlayerClipping(false);
            GameManager.cameraMode = 100;
            playerJumped2 = true;
        }
        if (sequence > 42f)
        {
            exponentialSequence = Mathf.Pow(2, sequence - 22f);

            hpBar.anchoredPosition = new Vector2(1000 - exponentialSequence / 10 - 130, -135);
            pauseButton.anchoredPosition = new Vector2(-1000 + exponentialSequence / 10 + 60, -60);
            paperCount.anchoredPosition = new Vector2(1000 - exponentialSequence / 10 - 130, -60);

            if (exponentialSequence > 10000f)
            {
                hpBar.anchoredPosition = new Vector2(-130, -135);
                pauseButton.anchoredPosition = new Vector2(60, -60);
                paperCount.anchoredPosition = new Vector2(-130, -60);
            }
        }

        if (sequence > 43.5f && sequence < 49f) playerRigidbody.rotation = Mathf.MoveTowards(playerRigidbody.rotation, -180f, 330 * Time.deltaTime);
        if (sequence > 62.5f)
        {
            playerRigidbody.gravityScale = 0f;
            GameManager.PlayerClipping(true);
            GameManager.playMode = 0;
            GameManager.PauseWorld(false);
            sequenceStopped = true;
            playerRigidbody.velocity = new Vector2(0.1f, -4f);
            GameManager.CloseSurfaceMenu();
        }
    }
}
