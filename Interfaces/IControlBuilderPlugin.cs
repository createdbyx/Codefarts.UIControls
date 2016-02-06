namespace Codefarts.UIControls.ControlBuilding
{
    using Codefarts.UIControls.Models;

    //public interface IBuilderPlugin : IPlugin
    //{
    //    void Build(IImportParser parser, Node node, ProgressModel<Control> progress);
    //}

    public interface IControlBuilderPlugin : IPlugin
    {
        string[] HandlesElements { get; }
        void Build(Markup node, Control parent, ProgressModel<Control> progress);
    }
}