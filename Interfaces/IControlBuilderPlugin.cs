namespace Codefarts.UIControls.ControlBuilding
{
    using Codefarts.UIControls.Models;

    //public interface IBuilderPlugin : IPlugin
    //{
    //    void Build(IImportParser parser, Node node, ProgressModel<Control> progress);
    //}

    public interface IControlBuilderPlugin  
    {
        /// <summary>
        /// Gets the names of the markup elements that the implementation is designed to handle.
        /// </summary>
        string[] HandlesElements { get; }

        /// <summary>
        /// Builds the specified node.
        /// </summary>
        /// <param name="node">The node to convert.</param>
        /// <param name="parent">The parent node.</param>
        /// <param name="progress">The progress model that will report progress and return results.</param>
        void Build(Markup node, Control parent, ProgressModel<Control> progress);
    }
}