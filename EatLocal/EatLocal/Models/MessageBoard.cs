using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EatLocal.Models
{
    public class MessageBoard
    {
        [Key]
        public int MessageBoardID { get; set; }
        public DateTime Date { get; set; }
        public string Topic { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }

    }
}
