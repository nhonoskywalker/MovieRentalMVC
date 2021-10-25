using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRentalAppUI.Models
{
    public class ResponseModel <T>
    {
        public T Data { get; set; }
    }
}
