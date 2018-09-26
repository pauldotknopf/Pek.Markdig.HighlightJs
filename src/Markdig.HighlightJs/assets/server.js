import hljs from 'highlight.js';
import hljsqml from 'highlight.js/lib/languages/qml';
import hljscpp from 'highlight.js/lib/languages/cpp';
import hljscs from 'highlight.js/lib/languages/cs';
import hljsjavascript from 'highlight.js/lib/languages/javascript';

hljs.registerLanguage('qml', hljsqml);
hljs.registerLanguage('cpp', hljscpp);
hljs.registerLanguage('cs', hljscs);
hljs.registerLanguage('csharp', hljscs);
hljs.registerLanguage('js', hljsjavascript);
hljs.registerLanguage('javascript', hljsjavascript);

export function highlight(name, value) {
    return hljs.highlight(name, value).value;
}