using UnityEngine;
using YG;
using UnityEngine.UI;
using System.Collections;
using System.Numerics;

public class Manager : MonoBehaviour
{
    [SerializeField] private Button[] buttonsOnOff;
    [SerializeField] private Collider2D[] collidersOnOff;

    [SerializeField] private Image background;
    [SerializeField] private Sprite[] backsSprites;
    [SerializeField] private Transform lbParent, textClickParent;
    [SerializeField] private Image progressImage, sunImage;
    [SerializeField] private Text counterText, btcPerSecText, progressPercent, currentLevelText, new_level_price, ad_showen_text, rankText;
    [SerializeField] private AudioClip buy_sound, noMoney, newComp, new_level_can_snd, moneyAdded;
    [SerializeField] private AudioSource audio_source;
    [SerializeField] private GameObject adBtn, leaderBoard, new_levelOBJ, _ClickTextPrefab, forbiddenImg, textPrefabNewLevel;
    [SerializeField] private GameObject[] comps;
    [SerializeField] private Font trFont;

    private string rank1, rank2, rank3, rank4, rank5, rank6, rank7, rank8, rank9, rank10, rank11, rank12;

    private float targetScale = 1.2f;
    private BigInteger new_level_price_num = 1000;

    public Color blinkColor = Color.red;
    public float blinkDuration = 0.2f;
    private Color originalColor;
    private bool isBlinking = false;

    public BigInteger bitcoins = 0;
    public BigInteger _plus_click = 1;
    public BigInteger _plus_auto = 0;

    public BigInteger slot_buy_count_1 = 0;
    public BigInteger slot_buy_count_2 = 0;
    public BigInteger slot_buy_count_3 = 0;
    public BigInteger slot_buy_count_4 = 0;
    public BigInteger slot_buy_count_5 = 0;
    public BigInteger slot_buy_count_6 = 0;
    public BigInteger slot_buy_count_7 = 0;
    public BigInteger slot_buy_count_8 = 0;
    public BigInteger slot_buy_count_9 = 0;
    public BigInteger slot_buy_count_10 = 0;
    public BigInteger slot_buy_count_11 = 0;
    public BigInteger slot_buy_count_12 = 0;

    public BigInteger slot_price_1 = 250;
    public BigInteger slot_price_2 = 500;
    public BigInteger slot_price_3 = 1500;
    public BigInteger slot_price_4 = 4500;
    public BigInteger slot_price_6 = 10000;
    public BigInteger slot_price_7 = 65000;
    public BigInteger slot_price_8 = 120000;
    public BigInteger slot_price_9 = 250000;
    public BigInteger slot_price_10 = 500000;
    public BigInteger slot_price_11 = 1250000;
    public BigInteger slot_price_12 = 3500000;
    private bool allowRotate = false;
    private bool isALreadyBuyShow = false;

    private string num1, num2, num3, num4, num5, num6, num7, num8, num9, num10, num11,
        num12, num13, num14, num15, num16, num17, num18, num19, num20, num21, max_text, max_level_text;
    private void Start()
    {
        if(YandexGame.savesData.isBuyBtnShow == true)
        {
            showBuyLevel();
            isALreadyBuyShow = true;
        }
        originalColor = counterText.color;
        if (YandexGame.SDKEnabled == true)
        {
            ad_showen_text.text = YandexGame.savesData.ad_showen + "/4";

            UpdateLang(YandexGame.lang);
            Update_comp();
            StartCoroutine(Saves_Coroutine());
            StartCoroutine(Auto_BTC_Coroutine());
            ConveringNumbers();

            switch (YandexGame.savesData.current_comp)
            {
                case var _ when YandexGame.savesData.current_comp >= 14:
                    background.sprite = backsSprites[3];
                    break;
                case var _ when YandexGame.savesData.current_comp >= 10:
                    background.sprite = backsSprites[2];
                    break;
                case var _ when YandexGame.savesData.current_comp >= 5:
                    background.sprite = backsSprites[1];
                    break;
                default:
                    background.sprite = backsSprites[0];
                    break;
            }
        }
    }

    private void UpdateLang(string str)
    {
        if (str == "ru")
        {
            max_level_text = "Максимальный";

            rank1 = "Посетитель";
            rank2 = "Начинающий";
            rank3 = "Начинающий кофеман";
            rank4 = "Любитель";
            rank5 = "Администратор";
            rank6 = "Бизнесмен";
            rank7 = "Предприниматель";
            rank8 = "Бариста";
            rank9 = "Кофеман";
            rank10 = "Магнат";
            rank11 = "Кофейный магнат";
            rank12 = "Основатель";

            num1 = " Тыс";
            num2 = " Млн";
            num3 = " Млрд";
            num4 = " Тн";
            num5 = " К";
            num6 = " Кн";
            num7 = " Ст";
            num8 = " Сп";
            num9 = " О";
            num10 = " Н";
            num11 = " Д";
            num12 = " Уд";
            num13 = " Дд";
            num14 = " Тд";
            num15 = " Чад";
            num16 = " Пнд";
            num17 = " Шнд";
            num18 = " Спд";
            num19 = " Внд";
            num20 = " Днд";
            num21 = " Двд";
            max_text = "Максимум";
        }
        else if (str == "en")
        {
            max_level_text = "Maximum";

            rank1 = "Visitor";
            rank2 = "Beginner";
            rank3 = "Beginner coffee drinker";
            rank4 = "Amateur";
            rank5 = "Administrator";
            rank6 = "Businessman";
            rank7 = "Entrepreneur";
            rank8 = "Barista";
            rank9 = "Coffee lover";
            rank10 = "Tycoon";
            rank11 = "Coffee Tycoon";
            rank12 = "Founder";

            num1 = " K";
            num2 = " M";
            num3 = " B";
            num4 = " T";
            num5 = " Q";
            num6 = " Qn";
            num7 = " Sx";
            num8 = " Sp";
            num9 = " Oc";
            num10 = " No";
            num11 = " Dc";
            num12 = " Ud";
            num13 = " Dd";
            num14 = " Td";
            num15 = " Qad";
            num16 = " Qnd";
            num17 = " Sxd";
            num18 = " Spd";
            num19 = " Ocd";
            num20 = " Nvd";
            num21 = " Vg";
            max_text = "Maximum";
        }
        else if (str == "tr")
        {
            max_level_text = "Maksimum";
            btcPerSecText.font = trFont;
            currentLevelText.font = trFont;
            new_level_price.font = trFont;
            counterText.font = trFont;

            rank1 = "Ziyaretçi";
            rank2 = "Başlangıç";
            rank3 = "Yeni başlayan kahve içicisi";
            rank4 = "Amatör";
            rank5 = "Yönetici";
            rank6 = "İşadamı";
            rank7 = "Girişimci";
            rank8 = "Barista";
            rank9 = "Kahve aşığı";
            rank10 = "İşadamı";
            rank11 = "Kahve Kralı";
            rank12 = "Kurucu";

            num1 = " K";
            num2 = " M";
            num3 = " B";
            num4 = " T";
            num5 = " Q";
            num6 = " Qn";
            num7 = " Sx";
            num8 = " Sp";
            num9 = " Oc";
            num10 = " No";
            num11 = " Dc";
            num12 = " Ud";
            num13 = " Dd";
            num14 = " Td";
            num15 = " Qad";
            num16 = " Qnd";
            num17 = " Sxd";
            num18 = " Spd";
            num19 = " Ocd";
            num20 = " Nvd";
            num21 = " Vg";
            max_text = "Maksimum";
        }
    }

