using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Erinn
{
    public sealed class LoginPanel : MonoBehaviour
    {
        [LabelText("黑色遮罩")] public Image DarkMask;
        [LabelText("登录渐隐")] public PanelFader LoginFader = new();
        [LabelText("输入渐隐")] public PanelFader InputFader = new();
        [LabelText("错误渐隐")] public PanelFader ErrorFader = new();
        [LabelText("输入面板")] public GameObject InputPanel;
        [LabelText("学号输入框")] public TMP_InputField IdInputField;
        [LabelText("姓名输入框")] public TMP_InputField NameInputField;
        [LabelText("登录按钮")] public Button LoginButton;
        [LabelText("错误文本")] public TMP_Text ErrorText;
        [LabelText("错误面板")] public GameObject ErrorPanel;
        [LabelText("错误按钮")] public Button ErrorButton;

        private void Start()
        {
            DarkMask.DOFade(0f, 2f).onComplete += () => DarkMask.gameObject.SetActive(false);
            LoginButton.onClick.AddListener(OnLoginClick);
            ErrorButton.onClick.AddListener(OnErrorClick);
        }

        private void OnLoginClick()
        {
            if (LoginFader.IsFading || InputFader.IsFading)
                return;
            if (!IdInputField.text.TryParse(out int id))
            {
                ShowError("学号无效");
                return;
            }

            if (string.IsNullOrEmpty(NameInputField.text))
            {
                ShowError("姓名不能为空");
                return;
            }

            PlayerData.Id = id;
            PlayerData.Name = NameInputField.text;
            LoginFader.DoFade(0f, 1f);
            Entry.Timer.Create(1.05f, () => Destroy(gameObject));
        }

        private void ShowError(string text)
        {
            ErrorText.text = text;
            InputFader.DoFade(0f, 1f);
            Entry.Timer.Create(1.05f, () =>
            {
                InputPanel.SetActive(false);
                ErrorPanel.SetActive(true);
                ErrorFader.DoFade(1f, 1f);
            });
        }

        private void OnErrorClick()
        {
            if (ErrorFader.IsFading)
                return;
            ErrorFader.DoFade(0f, 1f);
            Entry.Timer.Create(1.05f, () =>
            {
                ErrorPanel.SetActive(false);
                InputPanel.SetActive(true);
                InputFader.DoFade(1f, 1f);
            });
        }
    }
}