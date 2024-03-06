using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.domain
{
    public class ReservationTicket
    {
        public int Id { get; set; }
        public DateTime DateReservation{ get; set; }
        public float prix { get; set;}
        public Passenger Passenger { get; set; }
        public Ticket Ticket { get; set; } 
    }
}
