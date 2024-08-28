using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.VFX;

namespace Match3
{
    /// <summary>
    /// Oyunun tüm ayarlarýný içeren sýnýf. Bu ayarlar GameManager üzerinde, Resource klasöründe bulunur ve düzenlenebilir.
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
    /// Görsel efektlerle ilgili ayarlar. Düþme hýzý, zýplama eðrisi ve vfx gibi parametreleri içerir.
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
    /// Bonus taþlarla ilgili ayarlar. Mevcut tüm bonus taþlarý listeler.
    /// </summary>
    [System.Serializable]
    public class BonusSetting
    {
        public BonusGem[] Bonuses;
    }

    /// <summary>
    /// Maðaza ile ilgili ayarlar. Tüm Maðaza Öðelerini listeler.
    /// </summary>
    [System.Serializable]
    public class ShopSetting
    {
        /// <summary>
        /// Maðaza öðelerinin temel sýnýfý. Her öðe için fiyat ve satýn alýnabilirlik bilgilerini içerir.
        /// </summary>
        public abstract class ShopItem : ScriptableObject
        {
            public Sprite ItemSprite;
            public string ItemName;
            public int Price;

            /// <summary>
            /// Öðenin satýn alýnýp alýnamayacaðýný kontrol eder.
            /// </summary>
            public virtual bool CanBeBought()
            {
                return GameManager.Instance.Stars >= Price;
            }

            /// <summary>
            /// Öðeyi satýn alýr. Abstrakt metod, alt sýnýflarda uygulanmalýdýr.
            /// </summary>
            public abstract void Buy();
        }

        public ShopItem[] Items;
    }

    /// <summary>
    /// Ses ayarlarýyla ilgili sýnýf. Ses kaynaklarý, efektler ve müzik ayarlarýný içerir.
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
