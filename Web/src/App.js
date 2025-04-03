import React from "react";
import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom";
import Sidebar from "./components/Sidebar";
import MeetingRoomsPage from "./pages/MeetingRoomsPage";
import ReservationsPage from "./pages/ReservationsPage";
import ReserversPage from "./pages/ReserversPage";

function App() {
  return (
    <Router>
      <div className="app-container" style={{ display: "flex", height: "100vh" }}>
        <Sidebar />
        <main style={{ flex: 1, padding: "1rem", overflowY: "auto" }}>
          <Routes>
            <Route path="/" element={<Navigate to="/rooms" />} />
            <Route path="/rooms" element={<MeetingRoomsPage />} />
            <Route path="/reservations" element={<ReservationsPage />} />
            <Route path="/reservers" element={<ReserversPage />} />
          </Routes>
        </main>
      </div>
    </Router>
  );
}

export default App;
