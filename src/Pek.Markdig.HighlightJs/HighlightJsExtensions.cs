using Markdig;

namespace Pek.Markdig.HighlightJs
{
    public static class HighlightJsExtensions
    {
        public static MarkdownPipelineBuilder UseHighlightJs(this MarkdownPipelineBuilder pipeline, IHighlightJsEngine highlightJsEngine = null)
        {
            pipeline.Extensions.Add(new HighlightJsExtension(highlightJsEngine ?? new HighlightJsEngine()));
            return pipeline;
        }
    }
}