namespace Codefarts.UIControls.MarkupImporting
{
    using System.IO;

    using Codefarts.UIControls.Models;

    /// <summary>
    /// Provides a interface for markup importer plugins.
    /// </summary>
    public interface IImportParser  
    {
        //  IList<IParserPlugin> ParserPlugins { get; set; }

        /// <summary>
        /// Processes the specified stream and generates a Node result.
        /// </summary>
        /// <param name="stream">The stream to be processed.</param>
        /// <param name="progress">The progress model that will be used to report progress.</param>
        void Process(Stream stream, ProgressModel<Markup> progress);
    }
}