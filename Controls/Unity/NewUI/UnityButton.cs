namespace Codefarts.UIControls.Controls.Unity.NewUI
{
    using System;

    using Codefarts.UnityExtensionMethods;

    using UnityEngine;
    using UnityEngine.UI;

    using Button = Codefarts.UIControls.Button;

    public class UnityButton : Button, IDisposable
    {
        protected UnityEngine.UI.Button button;

        protected GameObject gameObject;

        private Image imageComponent;

        private Text textComponent;

        private RectTransform rectTransform;

        private Font font;

        private Color textColor;

        private TextAnchor textAlignment;

        private RectTransform textRectTransform;

        public override Texture Texture
        {
            get
            {
                return base.Texture;
            }

            set
            {
                if (!(value is Texture2D))
                {
                    throw new InvalidCastException("value must be a Texture2D!");
                }

                base.Texture = value;
                //  this.imageComponent.sprite = Sprite.Create(value as Texture2D, new Rect(0, 0, value.width, value.height), Vector2.zero);
            }
        }

        public override float Left
        {
            get
            {
                return base.Left;
            }

            set
            {
                base.Left = value;
                this.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, value, 0);
            }
        }

        public override float Top
        {
            get
            {
                return base.Top;
            }

            set
            {
                base.Top = value;
                this.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, value, 0);
            }
        }

        public override float Width
        {
            get
            {
                return base.Width;
            }

            set
            {
                base.Width = value;
                //   this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value);
                this.SetSize(new Vector2(value, this.rectTransform.rect.size.y));
            }
        }

        private void SetSize(Vector2 newSize)
        {
            var oldSize = this.rectTransform.rect.size;
            var deltaSize = newSize - oldSize;
            this.rectTransform.offsetMin = this.rectTransform.offsetMin - new Vector2(deltaSize.x * this.rectTransform.pivot.x, deltaSize.y * this.rectTransform.pivot.y);
            this.rectTransform.offsetMax = this.rectTransform.offsetMax + new Vector2(deltaSize.x * (1f - this.rectTransform.pivot.x), deltaSize.y * (1f - this.rectTransform.pivot.y));
        }

        public override Visibility Visibility
        {
            get
            {
                return base.Visibility;
            }

            set
            {
                var changed = base.Visibility != value;
                base.Visibility = value;
                if (changed)
                {
                    this.gameObject.SetActive(value == Visibility.Visible);
                }
            }
        }

        public override float Height
        {
            get
            {
                return base.Height;
            }

            set
            {
                base.Height = value;
                //   this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, value);
                this.SetSize(new Vector2(this.rectTransform.rect.size.x, value));
            }
        }


        public override string Text
        {
            get
            {
                return base.Text;
            }

            set
            {
                base.Text = value;
                this.textComponent.text = value;
            }
        }

        public Color TextColor
        {
            get
            {
                return this.textColor;
            }

            set
            {
                this.textColor = value;
                this.textComponent.color = value;
            }
        }

        public Font Font
        {
            get
            {
                return this.font;
            }

            set
            {
                this.font = value;
                this.textComponent.font = value;
            }
        }

        public UnityButton()
            : base()
        {
            this.gameObject = new GameObject("UnityButton") { layer = LayerMask.NameToLayer("UI") };
            this.rectTransform = this.gameObject.AddComponent<RectTransform>();
            this.rectTransform.pivot = Vector2.zero;
            this.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, this.Left, 0);
            this.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, this.Top, 0);
            this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.Width);
            this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.Height);

            this.gameObject.AddComponent<CanvasRenderer>();
            this.imageComponent = this.gameObject.AddComponent<Image>();
            this.imageComponent.type = Image.Type.Sliced;

            Sprite sprite = null;
            foreach (var item in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (item.name == "UISprite")
                {
                    sprite = item;
                    break;
                }
            }

            this.imageComponent.sprite = sprite;
            this.button = this.gameObject.AddComponent<UnityEngine.UI.Button>();
            this.button.onClick.AddListener(this.OnClick);

            var label = new GameObject("Text") { layer = LayerMask.NameToLayer("UI") };
            this.textRectTransform = label.AddComponent<RectTransform>();
            this.textRectTransform.anchorMin = Vector2.zero;
            this.textRectTransform.anchorMax = Vector2.one;

            label.AddComponent<CanvasRenderer>();
            this.textComponent = label.AddComponent<Text>();
            label.transform.SetParent(this.button.transform, false);


            var canvas = GameObject.FindObjectOfType<Canvas>();
            this.gameObject.transform.SetParent(canvas.transform, false);
            this.Font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            this.TextColor = Color.gray;
            this.TextAlignment = TextAnchor.MiddleCenter;
        }

        public TextAnchor TextAlignment
        {
            get
            {
                return this.textAlignment;
            }

            set
            {
                this.textAlignment = value;
                this.textComponent.alignment = value;
            }
        }

        public void Dispose()
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
