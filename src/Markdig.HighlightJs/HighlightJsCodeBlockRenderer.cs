using System.IO;
using System.Text;
using Markdig.Parsers;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace Markdig.HighlightJs
{
    public class HighlightJsCodeBlockRenderer : HtmlObjectRenderer<CodeBlock>
    {
        private readonly CodeBlockRenderer _underlyingRenderer;

        public HighlightJsCodeBlockRenderer(CodeBlockRenderer underlyingRenderer = null)
        {
            _underlyingRenderer = underlyingRenderer ?? new CodeBlockRenderer();
        }

        protected override void Write(HtmlRenderer renderer, CodeBlock obj)
        {
            renderer.EnsureLine();

            var fencedCodeBlock = obj as FencedCodeBlock;
            var language = fencedCodeBlock?.Info;

            if (renderer.EnableHtmlForBlock)
            {
                renderer.Write("<pre><code");
                renderer.WriteAttributes(obj);
                renderer.Write(">");
            }

            var code = GetCode(obj);
            renderer.Write(code);

            var engine = new Jurassic.ScriptEngine();
            engine.ExecuteFile("/Users/pknopf/git/Markdig.HighlightJs/src/Markdig.HighlightJs/Resources/main.js");
            var r = engine.Evaluate<string>("highlight('csharp', 'public class Test { }')");
            //var rs = jint.Invoke("test1", "test2");

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