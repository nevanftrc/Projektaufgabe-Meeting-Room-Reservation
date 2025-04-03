import React from "react";
import { NavLink } from "react-router-dom";

function Sidebar() {
  const linkStyle = {
    display: "block",
    padding: "1rem",
    textDecoration: "none",
    color: "black",
    fontWeight: "bold",
  };

  return (
    <nav style={{ width: "250px", background: "#f0f0f0", paddingTop: "1rem" }}>
      <NavLink to="/rooms" style={linkStyle}>📋 Meeting Rooms</NavLink>
      <NavLink to="/reservations" style={linkStyle}>📅 Reservations</NavLink>
      <NavLink to="/reservers" style={linkStyle}>👤 Reservers</NavLink>
    </nav>
  );
}

export default Sidebar;
