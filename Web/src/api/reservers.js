import axios from "axios";

const BASE_URL = "http://localhost:5261/api/Reservers";

export const getAllReservers = () => axios.get(BASE_URL);
export const createReserver = (data) => axios.post(BASE_URL, data);
export const updateReserver = (id, data) => axios.put(`${BASE_URL}/${id}`, data);
export const deleteReserver = (id) => axios.delete(`${BASE_URL}/${id}`);
