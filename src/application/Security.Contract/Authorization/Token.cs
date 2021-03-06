using System;

namespace Super.Paula.Authorization
{
    public class Token
    {
        public string? Identity { get; set; }
        public string? Proof { get; set; }

        public string? Organization { get; set; }
        public string? Inspector { get; set; }

        public string? ImpersonatorOrganization { get; set; }
        public string? ImpersonatorInspector { get; set; }

        public string[] Authorizations { get; set; } = Array.Empty<string>();
    }
}
