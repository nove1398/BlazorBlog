using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared.ViewModels
{
    public class ResponseViewModel
    {
        public bool IsValid { get; set; }

        public string Message { get; set; }

        public IEnumerable<string> Erros { get; set; }
    }
}
