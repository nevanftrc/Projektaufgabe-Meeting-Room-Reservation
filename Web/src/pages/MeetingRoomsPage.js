import React, { useEffect, useState } from "react";
import { getAllRooms, createRoom, updateRoom, deleteRoom } from "../api/meetingRooms";
import MeetingRoomForm from "../components/MeetingRoomForm";

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
    <div>
      <h2>Meeting Rooms</h2>
      <MeetingRoomForm
        onSubmit={editingRoom ? handleUpdate : handleCreate}
        initialData={editingRoom}
        onCancel={() => setEditingRoom(null)}
      />

      <table border="1" cellPadding="8">
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
            <tr key={room.roomRevNr}>
              <td>{room.roomName}</td>
              <td>{room.capacity}</td>
              <td>{room.availability ? "✅" : "❌"}</td>
              <td>
                {room.equipment?.map((eq, i) => (
                  <div key={i}>{eq.name} ({eq.count})</div>
                ))}
              </td>
              <td>
                <button onClick={() => setEditingRoom(room)}>✏️</button>
                <button onClick={() => handleDelete(room.roomRevNr)}>🗑️</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default MeetingRoomsPage;
