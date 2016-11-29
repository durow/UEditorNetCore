using Microsoft.AspNetCore.Http;

namespace UEditorNetCore.Handlers
{
    public class NotSupportedHandler : Handler
    {
        public NotSupportedHandler(HttpContext context)
            : base(context)
        {
        }

        public override void Process()
        {
            WriteJson(new
            {
                state = "action is empty or action not supperted."
            });
        }
    }
}