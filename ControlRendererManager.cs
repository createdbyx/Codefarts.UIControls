namespace Codefarts.UIControls
{
    using System;
    using System.Collections.Generic;

    using Codefarts.UIControls.Interfaces;

    using UnityEngine;

    public class ControlRendererManager : IDisposable
    {
        private static ControlRendererManager singleton;

        public static ControlRendererManager Instance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new ControlRendererManager();
                }

                return singleton;
            }
        }

        public GUISkin Skin { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlRendererManager"/> class.
        /// </summary>
        public ControlRendererManager()
        {
            this.controlRenderers = new Dictionary<Type, IControlRenderer>();
            this.MaximumNesting = 100000;
            this.LoadControlRendererPlugins();
        }

        /// <summary>
        /// Holds the list of editor tools.
        /// </summary>
        private IDictionary<Type, IControlRenderer> controlRenderers;

        public Type[] GetControlTypes()
        {
            var keys = new Type[this.controlRenderers.Count];
            this.controlRenderers.Keys.CopyTo(keys, 0);
            return keys;
        }

        public IControlRenderer Get(Type controlType)
        {
            return this.controlRenderers[controlType];
        }

        public int Count
        {
            get
            {
                return this.controlRenderers.Count;
            }
        }

        /// <summary>
        /// Unloads all previously loaded plugins.
        /// </summary>
        private void UnloadPlugins()
        {
            lock (this.controlRenderers)
            {
                this.controlRenderers.Clear();
            }
        }

        /// <summary>
        /// Loads all the tool plugins using reflection.
        /// </summary>
        private void LoadControlRendererPlugins()
        {
            // search for types in each assembly that implement the IEditorTool interface
            var list = new List<IControlRenderer>();
            var fullName = typeof(IControlRenderer).FullName;
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in asm.GetTypes())
                {
                    if (type.IsAbstract)
                    {
                        continue;
                    }

                    foreach (var inter in type.GetInterfaces())
                    {
                        try
                        {
                            if (string.CompareOrdinal(inter.FullName, fullName) == 0)
                            {

                                var obj = asm.CreateInstance(type.FullName);
                                var instance = obj as IControlRenderer;
                                list.Add(instance);
                            }
                        }
                        catch (Exception ex)
                        {
                            // ignore error
                            Debug.LogError(string.Format("Problem loading '{0}' as a control renderer plugin.", type.FullName));
                            Debug.LogException(ex);
                        }
                    }
                }
            }

            foreach (var renderer in list)
            {
                try
                {
                    this.controlRenderers.Add(renderer.ControlType, renderer);
                }
                catch (Exception ex)
                {
                    // ignore error
                    Debug.LogError(string.Format("Problem adding '{0}' another renderer may have already been added.", renderer.ControlType.FullName));
                    Debug.LogException(ex);
                }
            }
        }

        public void Dispose()
        {
            try
            {
                this.UnloadPlugins();
            }
            catch (Exception ex)                            
            {
                Debug.LogError(ex);
            }
        }

        public int MaximumNesting
        {
            get
            {
                return this.maximumNesting;
            }

            set
            {
                this.maximumNesting = value < 0 ? 0 : value;
            }
        }

        private int drawingNestingCount;
        private int updateNestingCount;

        private int maximumNesting;

        public void DrawControl(Control control, float elapsedGameTime, float totalGameTime)
        {
            if (control == null)
            {
                return;
            }

            IControlRenderer renderer = null;
            lock (this.controlRenderers)
            {
                if (this.controlRenderers.ContainsKey(control.GetType()))
                {
                    renderer = this.controlRenderers[control.GetType()];
                }
            }                                                                         

            if (renderer != null)
            {
                GUI.skin = this.Skin;
                if (this.drawingNestingCount + 1 > this.MaximumNesting)
                {
                    this.drawingNestingCount = 0;
                    throw new Exception("Exceeded maximum draw nesting count!");
                }

                try
                {
                    this.drawingNestingCount++;
                    renderer.Draw(this, control, elapsedGameTime, totalGameTime);
                }
                finally
                {
                    this.drawingNestingCount--;
                }
            }
            else
            {
                if (control is ICustomRendering)
                {
                    var custom = control as ICustomRendering;
                    custom.Draw(this, control, elapsedGameTime, totalGameTime);
                }
            }
        }

        public void UpdateControl(Control control, float elapsedGameTime, float totalGameTime)
        {
            if (control == null)
            {
                return;
            }

            IControlRenderer renderer = null;
            lock (this.controlRenderers)
            {
                if (this.controlRenderers.ContainsKey(control.GetType()))
                {
                    renderer = this.controlRenderers[control.GetType()];
                }
            }

            if (renderer != null)
            {
                if (this.updateNestingCount + 1 > this.MaximumNesting)
                {
                    this.updateNestingCount = 0;
                    throw new Exception("Exceeded maximum update nesting count!");
                }

                try
                {
                    this.updateNestingCount++;
                    renderer.Update(this, control, elapsedGameTime, totalGameTime);
                }
                finally
                {
                    this.updateNestingCount--;
                }
            }
            else
            {
                if (control is ICustomRendering)
                {
                    var custom = control as ICustomRendering;
                    custom.Update(this, control, elapsedGameTime, totalGameTime);
                }
            }
        }
    }
}