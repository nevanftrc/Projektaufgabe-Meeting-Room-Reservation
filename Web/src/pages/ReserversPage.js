import React, { useEffect, useState } from "react";
import {
  getAllReservers,
  createReserver,
  updateReserver,
  deleteReserver,
} from "../api/reservers";
import ReserverForm from "../components/ReserverForm";

function ReserversPage() {
  const [reservers, setReservers] = useState([]);
  const [editingReserver, setEditingReserver] = useState(null);

  const loadReservers = async () => {
    const res = await getAllReservers();
    setReservers(res.data);
  };

  useEffect(() => {
    loadReservers();
  }, []);

  const handleCreate = async (reserver) => {
    await createReserver(reserver);
    setEditingReserver(null);
    loadReservers();
  };

  const handleUpdate = async (reserver) => {
    await updateReserver(reserver.revNr, reserver);
    setEditingReserver(null);
    loadReservers();
  };

  const handleDelete = async (id) => {
    if (window.confirm("Delete this reserver?")) {
      await deleteReserver(id);
      loadReservers();
    }
  };

  return (
    <div>
      <h2>Reservers</h2>

      <ReserverForm
        onSubmit={editingReserver ? handleUpdate : handleCreate}
        initialData={editingReserver}
        onCancel={() => setEditingReserver(null)}
      />

      <table border="1" cellPadding="8">
        <thead>
          <tr>
            <th>Name</th>
            <th>revNr</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {reservers.map((r) => (
            <tr key={r.revNr}>
              <td>{r.name}</td>
              <td>{r.revNr}</td>
              <td>
                <button onClick={() => setEditingReserver(r)}>✏️</button>
                <button onClick={() => handleDelete(r.revNr)}>🗑️</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ReserversPage;
