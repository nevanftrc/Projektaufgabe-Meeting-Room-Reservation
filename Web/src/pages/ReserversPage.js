import React, { useEffect, useState } from "react";
import {
  getAllReservers,
  createReserver,
  updateReserver,
  deleteReserver,
} from "../api/reservers";
import ReserverForm from "../components/ReserverForm";
import * as css from "../css/reservers.css"

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
    <div className="container">
      <h2 className="title">Reservers</h2>

      <ReserverForm
        onSubmit={editingReserver ? handleUpdate : handleCreate}
        initialData={editingReserver}
        onCancel={() => setEditingReserver(null)}
      />

      <table className="table">
        <thead>
          <tr>
            <th className="tableHeader">Name</th>
            <th className="tableHeader">Actions</th>
          </tr>
        </thead>
        <tbody>
          {reservers.map((r) => (
            <tr key={r.revNr} className="tableRow">
              <td className="tableCell">{r.name}</td>
              <td className="tableCell">
                <button className="editBtn" onClick={() => setEditingReserver(r)}>✏️</button>
                <button className="deleteBtn" onClick={() => handleDelete(r.revNr)}>🗑️</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ReserversPage;
