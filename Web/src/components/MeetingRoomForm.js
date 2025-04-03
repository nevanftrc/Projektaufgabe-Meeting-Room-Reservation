import React, { useState, useEffect } from "react";

function MeetingRoomForm({ onSubmit, initialData, onCancel }) {
  const [form, setForm] = useState({
    roomName: "",
    capacity: 0,
    availability: true,
    equipment: []
  });

  useEffect(() => {
    if (initialData) setForm(initialData);
  }, [initialData]);

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setForm({ ...form, [name]: type === "checkbox" ? checked : value });
  };

  const handleEquipmentChange = (index, field, value) => {
    const updated = [...form.equipment];
    updated[index][field] = value;
    setForm({ ...form, equipment: updated });
  };

  const addEquipment = () => {
    setForm({ ...form, equipment: [...form.equipment, { name: "", description: "", count: 1 }] });
  };

  const removeEquipment = (index) => {
    const updated = form.equipment.filter((_, i) => i !== index);
    setForm({ ...form, equipment: updated });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onSubmit(form);
  };

  return (
    <form onSubmit={handleSubmit} style={{ border: "1px solid #ccc", padding: "1rem", marginBottom: "1rem" }}>
      <div>
        <label>Room Name: </label>
        <input name="roomName" value={form.roomName} onChange={handleChange} required />
      </div>
      <div>
        <label>Capacity: </label>
        <input name="capacity" type="number" value={form.capacity} onChange={handleChange} required />
      </div>
      <div>
        <label>Available: </label>
        <input name="availability" type="checkbox" checked={form.availability} onChange={handleChange} />
      </div>

      <div>
        <h4>Equipment</h4>
        {form.equipment.map((tool, index) => (
          <div key={index} style={{ marginBottom: "0.5rem" }}>
            <input
              placeholder="Name"
              value={tool.name}
              onChange={(e) => handleEquipmentChange(index, "name", e.target.value)}
            />
            <input
              placeholder="Description"
              value={tool.description}
              onChange={(e) => handleEquipmentChange(index, "description", e.target.value)}
            />
            <input
              placeholder="Count"
              type="number"
              value={tool.count}
              onChange={(e) => handleEquipmentChange(index, "count", e.target.value)}
            />
            <button type="button" onClick={() => removeEquipment(index)}>🗑️</button>
          </div>
        ))}
        <button type="button" onClick={addEquipment}>+ Add Equipment</button>
      </div>

      <div style={{ marginTop: "1rem" }}>
        <button type="submit">💾 Save</button>
        {onCancel && <button type="button" onClick={onCancel}>❌ Cancel</button>}
      </div>
    </form>
  );
}

export default MeetingRoomForm;
