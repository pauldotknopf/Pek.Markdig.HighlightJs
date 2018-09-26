namespace Markdig.HighlightJs
{
    public static class HighlightJsExtensions
    {
        public static MarkdownPipelineBuilder UseHighlightJs(this MarkdownPipelineBuilder pipeline)
        {
            pipeline.Extensions.Add(new HighlightJsExtension());
            return pipeline;
        }
    }
}