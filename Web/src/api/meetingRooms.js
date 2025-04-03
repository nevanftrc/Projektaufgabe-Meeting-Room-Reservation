import axios from "axios";

const BASE_URL = "http://localhost:5261/api/MeetingRooms";

export const getAllRooms = () => axios.get(BASE_URL);
export const createRoom = (room) => axios.post(BASE_URL, room);
export const updateRoom = (id, room) => axios.put(`${BASE_URL}/${id}`, room);
export const deleteRoom = (id) => axios.delete(`${BASE_URL}/${id}`);
