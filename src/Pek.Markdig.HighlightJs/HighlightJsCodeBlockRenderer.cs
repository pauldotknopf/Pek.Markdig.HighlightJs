using System.Text;
using Markdig.Parsers;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace Pek.Markdig.HighlightJs
{
    public class HighlightJsCodeBlockRenderer : HtmlObjectRenderer<CodeBlock>
    {
        private readonly IHighlightJsEngine _highlightJsEngine;
        private readonly CodeBlockRenderer _underlyingRenderer;

        public HighlightJsCodeBlockRenderer(
            IHighlightJsEngine highlightJsEngine,
            CodeBlockRenderer underlyingRenderer = null)
        {
            _highlightJsEngine = highlightJsEngine;
            _underlyingRenderer = underlyingRenderer ?? new CodeBlockRenderer();
        }

        protected override void Write(HtmlRenderer renderer, CodeBlock obj)
        {
            var fencedCodeBlock = obj as FencedCodeBlock;
            var parser = obj.Parser as FencedCodeBlockParser;
            
            if (fencedCodeBlock == null || parser == null)
            {
                _underlyingRenderer.Write(renderer, obj);
                return;
            }

            var languageMoniker = fencedCodeBlock.Info.Replace(parser.InfoPrefix, string.Empty);
            if (string.IsNullOrEmpty(languageMoniker))
            {
                _underlyingRenderer.Write(renderer, obj);
                return;
            }
            
            var attributes = obj.TryGetAttributes() ?? new HtmlAttributes();
            attributes.AddClass("hljs");
            
            if (renderer.EnableHtmlForBlock)
            {
                renderer.Write("<pre><code");
                renderer.WriteAttributes(attributes);
                renderer.Write(">");
            }

            var code = _highlightJsEngine.Run(languageMoniker, GetCode(obj));
            renderer.Write(code);

            
            if (renderer.EnableHtmlForBlock)
            {
                renderer.WriteLine("</code></pre>");
            }
        }

        private string GetCode(CodeBlock obj)
        {
            var code = new StringBuilder();
            string firstLine = null;
            foreach (var line in obj.Lines.Lines)
            {
                var slice = line.Slice;
                if (slice.Text == null)
                {
                    continue;
                }

                var lineText = slice.Text.Substring(slice.Start, slice.Length);

                if (firstLine == null)
                {
                    firstLine = lineText;
                }
                else
                {
                    code.AppendLine();
                }

                code.Append(lineText);
            }
            return code.ToString();
        }
    }
}