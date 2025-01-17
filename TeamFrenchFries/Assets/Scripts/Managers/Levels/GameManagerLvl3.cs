using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLvl3 : GameManagerBase
{
    #region Public Variables
    [Space, Header("End Area")]
    public Collider2D endCol2D;
    public SpriteRenderer endDoorImg;
    public Sprite openDoorImg;
    public SpriteRenderer[] pressurePlateImg;
    public Sprite pressedPlateImg;
    #endregion

    #region Private Variables
    private bool _hitPlate1;
    private bool _hitPlate2;
    private bool _hitPlate3;
    private bool _hitPlate4;
    #endregion

    #region Unity Callbacks

    #region Events
    void OnEnable()
    {
        PlayerController.OnLevelEnded += OnLevelEndedEventReceived;
        PlayerController.OnPressurePlatePressed += OnPressurePlatePressedEventReceived;
        PlayerController.OnVoidDeath += OnVoidDeathEventReceived;
    }

    void OnDisable()
    {
        PlayerController.OnLevelEnded -= OnLevelEndedEventReceived;
        PlayerController.OnPressurePlatePressed -= OnPressurePlatePressedEventReceived;
        PlayerController.OnVoidDeath -= OnVoidDeathEventReceived;
    }

    void OnDestroy()
    {
        PlayerController.OnLevelEnded -= OnLevelEndedEventReceived;
        PlayerController.OnPressurePlatePressed -= OnPressurePlatePressedEventReceived;
        PlayerController.OnVoidDeath -= OnVoidDeathEventReceived;
    }
    #endregion

    void Start()
    {
        StartCoroutine(StartGameDelay());
        HumanDimensionAudio(true);
    }

//#if UNITY_STANDALONE
//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Space) && gmData.currState != GameMangerData.GameState.Switch
//            && gmData.currState != GameMangerData.GameState.Paused)
//            StartCoroutine(SwitchDimensionDelay());

//        if (Input.GetKeyDown(KeyCode.Escape) && gmData.currState == GameMangerData.GameState.Game)
//            PauseGame();
//    }
//#endif

    #endregion

    #region Events
    void OnPressurePlatePressedEventReceived(int index)
    {
        if (index == 1)
        {
            _hitPlate1 = true;
            pressurePlateImg[0].sprite = pressedPlateImg;
        }

        if (index == 2 && _hitPlate1)
        {
            _hitPlate2 = true;
            pressurePlateImg[1].sprite = pressedPlateImg;
        }

        if (index == 3 && _hitPlate2)
        {
            _hitPlate3 = true;
            pressurePlateImg[2].sprite = pressedPlateImg;
        }

        if (index == 4 && _hitPlate3)
        {
            _hitPlate4 = true;
            pressurePlateImg[3].sprite = pressedPlateImg;
        }

        if (_hitPlate4)
        {
            endDoorImg.sprite = openDoorImg;
            endCol2D.enabled = true;
            doorSFXAud.Play();
        }
    }

    void OnVoidDeathEventReceived() => StartCoroutine(DeathScreenDelay());
    #endregion
}