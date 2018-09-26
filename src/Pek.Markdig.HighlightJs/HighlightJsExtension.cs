using Markdig;
using Markdig.Renderers;
using Markdig.Renderers.Html;

namespace Pek.Markdig.HighlightJs
{
    public class HighlightJsExtension : IMarkdownExtension
    {
        private readonly IHighlightJsEngine _highlightJsEngine;

        public HighlightJsExtension(IHighlightJsEngine highlightJsEngine)
        {
            _highlightJsEngine = highlightJsEngine;
        }
        
        public void Setup(MarkdownPipelineBuilder pipeline)
        {
            
        }

        public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
        {
            var htmlRenderer = renderer as TextRendererBase<HtmlRenderer>;
            if (htmlRenderer == null)
            {
                return;
            }

            var originalCodeBlockRenderer = htmlRenderer.ObjectRenderers.FindExact<CodeBlockRenderer>();
            if (originalCodeBlockRenderer != null)
            {
                htmlRenderer.ObjectRenderers.Remove(originalCodeBlockRenderer);
            }

            htmlRenderer.ObjectRenderers.AddIfNotAlready(new HighlightJsCodeBlockRenderer(_highlightJsEngine, originalCodeBlockRenderer));
        }
    }
}