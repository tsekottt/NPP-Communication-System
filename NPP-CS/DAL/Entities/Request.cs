﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Request
    {
        public int Id { get; set; }
        public int UserId { get; set; }           
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } = "new"; 
        public DateTime CreatedAt { get; set; }
    }
}
