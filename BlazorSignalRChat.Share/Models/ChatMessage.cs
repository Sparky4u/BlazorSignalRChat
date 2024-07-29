using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSignalRChat.Share.Models
{
    public class ChatMessage
    {        
        public int Id { get; set; }
        public string? Text { get; set; }
        public string? SentimentClass { get; set; }     
    }
}
