using DTOs;
using Enums;
using Logic_Layer.Interface.LL;
using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Services.Planes
{
    public class SeatService : ISeatService
    {
        private readonly IPlaneService _planeService;
        private readonly ITicketService _ticketsService;
        public SeatService(IPlaneService planeService, ITicketService ticketService)
        {
            _planeService = planeService;
            _ticketsService = ticketService;
        }

        public List<Seat> GetAvailableSeatsByFlightID(int flightID)
        {
            List<Seat> availableSeats = new List<Seat>();
            Plane plane = _planeService.GetPlaneByFlightID(flightID);

            foreach (SeatGroup group in plane.SeatGroups)
            {
                for (int i = 0; i < group.Seats.GetLength(0); i++)
                {
                    for (int j = 0; j < group.Seats.GetLength(1); j++)
                    {
                        Seat seat = group.Seats[i, j];
                        if (seat != null && seat.Taken == false)
                        {
                            availableSeats.Add(seat);
                        }
                    }
                }
            }

            availableSeats = availableSeats.OrderBy(seat => seat.Id).ToList();
            return availableSeats;
        }

        public List<Seat> GetAvailableFirstSeatsByFlightID(int flightID)
        {
            List<Seat> seats = new List<Seat>();
            foreach (Seat seat in GetAvailableSeatsByFlightID(flightID))
            {
                if (seat.SeatModel == SeatModel.First)
                {
                    seats.Add(seat);
                }
            }

            seats = seats.OrderBy(seat => seat.Id).ToList();
            return seats;
        }

        public Seat GetAvailableFirstSeatByFlightID(int flightID)
        {
            return GetAvailableFirstSeatsByFlightID(flightID)[0];
        }



        public List<Seat> GetAvailableBusinessSeatsByFlightID(int flightID)
        {
            List<Seat> seats = new List<Seat>();
            foreach (Seat seat in GetAvailableSeatsByFlightID(flightID))
            {
                if (seat.SeatModel == SeatModel.Business)
                {
                    seats.Add(seat);
                }
            }
            seats = seats.OrderBy(seat => seat.Id).ToList();
            return seats;
        }

        public Seat GetAvailableBusinessSeatByFlightID(int flightID)
        {
            return GetAvailableBusinessSeatsByFlightID(flightID)[0];
        }


        public List<Seat> GetAvailableEconomySeatsByFlightID(int flightID)
        {
            List<Seat> seats = new List<Seat>();
            foreach (Seat seat in GetAvailableSeatsByFlightID(flightID))
            {
                if (seat.SeatModel == SeatModel.Economy)
                {
                    seats.Add(seat);
                }
            }
            seats = seats.OrderBy(seat => seat.Id).ToList();
            return seats;
        }

        public Seat GetAvailableEconomySeatByFlightID(int flightID)
        {
            return GetAvailableEconomySeatsByFlightID(flightID)[0];
        }


        public List<Seat> GetAllSeatsByFlightID(int flightID)
        {
            List<Seat> allSeats = new List<Seat>();
            Plane plane = _planeService.GetPlaneByFlightID(flightID);

            foreach (SeatGroup group in plane.SeatGroups)
            {
                for (int i = 0; i < group.Seats.GetLength(0); i++)
                {
                    for (int j = 0; j < group.Seats.GetLength(1); j++)
                    {
                        Seat seat = group.Seats[i, j];
                        allSeats.Add(seat);
                    }
                }
            }

            allSeats = allSeats.OrderBy(seat => seat.Id).ToList();
            return allSeats;
        }

        public List<Seat> GetAllSeatsByFlightPlaneDTO(PlaneDTO planeDTO)
        {
            List<Seat> allSeats = new List<Seat>();

            foreach (SeatGroup group in planeDTO.SeatGroups)
            {
                for (int i = 0; i < group.Seats.GetLength(0); i++)
                {
                    for (int j = 0; j < group.Seats.GetLength(1); j++)
                    {
                        Seat seat = group.Seats[i, j];
                        allSeats.Add(seat);
                    }
                }
            }

            allSeats = allSeats.OrderBy(seat => seat.Id).ToList();
            return allSeats;
        }

    

    }
}
