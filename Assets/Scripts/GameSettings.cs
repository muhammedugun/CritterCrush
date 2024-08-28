using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.VFX;

namespace Match3
{
    /// <summary>
    /// Oyunun t�m ayarlar�n� i�eren s�n�f. Bu ayarlar GameManager �zerinde, Resource klas�r�nde bulunur ve d�zenlenebilir.
    /// </summary>
    [System.Serializable]
    public class GameSettings
    {
        public float InactivityBeforeHint = 2.0f;

        public VisualSetting VisualSettings;
        public BonusSetting BonusSettings;
        public ShopSetting ShopSettings;
        public SoundSetting SoundSettings;
    }

    /// <summary>
    /// G�rsel efektlerle ilgili ayarlar. D��me h�z�, z�plama e�risi ve vfx gibi parametreleri i�erir.
    /// </summary>
    [System.Serializable]
    public class VisualSetting
    {
        public float FallSpeed = 10.0f;
        public AnimationCurve FallAccelerationCurve;
        public AnimationCurve BounceCurve;
        public AnimationCurve SquishCurve;

        public AnimationCurve MatchFlyCurve;
        public AnimationCurve CoinFlyCurve;

        public GameObject BoosterModePrefab;

        public GameObject HintPrefab;

        public VisualEffect WinEffect;
        public VisualEffect LoseEffect;
    }

    /// <summary>
    /// Bonus ta�larla ilgili ayarlar. Mevcut t�m bonus ta�lar� listeler.
    /// </summary>
    [System.Serializable]
    public class BonusSetting
    {
        public BonusGem[] Bonuses;
    }

    /// <summary>
    /// Ma�aza ile ilgili ayarlar. T�m Ma�aza ��elerini listeler.
    /// </summary>
    [System.Serializable]
    public class ShopSetting
    {
        /// <summary>
        /// Ma�aza ��elerinin temel s�n�f�. Her ��e i�in fiyat ve sat�n al�nabilirlik bilgilerini i�erir.
        /// </summary>
        public abstract class ShopItem : ScriptableObject
        {
            public Sprite ItemSprite;
            public string ItemName;
            public int Price;

            /// <summary>
            /// ��enin sat�n al�n�p al�namayaca��n� kontrol eder.
            /// </summary>
            public virtual bool CanBeBought()
            {
                return GameManager.Instance.Stars >= Price;
            }

            /// <summary>
            /// ��eyi sat�n al�r. Abstrakt metod, alt s�n�flarda uygulanmal�d�r.
            /// </summary>
            public abstract void Buy();
        }

        public ShopItem[] Items;
    }

    /// <summary>
    /// Ses ayarlar�yla ilgili s�n�f. Ses kaynaklar�, efektler ve m�zik ayarlar�n� i�erir.
    /// </summary>
    [System.Serializable]
    public class SoundSetting
    {
        public AudioMixer Mixer;

        public AudioSource SFXSourcePrefab;
        public AudioSource MusicSourcePrefab;

        public AudioClip MenuSound;

        public AudioClip SwipSound;
        public AudioClip FallSound;

        public AudioClip WinVoice;
        public AudioClip LooseVoice;

        public AudioClip WinSound;
        public AudioClip LooseSound;
    }
}
