import React, { useEffect, useState } from "react";
import { getAllRooms, createRoom, updateRoom, deleteRoom } from "../api/meetingRooms";
import MeetingRoomForm from "../components/MeetingRoomForm";
import * as css from "../css/meetingRooms.css"

function MeetingRoomsPage() {
  const [rooms, setRooms] = useState([]);
  const [editingRoom, setEditingRoom] = useState(null);

  const loadRooms = async () => {
    const res = await getAllRooms();
    setRooms(res.data);
  };

  useEffect(() => {
    loadRooms();
  }, []);

  const handleCreate = async (room) => {
    await createRoom(room);
    setEditingRoom(null);
    loadRooms();
  };

  const handleUpdate = async (room) => {
    await updateRoom(room.roomRevNr, room);
    setEditingRoom(null);
    loadRooms();
  };

  const handleDelete = async (id) => {
    if (window.confirm("Delete this room?")) {
      await deleteRoom(id);
      loadRooms();
    }
  };

  return (
    <div className="meeting-container">
      <h2 className="meeting-title">Meeting Rooms</h2>
      
      <MeetingRoomForm
        onSubmit={editingRoom ? handleUpdate : handleCreate}
        initialData={editingRoom}
        onCancel={() => setEditingRoom(null)}
      />
  
      <table className="meeting-table">
        <thead>
          <tr>
            <th>Name</th>
            <th>Capacity</th>
            <th>Available</th>
            <th>Equipment</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {rooms.map((room) => (
            <tr key={room.roomRevNr} className="meeting-row">
              <td className="meeting-name">{room.roomName}</td>
              <td className="meeting-capacity">{room.capacity}</td>
              <td className={`meeting-availability ${room.availability ? "available" : "not-available"}`}>
                {room.availability ? "✅" : "❌"}
              </td>
              <td className="meeting-equipment">
                {room.equipment?.map((eq, i) => (
                  <div key={i}>{eq.name} ({eq.count})</div>
                ))}
              </td>
              <td className="meeting-actions">
                <button className="edit-btn" onClick={() => setEditingRoom(room)}>✏️</button>
                <button className="delete-btn" onClick={() => handleDelete(room.roomRevNr)}>🗑️</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}  

export default MeetingRoomsPage;
