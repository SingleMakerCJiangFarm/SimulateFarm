using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Erinn
{
    [Serializable]
    public sealed class PanelFader
    {
        public Image[] Images;
        public TMP_Text[] Texts;
        public bool IsFading;

        public void DoFade(float target, float duration)
        {
            if (IsFading)
                return;
            IsFading = true;
            Entry.Timer.Create(duration, () => IsFading = false);
            for (var i = 0; i < Images.Length; ++i)
                Images[i].DOFade(target, duration);
            for (var i = 0; i < Texts.Length; ++i)
                Texts[i].DOFade(target, duration);
        }

        [Button]
        public void Fade(float alpha)
        {
            for (var i = 0; i < Images.Length; ++i)
            {
                var color = Images[i].color;
                color.a = alpha;
                Images[i].color = color;
            }

            for (var i = 0; i < Texts.Length; ++i)
            {
                var color = Texts[i].color;
                color.a = alpha;
                Texts[i].color = color;
            }
        }

        [Button]
        public void Find(GameObject gameObject)
        {
            Images = gameObject.GetComponentsInChildren<Image>(true);
            Texts = gameObject.GetComponentsInChildren<TMP_Text>(true);
        }
    }
}