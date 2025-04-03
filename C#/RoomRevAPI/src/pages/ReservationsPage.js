import React, { useEffect, useState } from "react";
import { getAllReservations, createReservation, updateReservation, deleteReservation } from "../api/reservations";
import ReservationForm from "../components/ReservationForm";

function ReservationsPage() {
  const [reservations, setReservations] = useState([]);
  const [editingReservation, setEditingReservation] = useState(null);

  const loadReservations = async () => {
    const res = await getAllReservations();
    setReservations(res.data);
  };

  useEffect(() => {
    loadReservations();
  }, []);

  const handleCreate = async (reservation) => {
    await createReservation(reservation);
    setEditingReservation(null);
    loadReservations();
  };

  const handleUpdate = async (reservation) => {
    await updateReservation(reservation.revRoomMet, reservation);
    setEditingReservation(null);
    loadReservations();
  };

  const handleDelete = async (id) => {
    if (window.confirm("Delete this reservation?")) {
      await deleteReservation(id);
      loadReservations();
    }
  };

  return (
    <div>
      <h2>Reservations</h2>

      <ReservationForm
        onSubmit={editingReservation ? handleUpdate : handleCreate}
        initialData={editingReservation}
        onCancel={() => setEditingReservation(null)}
        existingReservations={reservations}
      />

      <table border="1" cellPadding="8">
        <thead>
          <tr>
            <th>Room ID</th>
            <th>Reserver ID</th>
            <th>Start</th>
            <th>End</th>
            <th>Reason</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {reservations.map((r) => (
            <tr key={r.revRoomMet}>
              <td>{r.roomRevNr}</td>
              <td>{r.revNr}</td>
              <td>{new Date(r.startTime).toLocaleString()}</td>
              <td>{new Date(r.endTime).toLocaleString()}</td>
              <td>{r.reason}</td>
              <td>
                <button onClick={() => setEditingReservation(r)}>✏️</button>
                <button onClick={() => handleDelete(r.revRoomMet)}>🗑️</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ReservationsPage;