    private void Update_comp()
    {
        switch (YandexGame.savesData.current_comp)
        {
            case 1:
                foreach(GameObject obj in comps)
                {
                    obj.SetActive(false);
                }
                comps[0].SetActive(true);
                progressImage.fillAmount = YandexGame.savesData.currentProgress / 100f;
                progressPercent.text = Mathf.RoundToInt(YandexGame.savesData.currentProgress) + "%";
                break;
            case 2:
                foreach (GameObject obj in comps)
                {
                    obj.SetActive(false);
                }
                comps[1].SetActive(true);
                progressImage.fillAmount = YandexGame.savesData.currentProgress / 100f;
                progressPercent.text = Mathf.RoundToInt(YandexGame.savesData.currentProgress) + "%";
                break;
            case 3:
                foreach (GameObject obj in comps)
                {
                    obj.SetActive(false);
                }
                comps[2].SetActive(true);
                progressImage.fillAmount = YandexGame.savesData.currentProgress / 100f;
                progressPercent.text = Mathf.RoundToInt(YandexGame.savesData.currentProgress) + "%";
                break;
            case 4:
                foreach (GameObject obj in comps)
                {
                    obj.SetActive(false);
                }
                comps[3].SetActive(true);
                progressImage.fillAmount = YandexGame.savesData.currentProgress / 100f;
                progressPercent.text = Mathf.RoundToInt(YandexGame.savesData.currentProgress) + "%";
                break;
            case 5:
                foreach (GameObject obj in comps)
                {
                    obj.SetActive(false);
                }
                comps[4].SetActive(true);
                progressImage.fillAmount = YandexGame.savesData.currentProgress / 100f;
                progressPercent.text = Mathf.RoundToInt(YandexGame.savesData.currentProgress) + "%";
                break;
            case 6:
                foreach (GameObject obj in comps)
                {
                    obj.SetActive(false);
                }
                comps[5].SetActive(true);
                progressImage.fillAmount = YandexGame.savesData.currentProgress / 100f;
                progressPercent.text = Mathf.RoundToInt(YandexGame.savesData.currentProgress) + "%";
                break;
            case 7:
                foreach (GameObject obj in comps)
                {
                    obj.SetActive(false);
                }
                comps[6].SetActive(true);
                progressImage.fillAmount = YandexGame.savesData.currentProgress / 100f;
                progressPercent.text = Mathf.RoundToInt(YandexGame.savesData.currentProgress) + "%";
                break;
            case 8:
                foreach (GameObject obj in comps)
                {
                    obj.SetActive(false);
                }
                comps[7].SetActive(true);
                progressImage.fillAmount = YandexGame.savesData.currentProgress / 100f;
                progressPercent.text = Mathf.RoundToInt(YandexGame.savesData.currentProgress) + "%";
                break;
            case 9:
                foreach (GameObject obj in comps)
                {
                    obj.SetActive(false);
                }
                comps[8].SetActive(true);
                progressImage.fillAmount = YandexGame.savesData.currentProgress / 100f;
                progressPercent.text = Mathf.RoundToInt(YandexGame.savesData.currentProgress) + "%";
                break;
            case 10:
                foreach (GameObject obj in comps)
                {
                    obj.SetActive(false);
                }
                comps[9].SetActive(true);
                progressImage.fillAmount = YandexGame.savesData.currentProgress / 100f;
                progressPercent.text = Mathf.RoundToInt(YandexGame.savesData.currentProgress) + "%";
                break;
            case 11:
                foreach (GameObject obj in comps)
                {
                    obj.SetActive(false);
                }
                comps[10].SetActive(true);
                progressImage.fillAmount = YandexGame.savesData.currentProgress / 100f;
                progressPercent.text = Mathf.RoundToInt(YandexGame.savesData.currentProgress) + "%";
                break;
            case 12:
                foreach (GameObject obj in comps)
                {
                    obj.SetActive(false);
                }
                comps[11].SetActive(true);
                progressImage.fillAmount = YandexGame.savesData.currentProgress / 100f;
                progressPercent.text = Mathf.RoundToInt(YandexGame.savesData.currentProgress) + "%";
                break;
            case 13:
                foreach (GameObject obj in comps)
                {
                    obj.SetActive(false);
                }
                comps[12].SetActive(true);
                progressImage.fillAmount = YandexGame.savesData.currentProgress / 100f;
                progressPercent.text = Mathf.RoundToInt(YandexGame.savesData.currentProgress) + "%";
                break;
            case 14:
                foreach (GameObject obj in comps)
                {
                    obj.SetActive(false);
                }
                comps[13].SetActive(true);
                progressImage.fillAmount = YandexGame.savesData.currentProgress / 100f;
                progressPercent.text = Mathf.RoundToInt(YandexGame.savesData.currentProgress) + "%";
                break;
            case 15:
                foreach (GameObject obj in comps)
                {
                    obj.SetActive(false);
                }
                comps[14].SetActive(true);
                progressImage.fillAmount = YandexGame.savesData.currentProgress / 100f;
                progressPercent.text = Mathf.RoundToInt(YandexGame.savesData.currentProgress) + "%";
                break;
            case 16:
                foreach (GameObject obj in comps)
                {
                    obj.SetActive(false);
                }
                comps[15].SetActive(true);
                progressImage.fillAmount = YandexGame.savesData.currentProgress / 100f;
                progressPercent.text = Mathf.RoundToInt(YandexGame.savesData.currentProgress) + "%";
                break;
            case 17:
                foreach (GameObject obj in comps)
                {
                    obj.SetActive(false);
                }
                comps[16].SetActive(true);
                progressImage.fillAmount = YandexGame.savesData.currentProgress / 100f;
                progressPercent.text = Mathf.RoundToInt(YandexGame.savesData.currentProgress) + "%";
                break;
            case 18:
                foreach (GameObject obj in comps)
                {
                    obj.SetActive(false);
                }
                comps[17].SetActive(true);
                progressImage.fillAmount = YandexGame.savesData.currentProgress / 100f;
                progressPercent.text = Mathf.RoundToInt(YandexGame.savesData.currentProgress) + "%";
                break;
            case 19:
                foreach (GameObject obj in comps)
                {
                    obj.SetActive(false);
                }
                comps[18].SetActive(true);
                progressPercent.text = max_level_text;
                progressImage.fillAmount = 100f;
                break;
        }
    }

