namespace Codefarts.UIControls
{
    public abstract class Brush
    {
        public virtual float Opacity { get; set; }

        public Brush()
        {
            this.Opacity = 1;
        }
    }
}
