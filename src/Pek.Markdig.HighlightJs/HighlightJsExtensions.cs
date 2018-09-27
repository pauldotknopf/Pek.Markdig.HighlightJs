using Markdig;

namespace Pek.Markdig.HighlightJs
{
    public static class HighlightJsExtensions
    {
        private static IHighlightJsEngine _sharedEngine;
        private static readonly object Lock = new object();
        
        public static MarkdownPipelineBuilder UseHighlightJs(this MarkdownPipelineBuilder pipeline, IHighlightJsEngine highlightJsEngine = null)
        {
            if (highlightJsEngine == null)
            {
                lock (Lock)
                {
                    if (_sharedEngine == null)
                    {
                        _sharedEngine = new HighlightJsEngine();
                    }

                    highlightJsEngine = _sharedEngine;
                }
            }
            pipeline.Extensions.Add(new HighlightJsExtension(highlightJsEngine));
            return pipeline;
        }
    }
}