using System;
using System.IO;
using Jurassic;

namespace Pek.Markdig.HighlightJs
{
    public class HighlightJsEngine : IHighlightJsEngine
    {
        ScriptEngine _scriptEngine;
        
        public HighlightJsEngine()
        {
            _scriptEngine = new ScriptEngine();
            _scriptEngine.SetGlobalValue("console", new Jurassic.Library.FirebugConsole(_scriptEngine));
            
            var embeddedResource = typeof(HighlightJsEngine).Assembly.GetManifestResourceStream("Pek.Markdig.HighlightJs.Resources.main.js");
            if (embeddedResource == null)
            {
                throw new Exception("Couldn't load embedded main.js");
            }

            string script;
            using (var reader = new StreamReader(embeddedResource))
            {
                script = reader.ReadToEnd();
            }
            
            _scriptEngine.Execute(script);
        }
        
        public string Run(string language, string code)
        {
            if(string.IsNullOrEmpty(language)) throw new ArgumentOutOfRangeException(nameof(language));
            if(string.IsNullOrEmpty(code)) throw new ArgumentOutOfRangeException(nameof(language));

            var tempVar = $"temp{Guid.NewGuid().ToString().Replace("-", "")}";
            _scriptEngine.SetGlobalValue(tempVar, code);

            try
            {
                return _scriptEngine.Evaluate<string>($"highlight('{language}', {tempVar})");
            }
            finally
            {
                _scriptEngine.SetGlobalValue(tempVar, null);
            }
        }
    }
}