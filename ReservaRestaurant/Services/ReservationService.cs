using ReservaRestaurant.Models;
using ReservaRestaurant.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaRestaurant.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            this._reservationRepository = reservationRepository;
        }

        public bool CreateReservation(Reservation reservation)
        {
            try
            {
                // Setear el status
                reservation.Status = ReservationStatus.Pending;

                // Agregamos la reserva
                _reservationRepository.Add(reservation);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public bool UpdateReservation(Reservation reservation)
        {
            try
            {
                _reservationRepository.Update(reservation);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public bool CancelReservation(Reservation reservation)
        {
            var existingReservation = _reservationRepository.GetById(reservation.Id);
            if (existingReservation == null)
                return false;

            existingReservation.Status = ReservationStatus.Cancelled;
            _reservationRepository.Update(existingReservation);

            return true;
        }

        public Reservation GetReservationById(int id)
        {
            return _reservationRepository.GetById(id);
        }

        public List<Reservation> GetReservationsByDate(DateTime date)
        {
            return _reservationRepository.GetReservationsByDate(date);
        }
    }
}
