import React, { useEffect, useState } from "react";
import { getAllReservations, createReservation, updateReservation, deleteReservation } from "../api/reservations";
import { getAllRooms } from "../api/meetingRooms";
import { getAllReservers } from "../api/reservers";
import ReservationForm from "../components/ReservationForm";
import * as css from "../css/reservation.css"

function ReservationsPage() {
  const [reservations, setReservations] = useState([]);
  const [rooms, setRooms] = useState([]);
  const [reservers, setReservers] = useState([]);
  const [editingReservation, setEditingReservation] = useState(null);

  const loadData = async () => {
    const resReservations = await getAllReservations();
    const resRooms = await getAllRooms();
    const resReservers = await getAllReservers();
    
    setReservations(resReservations.data);
    setRooms(resRooms.data);
    setReservers(resReservers.data);
  };

  useEffect(() => {
    loadData();
  }, []);

  const handleCreate = async (reservation) => {
    await createReservation(reservation);
    setEditingReservation(null);
    loadData();
  };

  const handleUpdate = async (reservation) => {
    await updateReservation(reservation.revRoomMet, reservation);
    setEditingReservation(null);
    loadData();
  };

  const handleDelete = async (id) => {
    if (window.confirm("Delete this reservation?")) {
      await deleteReservation(id);
      loadData();
    }
  };

  return (
    <div className="reservations-container">
      <h2 className="reservations-title">Reservations</h2>
  
      <ReservationForm
        onSubmit={editingReservation ? handleUpdate : handleCreate}
        initialData={editingReservation}
        onCancel={() => setEditingReservation(null)}
        existingReservations={reservations}
        reservers={reservers} // Pass available reservers
      />
  
      <table className="reservations-table">
        <thead>
          <tr>
            <th>Room</th>
            <th>Reserver</th>
            <th>Start</th>
            <th>End</th>
            <th>Reason</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {reservations.map((r) => {
            const room = rooms.find((room) => room.roomRevNr === r.roomRevNr);
            const reserver = reservers.find((reserver) => reserver.revNr === r.revNr);
  
            return (
              <tr key={r.revRoomMet}>
                <td>{room ? room.roomName : "Unknown Room"}</td>
                <td>{reserver ? reserver.name : "Unknown Reserver"}</td>
                <td>{new Date(r.startTime).toLocaleString()}</td>
                <td>{new Date(r.endTime).toLocaleString()}</td>
                <td>{r.reason}</td>
                <td className="actions-column">
                  <button className="edit-button" onClick={() => setEditingReservation(r)}>✏️</button>
                  <button className="delete-button" onClick={() => handleDelete(r.revRoomMet)}>🗑️</button>
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
}  

export default ReservationsPage;
