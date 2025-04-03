import axios from "axios";

const BASE_URL = "http://localhost:5261/api/Reservations";

export const getAllReservations = () => axios.get(BASE_URL);
export const createReservation = (data) => axios.post(BASE_URL, data);
export const updateReservation = (id, data) => axios.put(`${BASE_URL}/${id}`, data);
export const deleteReservation = (id) => axios.delete(`${BASE_URL}/${id}`);
