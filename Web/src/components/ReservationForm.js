import React, { useState, useEffect } from "react";

function ReservationForm({ onSubmit, initialData, onCancel, existingReservations }) {
  const [form, setForm] = useState({
    revNr: "",
    roomRevNr: "",
    startTime: "",
    endTime: "",
    reason: ""
  });

  useEffect(() => {
    if (initialData) {
      setForm({
        ...initialData,
        startTime: initialData.startTime?.slice(0, 16),
        endTime: initialData.endTime?.slice(0, 16),
      });
    }
  }, [initialData]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm({ ...form, [name]: value });
  };

  const isOverlapping = () => {
    const newStart = new Date(form.startTime);
    const newEnd = new Date(form.endTime);

    return existingReservations.some((res) => {
      if (res.revRoomMet === initialData?.revRoomMet) return false;
      if (res.roomRevNr !== form.roomRevNr) return false;

      const existingStart = new Date(res.startTime);
      const existingEnd = new Date(res.endTime);

      return (
        (newStart >= existingStart && newStart < existingEnd) ||
        (newEnd > existingStart && newEnd <= existingEnd) ||
        (newStart <= existingStart && newEnd >= existingEnd)
      );
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    if (!form.revNr || !form.roomRevNr || !form.startTime || !form.endTime) {
      alert("All required fields must be completed.");
      return;
    }

    const startISO = new Date(form.startTime).toISOString();
    const endISO = new Date(form.endTime).toISOString();

    if (new Date(startISO) >= new Date(endISO)) {
      alert("Start time must be before end time");
      return;
    }

    if (isOverlapping()) {
      alert("The selected time overlaps with another reservation for this room.");
      return;
    }

    onSubmit({
      ...form,
      startTime: startISO,
      endTime: endISO
    });
  };

  return (
    <form onSubmit={handleSubmit} style={{ border: "1px solid #ccc", padding: "1rem", marginBottom: "1rem" }}>
      <div>
        <label>Reserver ID (revNr): </label>
        <input name="revNr" value={form.revNr} onChange={handleChange} required />
      </div>
      <div>
        <label>Room ID (roomRevNr): </label>
        <input name="roomRevNr" value={form.roomRevNr} onChange={handleChange} required />
      </div>
      <div>
        <label>Start Time: </label>
        <input type="datetime-local" name="startTime" value={form.startTime} onChange={handleChange} required />
      </div>
      <div>
        <label>End Time: </label>
        <input type="datetime-local" name="endTime" value={form.endTime} onChange={handleChange} required />
      </div>
      <div>
        <label>Reason: </label>
        <input name="reason" value={form.reason} onChange={handleChange} />
      </div>

      <div style={{ marginTop: "1rem" }}>
        <button type="submit">💾 Save</button>
        {onCancel && <button type="button" onClick={onCancel}>❌ Cancel</button>}
      </div>
    </form>
  );
}

export default ReservationForm;