    private void Update()
    {
        if(allowRotate == true)
        {
            sunImage.transform.Rotate(UnityEngine.Vector3.forward * 100 * Time.deltaTime);
        }
        if (bitcoins < new_level_price_num)
        {
            new_level_price.color = Color.red;
        }
        else
        {
            new_level_price.color = Color.white;
        }

        if (isALreadyBuyShow == true)
        {
            new_level_price.text = FormatNumberShort(new_level_price_num);
        }
        if (YandexGame.SDKEnabled == true)
        {
            if(YandexGame.savesData.ad_showen == 4)
            {
                forbiddenImg.SetActive(true);
            }
            if (YandexGame.lang == "ru")
            {
                currentLevelText.text = "Кофейня Уровня " + YandexGame.savesData.current_comp;
                btcPerSecText.text = FormatNumberShort(_plus_auto).ToString() + " зерен/сек";
            }
            else if (YandexGame.lang == "en")
            {
                currentLevelText.text = "Coffee Shop Level " + YandexGame.savesData.current_comp;
                btcPerSecText.text = FormatNumberShort(_plus_auto).ToString() + " grains/sec";
            }
            else if (YandexGame.lang == "tr")
            {
                currentLevelText.text = "Kahvehane Seviye " + YandexGame.savesData.current_comp;
                btcPerSecText.text = FormatNumberShort(_plus_auto).ToString() + " tane/sn";
            }

            FormatNumber(bitcoins);
            ShowAdButton();
            UpdateLang(YandexGame.lang);
            UpdateRank();
        }
    }
    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }
    private void ShowAdButton()
    {
        if (bitcoins > 0)
        {
            adBtn.SetActive(true);
        }
        else
        {
            adBtn.SetActive(false);
        }
    }
    public void UpdateTextCounter()
    {
        bitcoins += _plus_click;

        // Получаем позицию курсора
        UnityEngine.Vector3 cursorPosition = Input.mousePosition;
        cursorPosition.z = 10; // Меняем Z-координату для правильного отображения в двухмерном пространстве

        // Преобразуем экранные координаты в мировые
        UnityEngine.Vector3 worldPosition = Camera.main.ScreenToWorldPoint(cursorPosition);

        // Создаем экземпляр _ClickTextPrefab в нужной позиции
        GameObject instantiatedObject = Instantiate(_ClickTextPrefab, worldPosition, UnityEngine.Quaternion.identity, textClickParent);

        Text click_num_fade = instantiatedObject.transform.Find("number").GetComponent<Text>();
        click_num_fade.text = "+" + FormatNumberShort(_plus_click);
    }

    public void UpdateTextCounterAuto()
    {
        bitcoins += _plus_auto;
    }
    private IEnumerator Auto_BTC_Coroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            UpdateTextCounterAuto();
        }
    }
    private IEnumerator Saves_Coroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            ConveringNumbersBack();
            YandexGame.SaveProgress();
        }
    }
    public void BuySlot_1()
    {
        if (TimerBeforeAdsYG.allowClicks == true)
        {
            if (bitcoins >= slot_price_1)
            {
                bitcoins -= slot_price_1;
                _plus_click += 1;
                audio_source.PlayOneShot(buy_sound);
                slot_buy_count_1 += 1;

                BigInteger bigIntValue = (slot_price_1);
                BigInteger multipliedValue = BigInteger.Multiply(bigIntValue, 105);
                BigInteger roundedValue = BigInteger.Divide(multipliedValue, 100);
                slot_price_1 = roundedValue;
                UpdateRank();
            }
            else
            {
                audio_source.PlayOneShot(noMoney);
                StartBlinkingNoMoney();
            }
        }
    }
    public void BuySlot_2()
    {
        if (TimerBeforeAdsYG.allowClicks == true)
        {
            if (bitcoins >= slot_price_2)
            {
                bitcoins -= slot_price_2;
                _plus_auto += 1;
                audio_source.PlayOneShot(buy_sound);
                slot_buy_count_2 += 1;

                BigInteger bigIntValue = (slot_price_2);
                BigInteger multipliedValue = BigInteger.Multiply(bigIntValue, 105);
                BigInteger roundedValue = BigInteger.Divide(multipliedValue, 100);
                slot_price_2 = roundedValue;
                UpdateRank();
            }
            else
            {
                audio_source.PlayOneShot(noMoney);
                StartBlinkingNoMoney();
            }
        }
    }
    public void BuySlot_3()
    {
        if (TimerBeforeAdsYG.allowClicks == true)
        {
            if (bitcoins >= slot_price_3)
            {
                bitcoins -= slot_price_3;
                _plus_click += 10;
                audio_source.PlayOneShot(buy_sound);
                slot_buy_count_3 += 1;

                BigInteger bigIntValue = (slot_price_3);
                BigInteger multipliedValue = BigInteger.Multiply(bigIntValue, 105);
                BigInteger roundedValue = BigInteger.Divide(multipliedValue, 100);
                slot_price_3 = roundedValue;
                UpdateRank();
            }
            else
            {
                audio_source.PlayOneShot(noMoney);
                StartBlinkingNoMoney();
            }
        }
    }
    public void BuySlot_4()
    {
        if (TimerBeforeAdsYG.allowClicks == true)
        {
            if (bitcoins >= slot_price_4)
            {
                bitcoins -= slot_price_4;
                _plus_click += 30;
                audio_source.PlayOneShot(buy_sound);
                slot_buy_count_4 += 1;

                BigInteger bigIntValue = (slot_price_4);
                BigInteger multipliedValue = BigInteger.Multiply(bigIntValue, 105);
                BigInteger roundedValue = BigInteger.Divide(multipliedValue, 100);
                slot_price_4 = roundedValue;
                UpdateRank();
            }
            else
            {
                audio_source.PlayOneShot(noMoney);
                StartBlinkingNoMoney();
            }
        }
    }
    public void BuySlot_5()
    {
        if (TimerBeforeAdsYG.allowClicks == true)
        {
            OpenRewardAd2(2);
        }
    }
    public void BuySlot_6()
    {
        if (TimerBeforeAdsYG.allowClicks == true)
        {
            if (bitcoins >= slot_price_6)
            {
                bitcoins -= slot_price_6;
                _plus_click += 60;
                audio_source.PlayOneShot(buy_sound);
                slot_buy_count_6 += 1;

                BigInteger bigIntValue = (slot_price_6);
                BigInteger multipliedValue = BigInteger.Multiply(bigIntValue, 105);
                BigInteger roundedValue = BigInteger.Divide(multipliedValue, 100);
                slot_price_6 = roundedValue;
                UpdateRank();
            }
            else
            {
                audio_source.PlayOneShot(noMoney);
                StartBlinkingNoMoney();
            }
        }
    }
    public void BuySlot_7()
    {
        if (TimerBeforeAdsYG.allowClicks == true)
        {
            if (bitcoins >= slot_price_7)
            {
                bitcoins -= slot_price_7;
                _plus_auto += 100;
                audio_source.PlayOneShot(buy_sound);
                slot_buy_count_7 += 1;

                BigInteger bigIntValue = (slot_price_7);
                BigInteger multipliedValue = BigInteger.Multiply(bigIntValue, 105);
                BigInteger roundedValue = BigInteger.Divide(multipliedValue, 100);
                slot_price_7 = roundedValue;
                UpdateRank();
            }
            else
            {
                audio_source.PlayOneShot(noMoney);
                StartBlinkingNoMoney();
            }
        }
    }
    public void BuySlot_8()
    {
        if (TimerBeforeAdsYG.allowClicks == true)
        {
            if (bitcoins >= slot_price_8)
            {
                bitcoins -= slot_price_8;
                _plus_click += 210;
                audio_source.PlayOneShot(buy_sound);
                slot_buy_count_8 += 1;

                BigInteger bigIntValue = (slot_price_8);
                BigInteger multipliedValue = BigInteger.Multiply(bigIntValue, 105);
                BigInteger roundedValue = BigInteger.Divide(multipliedValue, 100);
                slot_price_8 = roundedValue;
                UpdateRank();
            }
            else
            {
                audio_source.PlayOneShot(noMoney);
                StartBlinkingNoMoney();
            }
        }
    }
    public void BuySlot_9()
    {
        if (TimerBeforeAdsYG.allowClicks == true)
        {
            if (bitcoins >= slot_price_9)
            {
                bitcoins -= slot_price_9;
                audio_source.PlayOneShot(buy_sound);
                _plus_click += 300;
                slot_buy_count_9 += 1;

                BigInteger bigIntValue = (slot_price_9);
                BigInteger multipliedValue = BigInteger.Multiply(bigIntValue, 105);
                BigInteger roundedValue = BigInteger.Divide(multipliedValue, 100);
                slot_price_9 = roundedValue;
                UpdateRank();
            }
            else
            {
                audio_source.PlayOneShot(noMoney);
                StartBlinkingNoMoney();
            }
        }
    }
    public void BuySlot_10()
    {
        if (TimerBeforeAdsYG.allowClicks == true)
        {
            if (bitcoins >= slot_price_10)
            {
                bitcoins -= slot_price_10;
                audio_source.PlayOneShot(buy_sound);
                _plus_click += 1000;
                slot_buy_count_10 += 1;

                BigInteger bigIntValue = (slot_price_10);
                BigInteger multipliedValue = BigInteger.Multiply(bigIntValue, 105);
                BigInteger roundedValue = BigInteger.Divide(multipliedValue, 100);
                slot_price_10 = roundedValue;
                UpdateRank();
            }
            else
            {
                audio_source.PlayOneShot(noMoney);
                StartBlinkingNoMoney();
            }
        }
    }
    public void BuySlot_11()
    {
        if (TimerBeforeAdsYG.allowClicks == true)
        {
            if (bitcoins >= slot_price_11)
            {
                bitcoins -= slot_price_11;
                audio_source.PlayOneShot(buy_sound);
                _plus_click += 2500;
                slot_buy_count_11 += 1;

                BigInteger bigIntValue = (slot_price_11);
                BigInteger multipliedValue = BigInteger.Multiply(bigIntValue, 105);
                BigInteger roundedValue = BigInteger.Divide(multipliedValue, 100);
                slot_price_11 = roundedValue;
                UpdateRank();
            }
            else
            {
                audio_source.PlayOneShot(noMoney);
                StartBlinkingNoMoney();
            }
        }
    }
    public void BuySlot_12()
    {
        if (TimerBeforeAdsYG.allowClicks == true)
        {
            if (bitcoins >= slot_price_12)
            {
                bitcoins -= slot_price_12;
                audio_source.PlayOneShot(buy_sound);
                _plus_auto += 9500;
                slot_buy_count_12 += 1;

                BigInteger bigIntValue = (slot_price_12);
                BigInteger multipliedValue = BigInteger.Multiply(bigIntValue, 105);
                BigInteger roundedValue = BigInteger.Divide(multipliedValue, 100);
                slot_price_12 = roundedValue;
                UpdateRank();
            }
            else
            {
                audio_source.PlayOneShot(noMoney);
                StartBlinkingNoMoney();
            }
        }
    }
    public void StartBlinkingNoMoney()
    {
        if (!isBlinking)
        {
            StartCoroutine(BlinkText());
        }
    }
    IEnumerator BlinkText()
    {
        isBlinking = true;
        float time = 0f;

        while (time < blinkDuration)
        {
            counterText.color = (counterText.color == originalColor) ? blinkColor : originalColor;
            time += Time.deltaTime;
            yield return null;
        }

        counterText.color = originalColor;
        isBlinking = false;
    }
    void Rewarded(int id)
    {
        if (id == 1)
        {
            if(YandexGame.savesData.ad_showen != 4)
            {
                audio_source.PlayOneShot(moneyAdded);
                YandexGame.savesData.ad_showen += 1;
                ad_showen_text.text = YandexGame.savesData.ad_showen + "/4";
                bitcoins *= 2;
                YandexGame.SaveProgress();
            }
        }
        if (id ==2)
        {
            audio_source.PlayOneShot(buy_sound);
            _plus_auto += 50;
            slot_buy_count_5 += 1;
            UpdateRank();
            YandexGame.SaveProgress();
        }
    }
    public void OpenRewardAd(int id)
    {
        if(YandexGame.savesData.ad_showen != 4)
        {
            YandexGame.RewVideoShow(id);
        }
    }
    public void OpenRewardAd2(int id)
    {
        YandexGame.RewVideoShow(id);
    }

    private void FormatNumber(BigInteger number)
    {
        string formattedNumber = FormatNumberShort(number);
        counterText.text = formattedNumber;
    }

    public string FormatNumberShort(BigInteger number)
    {
        string[] suffixes = { num1, num2, num3, num4, num5, num6, num7, num8, num9, num10, num11,
            num12, num13, num14, num15, num16, num17, num18, num19, num20, num21 };
        int suffixIndex = 0;

        // Handle cases where BigInteger is within decimal range
        if (number <= (BigInteger)decimal.MaxValue &&
            number >= (BigInteger)decimal.MinValue)
        {
            decimal numValue = (decimal)number;
            while (numValue >= 1000 && suffixIndex < suffixes.Length)
            {
                numValue /= 1000;
                suffixIndex++;
            }

            if (suffixIndex >= suffixes.Length)
            {
                return max_text;
            }
            else if (suffixIndex > 0)
            {
                return $"{numValue:0.00}{suffixes[suffixIndex - 1]}";
            }
            else
            {
                return number.ToString();
            }
        }
        else
        {
            // Handle cases exceeding decimal range - keep as BigInteger
            BigInteger numValue = number;
            while (numValue >= 1000 && suffixIndex < suffixes.Length)
            {
                numValue /= 1000;
                suffixIndex++;
            }

            if (suffixIndex >= suffixes.Length)
            {
                return max_text;
            }
            else
            {
                return $"{numValue}{suffixes[suffixIndex - 1]}"; // No decimal formatting here
            }
        }
    }

    private void ConveringNumbers()
    {
        bitcoins = ConvertStringToBigInteger(YandexGame.savesData.bitcoins);
        _plus_click = ConvertStringToBigInteger(YandexGame.savesData._plus_click);
        _plus_auto = ConvertStringToBigInteger(YandexGame.savesData._plus_auto);
        slot_buy_count_1 = ConvertStringToBigInteger(YandexGame.savesData.slot_buy_count_1);
        slot_buy_count_2 = ConvertStringToBigInteger(YandexGame.savesData.slot_buy_count_2);
        slot_buy_count_3 = ConvertStringToBigInteger(YandexGame.savesData.slot_buy_count_3);
        slot_buy_count_4 = ConvertStringToBigInteger(YandexGame.savesData.slot_buy_count_4);
        slot_buy_count_5 = ConvertStringToBigInteger(YandexGame.savesData.slot_buy_count_5);
        slot_buy_count_6 = ConvertStringToBigInteger(YandexGame.savesData.slot_buy_count_6);
        slot_buy_count_7 = ConvertStringToBigInteger(YandexGame.savesData.slot_buy_count_7);
        slot_buy_count_8 = ConvertStringToBigInteger(YandexGame.savesData.slot_buy_count_8);
        slot_buy_count_9 = ConvertStringToBigInteger(YandexGame.savesData.slot_buy_count_9);
        slot_buy_count_10 = ConvertStringToBigInteger(YandexGame.savesData.slot_buy_count_10);
        slot_buy_count_11 = ConvertStringToBigInteger(YandexGame.savesData.slot_buy_count_11);
        slot_buy_count_12 = ConvertStringToBigInteger(YandexGame.savesData.slot_buy_count_12);
        slot_price_1 = ConvertStringToBigInteger(YandexGame.savesData.slot_price_1);
        slot_price_2 = ConvertStringToBigInteger(YandexGame.savesData.slot_price_2);
        slot_price_3 = ConvertStringToBigInteger(YandexGame.savesData.slot_price_3);
        slot_price_4 = ConvertStringToBigInteger(YandexGame.savesData.slot_price_4);
        slot_price_6 = ConvertStringToBigInteger(YandexGame.savesData.slot_price_6);
        slot_price_7 = ConvertStringToBigInteger(YandexGame.savesData.slot_price_7);
        slot_price_8 = ConvertStringToBigInteger(YandexGame.savesData.slot_price_8);
        slot_price_9 = ConvertStringToBigInteger(YandexGame.savesData.slot_price_9);
        slot_price_10 = ConvertStringToBigInteger(YandexGame.savesData.slot_price_10);
        slot_price_11 = ConvertStringToBigInteger(YandexGame.savesData.slot_price_11);
        slot_price_12 = ConvertStringToBigInteger(YandexGame.savesData.slot_price_12);
    }
    private void ConveringNumbersBack()
    {
        YandexGame.savesData.bitcoins = ConvertBigIntegerToString(bitcoins);
        YandexGame.savesData._plus_click = ConvertBigIntegerToString(_plus_click);
        YandexGame.savesData._plus_auto = ConvertBigIntegerToString(_plus_auto);
        YandexGame.savesData.slot_buy_count_1 = ConvertBigIntegerToString(slot_buy_count_1);
        YandexGame.savesData.slot_buy_count_2 = ConvertBigIntegerToString(slot_buy_count_2);
        YandexGame.savesData.slot_buy_count_3 = ConvertBigIntegerToString(slot_buy_count_3);
        YandexGame.savesData.slot_buy_count_4 = ConvertBigIntegerToString(slot_buy_count_4);
        YandexGame.savesData.slot_buy_count_5 = ConvertBigIntegerToString(slot_buy_count_5);
        YandexGame.savesData.slot_buy_count_6 = ConvertBigIntegerToString(slot_buy_count_6);
        YandexGame.savesData.slot_buy_count_7 = ConvertBigIntegerToString(slot_buy_count_7);
        YandexGame.savesData.slot_buy_count_8 = ConvertBigIntegerToString(slot_buy_count_8);
        YandexGame.savesData.slot_buy_count_9 = ConvertBigIntegerToString(slot_buy_count_9);
        YandexGame.savesData.slot_buy_count_10 = ConvertBigIntegerToString(slot_buy_count_10);
        YandexGame.savesData.slot_buy_count_11 = ConvertBigIntegerToString(slot_buy_count_11);
        YandexGame.savesData.slot_buy_count_12 = ConvertBigIntegerToString(slot_buy_count_12);
        YandexGame.savesData.slot_price_1 = ConvertBigIntegerToString(slot_price_1);
        YandexGame.savesData.slot_price_2 = ConvertBigIntegerToString(slot_price_2);
        YandexGame.savesData.slot_price_3 = ConvertBigIntegerToString(slot_price_3);
        YandexGame.savesData.slot_price_4 = ConvertBigIntegerToString(slot_price_4);
        YandexGame.savesData.slot_price_6 = ConvertBigIntegerToString(slot_price_6);
        YandexGame.savesData.slot_price_7 = ConvertBigIntegerToString(slot_price_7);
        YandexGame.savesData.slot_price_8 = ConvertBigIntegerToString(slot_price_8);
        YandexGame.savesData.slot_price_9 = ConvertBigIntegerToString(slot_price_9);
        YandexGame.savesData.slot_price_10 = ConvertBigIntegerToString(slot_price_10);
        YandexGame.savesData.slot_price_11 = ConvertBigIntegerToString(slot_price_11);
        YandexGame.savesData.slot_price_12 = ConvertBigIntegerToString(slot_price_12);
    }
    public string ConvertBigIntegerToString(BigInteger bigIntegerValue)
    {
        string stringValue = bigIntegerValue.ToString();

        return stringValue;
    }

    private BigInteger ConvertStringToBigInteger(string stringValue)
    {
        BigInteger bigIntegerValue;

        if (BigInteger.TryParse(stringValue, out bigIntegerValue))
        {
            return bigIntegerValue;
        }
        else
        {
            return BigInteger.Zero;
        }
    }

    public void ProgressUp(float progress)
    {
        if(YandexGame.savesData.current_comp != 19)
        {
            YandexGame.savesData.currentProgress += progress;
            YandexGame.savesData.currentProgress = Mathf.Clamp(YandexGame.savesData.currentProgress, 0, 100);
            progressImage.fillAmount = YandexGame.savesData.currentProgress / 100f;
            progressPercent.text = Mathf.RoundToInt(progressImage.fillAmount * 100) + "%";
            if (YandexGame.savesData.currentProgress == 100f)
            {
                if(isALreadyBuyShow == false)
                {
                    audio_source.PlayOneShot(new_level_can_snd); 
                    YandexGame.savesData.isBuyBtnShow = true;
                    isALreadyBuyShow = true;

                    showBuyLevel();
                }
            }
        }
    }
    private void showBuyLevel()
    {
        string price2 = "7500";
        string price3 = "20000";
        string price4 = "50000";
        string price5 = "75000";
        string price6 = "100000";
        string price7 = "250000";
        string price8 = "500000";
        string price9 = "750000";
        string price10 = "1000000";
        string price11 = "1250000";
        string price12 = "1500000";
        string price13 = "1750000";
        string price14 = "2000000";
        string price15 = "2250000";
        string price16 = "2500000";
        string price17 = "2750000";
        string price18 = "3000000";
        string price19 = "4500000";

        switch (YandexGame.savesData.current_comp)
        {
            case 1:
                //2
                new_level_price_num = ConvertStringToBigInteger(price2);
                background.sprite = backsSprites[0];
                break;
            case 2:
                //3
                new_level_price_num = ConvertStringToBigInteger(price3);
                background.sprite = backsSprites[0];
                break;
            case 3:
                //4
                new_level_price_num = ConvertStringToBigInteger(price4);
                background.sprite = backsSprites[0];
                break;
            case 4:
                //5
                new_level_price_num = ConvertStringToBigInteger(price5);
                background.sprite = backsSprites[1];
                break;
            case 5:
                //6
                new_level_price_num = ConvertStringToBigInteger(price6);
                background.sprite = backsSprites[1];
                break;
            case 6:
                //7
                new_level_price_num = ConvertStringToBigInteger(price7);
                background.sprite = backsSprites[1];
                break;
            case 7:
                //8
                new_level_price_num = ConvertStringToBigInteger(price8);
                background.sprite = backsSprites[1];
                break;
            case 8:
                //9
                new_level_price_num = ConvertStringToBigInteger(price9);
                background.sprite = backsSprites[1];
                break;
            case 9:
                //10
                new_level_price_num = ConvertStringToBigInteger(price10);
                background.sprite = backsSprites[2];
                break;
            case 10:
                //11
                new_level_price_num = ConvertStringToBigInteger(price11);
                background.sprite = backsSprites[2];
                break;
            case 11:
                //12
                new_level_price_num = ConvertStringToBigInteger(price12);
                background.sprite = backsSprites[2];
                break;
            case 12:
                //13
                new_level_price_num = ConvertStringToBigInteger(price13);
                background.sprite = backsSprites[2];
                break;
            case 13:
                //14
                new_level_price_num = ConvertStringToBigInteger(price14);
                background.sprite = backsSprites[3];
                break;
            case 14:
                //15
                new_level_price_num = ConvertStringToBigInteger(price15);
                background.sprite = backsSprites[3];
                break;
            case 15:
                //16
                new_level_price_num = ConvertStringToBigInteger(price16);
                background.sprite = backsSprites[3];
                break;
            case 16:
                //17
                new_level_price_num = ConvertStringToBigInteger(price17);
                background.sprite = backsSprites[3];
                break;
            case 17:
                //18
                new_level_price_num = ConvertStringToBigInteger(price18);
                background.sprite = backsSprites[3];
                break;
            case 18:
                //19
                new_level_price_num = ConvertStringToBigInteger(price19);
                background.sprite = backsSprites[3];
                break;
        }
        progressPercent.gameObject.SetActive(false);
        new_levelOBJ.SetActive(true);
        YandexGame.SaveProgress();
    }
    public void hideBuyLevel()
    {
        if(YandexGame.savesData.current_comp < 19)
        {
            if (bitcoins >= new_level_price_num)
            {
                StartCoroutine(StartSunScaleAndRotate());
                YandexGame.savesData.isBuyBtnShow = false;
                new_levelOBJ.SetActive(false);
                progressPercent.text = "0%";
                progressImage.fillAmount = 0f;
                YandexGame.savesData.current_comp += 1;
                YandexGame.savesData.currentProgress = 0f;
                audio_source.PlayOneShot(newComp);
                YandexGame.NewLeaderboardScores("complevel", YandexGame.savesData.current_comp);
                bitcoins -= new_level_price_num;
                progressPercent.gameObject.SetActive(true);
                foreach (Transform child in lbParent)
                {
                    Destroy(child.gameObject);
                }
                Instantiate(leaderBoard, lbParent);
                Update_comp();
                YandexGame.SaveProgress();
                isALreadyBuyShow = false;
                YandexGame.savesData.ad_showen = 0;
                ad_showen_text.text = YandexGame.savesData.ad_showen + "/4";
                forbiddenImg.SetActive(false);

                string newLevelText = "Коплимент";

                switch (YandexGame.savesData.current_comp)
                {
                    case 1:
                        if (YandexGame.lang == "ru")
                            newLevelText = "Ты просто молодец!";
                        if (YandexGame.lang == "en")
                            newLevelText = "You're doing great!";
                        if (YandexGame.lang == "tr")
                            newLevelText = "Harika gidiyorsun!";
                        break;
                    case 2:
                        if (YandexGame.lang == "ru")
                            newLevelText = "Ты лучший! Продолжай тапать!";
                        if (YandexGame.lang == "en")
                            newLevelText = "You are the best! Keep tapping!";
                        if (YandexGame.lang == "tr")
                            newLevelText = "Sen en iyisisin! Vurmaya devam et!";
                        break;
                    case 3:
                        if (YandexGame.lang == "ru")
                            newLevelText = "Знай, что ты лучший тапатель!";
                        if (YandexGame.lang == "en")
                            newLevelText = "Know that you are the best learner!";
                        if (YandexGame.lang == "tr")
                            newLevelText = "En iyi öğrenen olduğunu bilsen!";
                        break;
                    case 4:
                        if (YandexGame.lang == "ru")
                            newLevelText = "Ты очень умный и талантливый!";
                        if (YandexGame.lang == "en")
                            newLevelText = "You are very smart and talented!";
                        if (YandexGame.lang == "tr")
                            newLevelText = "Sen çok zeki ve yeteneklisin!";
                        break;
                    case 5:
                        if (YandexGame.lang == "ru")
                            newLevelText = "Ты супер! Помни это!";
                        if (YandexGame.lang == "en")
                            newLevelText = "You are super! Remember that!";
                        if (YandexGame.lang == "tr")
                            newLevelText = "Süpersin! Bunu unutma!";
                        break;
                    case 6:
                        if (YandexGame.lang == "ru")
                            newLevelText = "Молодчинка!";
                        if (YandexGame.lang == "en")
                            newLevelText = "Well done!";
                        if (YandexGame.lang == "tr")
                            newLevelText = "Bravo!";
                        break;
                    case 7:
                        if (YandexGame.lang == "ru")
                            newLevelText = "Гений!";
                        if (YandexGame.lang == "en")
                            newLevelText = "Genius!";
                        if (YandexGame.lang == "tr")
                            newLevelText = "Deha!";
                        break;
                    case 8:
                        if (YandexGame.lang == "ru")
                            newLevelText = "Талантище!";
                        if (YandexGame.lang == "en")
                            newLevelText = "Talented one!";
                        if (YandexGame.lang == "tr")
                            newLevelText = "Yetenekli kişi!";
                        break;
                    case 9:
                        if (YandexGame.lang == "ru")
                            newLevelText = "Гигант!";
                        if (YandexGame.lang == "en")
                            newLevelText = "Giant!";
                        if (YandexGame.lang == "tr")
                            newLevelText = "Dev!";
                        break;
                    case 10:
                        if (YandexGame.lang == "ru")
                            newLevelText = "У тебя то палец не опух?! Силач!";
                        if (YandexGame.lang == "en")
                            newLevelText = "Is your finger not swollen yet?! Strong one!";
                        if (YandexGame.lang == "tr")
                            newLevelText = "Parmakların hâlâ şişmedi mi?! Güçlüsün!";
                        break;
                    case 11:
                        if (YandexGame.lang == "ru")
                            newLevelText = "Не сдавайся! Осталось ещё чуть-чуть, чтобы построить галактическую кофейню!";
                        if (YandexGame.lang == "en")
                            newLevelText = "Don't give up! It's just a little more to build a galactic coffee shop!";
                        if (YandexGame.lang == "tr")
                            newLevelText = "Pes etme! Galaksi kahve dükkanını inşa etmeye biraz daha kaldı!";
                        break;
                    case 12:
                        if (YandexGame.lang == "ru")
                            newLevelText = "Ты близок к победе.\nНе сдавайся!";
                        if (YandexGame.lang == "en")
                            newLevelText = "You are close to victory. Don't give up!";
                        if (YandexGame.lang == "tr")
                            newLevelText = "Zafer çok yakın. Pes etme!";
                        break;
                    case 13:
                        if (YandexGame.lang == "ru")
                            newLevelText = "Давай, ты лучший!";
                        if (YandexGame.lang == "en")
                            newLevelText = "Go on, you are the best!";
                        if (YandexGame.lang == "tr")
                            newLevelText = "Haydi, sen en iyisisin!";
                        break;
                    case 14:
                        if (YandexGame.lang == "ru")
                            newLevelText = "Давай! Давай! Тапай!";
                        if (YandexGame.lang == "en")
                            newLevelText = "Let's go! Let's go! Tap!";
                        if (YandexGame.lang == "tr")
                            newLevelText = "Hadi! Hadi! Dokun!";
                        break;
                    case 15:
                        if (YandexGame.lang == "ru")
                            newLevelText = "Победа близка!";
                        if (YandexGame.lang == "en")
                            newLevelText = "Victory is near!";
                        if (YandexGame.lang == "tr")
                            newLevelText = "Zafer yakın!";
                        break;
                    case 16:
                        if (YandexGame.lang == "ru")
                            newLevelText = "Ещё чуток. Подножми!";
                        if (YandexGame.lang == "en")
                            newLevelText = "Just a little more. Push on!";
                        if (YandexGame.lang == "tr")
                            newLevelText = "Biraz daha. Devam et!";
                        break;
                    case 17:
                        if (YandexGame.lang == "ru")
                            newLevelText = "Последние шаги!";
                        if (YandexGame.lang == "en")
                            newLevelText = "The final steps!";
                        if (YandexGame.lang == "tr")
                            newLevelText = "Son adımlar!";
                        break;
                    case 18:
                        if (YandexGame.lang == "ru")
                            newLevelText = "Один шаг до победы!!";
                        if (YandexGame.lang == "en")
                            newLevelText = "One step to victory!!";
                        if (YandexGame.lang == "tr")
                            newLevelText = "Zafer'e bir adım daha!!";
                        break;
                    case 19:
                        if (YandexGame.lang == "ru")
                            newLevelText = "Победа! Ты стал кофейным магнатом! Поздравляю!!";
                        if (YandexGame.lang == "en")
                            newLevelText = "Victory! You have become a coffee magnate! Congratulations!!";
                        if (YandexGame.lang == "tr")
                            newLevelText = "Zafer! Artık bir kahve milyonerisin! Tebrikler!!";
                        break;
                    default:
                        if (YandexGame.lang == "ru")
                            newLevelText = "Ты просто молодец!";
                        if (YandexGame.lang == "en")
                            newLevelText = "You're doing great!";
                        if (YandexGame.lang == "tr")
                            newLevelText = "Harika gidiyorsun!";
                        break;
                }

                UnityEngine.Vector3 centerPos = new UnityEngine.Vector3(0, 0, 0);
                GameObject instantiatedObject = Instantiate(textPrefabNewLevel, centerPos, UnityEngine.Quaternion.identity, textClickParent);

                Text click_num_fade = instantiatedObject.transform.Find("text").GetComponent<Text>();
                click_num_fade.text = newLevelText;
            }
            else
            {
                audio_source.PlayOneShot(noMoney);
                StartBlinkingNoMoney();
            }
        }
    }
    private IEnumerator StartSunScaleAndRotate()
    {
        allowRotate = true;
        float duration = 2f;
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            sunImage.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
            sunImage.transform.localScale = UnityEngine.Vector3.Lerp(UnityEngine.Vector3.one, UnityEngine.Vector3.one * targetScale, t);
            sunImage.transform.Rotate(UnityEngine.Vector3.forward * 100 * Time.deltaTime);
            yield return null;
        }

        duration = 2f;
        startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            sunImage.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
            yield return null;
        }
        allowRotate = false;
    }

    private void UpdateRank()
    {
        BigInteger generalPlus = slot_buy_count_1 + slot_buy_count_2 + slot_buy_count_3 + slot_buy_count_4 + slot_buy_count_5 +
            slot_buy_count_6 + slot_buy_count_7 + slot_buy_count_8 + slot_buy_count_9 + slot_buy_count_10 + slot_buy_count_11 +
            slot_buy_count_12;

        switch (generalPlus)
        {
            case var _ when generalPlus >= 1750:
                rankText.text = rank12;
                break;
            case var _ when generalPlus >= 1500:
                rankText.text = rank11;
                break;
            case var _ when generalPlus >= 1250:
                rankText.text = rank10;
                break;
            case var _ when generalPlus >= 1000:
                rankText.text = rank9;
                break;
            case var _ when generalPlus >= 750:
                rankText.text = rank8;
                break;
            case var _ when generalPlus >= 500:
                rankText.text = rank7;
                break;
            case var _ when generalPlus >= 350:
                rankText.text = rank6;
                break;
            case var _ when generalPlus >= 200:
                rankText.text = rank5;
                break;
            case var _ when generalPlus >= 100:
                rankText.text = rank4;
                break;
            case var _ when generalPlus >= 50:
                rankText.text = rank3;
                break;
            case var _ when generalPlus >= 15:
                rankText.text = rank2;
                break;
            default:
                rankText.text = rank1;
                break;
        }
    }

    public void On_onOffObjs()
    {
        foreach(Button btns in buttonsOnOff)
        {
            btns.enabled = true;
        }
        foreach (Collider2D clds in collidersOnOff)
        {
            clds.enabled = true;
        }
    }
    public void Off_onOffObjs()
    {
        foreach (Button btns in buttonsOnOff)
        {
            btns.enabled = false;
        }
        foreach (Collider2D clds in collidersOnOff)
        {
            clds.enabled = false;
        }
    }
}
