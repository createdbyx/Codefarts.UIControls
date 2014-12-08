namespace Codefarts.UIControls.Controls.Unity.NewUI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;

    using UnityEngine;

    public interface IIUnityControl
    {
        GameObject GameObject { get; }
    }

    public class UnityContainerControl : ContainerControl, IDisposable, IIUnityControl
    {
        protected GameObject gameObject;

        private RectTransform rectTransform;

        public override IList<Control> Children
        {
            get;
            set;
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
                this.SetSize(new Vector2(this.rectTransform.rect.size.x, value));
            }
        }


        public UnityContainerControl()
        {
            var observable = new ObservableCollection<Control>();
            this.Children = observable;
            observable.CollectionChanged += this.observable_CollectionChanged;

            this.gameObject = new GameObject("UnityContainerControl") { layer = LayerMask.NameToLayer("UI") };
            this.rectTransform = this.gameObject.AddComponent<RectTransform>();
            this.rectTransform.pivot = Vector2.zero;
            this.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, this.Left, 0);
            this.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, this.Top, 0);
            this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.Width);
            this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.Height);

            this.gameObject.AddComponent<CanvasRenderer>();
        }

        private void observable_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.AddNewItems(e.NewItems);   
                    break;

                case NotifyCollectionChangedAction.Remove:
                    this.RemoveOldItems(e.OldItems);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    this.RemoveOldItems(e.OldItems);
                    this.AddNewItems(e.NewItems);    
                    break;

                case NotifyCollectionChangedAction.Move:
                    break;

                case NotifyCollectionChangedAction.Reset:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void RemoveOldItems(IList items)
        {
            foreach (var item in items)
            {
                var control = item as IIUnityControl;
                if (control == null)
                {
                    continue;
                }

                if (control.GameObject.transform.parent == this.gameObject.transform)
                {
                    control.GameObject.transform.parent = null;
                }
            }
        }

        private void AddNewItems(IList items)
        {
            foreach (var item in items)
            {
                var control = item as IIUnityControl;
                if (control == null)
                {
                    continue;
                }

                control.GameObject.transform.SetParent(this.gameObject.transform, false);
            }
        }

        public void Dispose()
        {
            GameObject.Destroy(this.gameObject);
        }

        public GameObject GameObject
        {
            get
            {
                return this.gameObject;
            }
        }
    }
}