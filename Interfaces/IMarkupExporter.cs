namespace Codefarts.UIControls.MarkupImporting
{
    using System.IO;

    using Codefarts.UIControls.Models;

    /// <summary>
    /// Provides a interface for markup exporter plugins.
    /// </summary>
    public interface IMarkupExporter : IPlugin
    {
        /// <summary>
        /// Processes the specified markup and writes it to the stream.
        /// </summary>
        /// <param name="markup">The markup data to be processed.</param>
        /// <param name="stream">The stream where the exported data will be written to.</param>
        /// <param name="progress">The progress model that will be used to report progress.</param>
        void Process(Markup markup, Stream stream, ProgressModel<Markup> progress);
    }
}