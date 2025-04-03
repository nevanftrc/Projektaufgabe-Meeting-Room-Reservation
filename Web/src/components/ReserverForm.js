import React, { useState, useEffect } from "react";

function ReserverForm({ onSubmit, initialData, onCancel }) {
  const [form, setForm] = useState({ name: "" });

  useEffect(() => {
    if (initialData) setForm(initialData);
  }, [initialData]);

  const handleChange = (e) => {
    setForm({ ...form, name: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!form.name.trim()) {
      alert("Name is required.");
      return;
    }
    onSubmit(form);
  };

  return (
    <form onSubmit={handleSubmit} style={{ border: "1px solid #ccc", padding: "1rem", marginBottom: "1rem" }}>
      <div>
        <label>Name: </label>
        <input value={form.name} onChange={handleChange} required />
      </div>
      <div style={{ marginTop: "1rem" }}>
        <button type="submit">💾 Save</button>
        {onCancel && <button type="button" onClick={onCancel}>❌ Cancel</button>}
      </div>
    </form>
  );
}

export default ReserverForm;
