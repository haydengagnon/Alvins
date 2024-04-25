using System;
using System.Collections.Specialized;
using Anna.Request;

namespace server.app {
    internal class getLanguageStrings : RequestHandler {
        public override void HandleRequest(RequestContext context, NameValueCollection query) {
            var lang = query["languageType"];
            if (!Program.Resources.Languages.TryGetValue(lang, out var response)) {
                Write(context, Program.Resources.Languages["en"], true);
                return;
            }

            Write(context, response, true);
        }
    }
}
