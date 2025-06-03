using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums;
using Shared_Classes;

namespace DTOs
{
    public class SeatDTO
    {
        public int Id { get; set; }
        public SeatModel SeatModel { get; set; }
        public int Row { get; set; }
        public char Column { get; set; }
        public bool Taken { get; set; }
    }
}
