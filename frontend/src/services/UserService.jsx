import axios from "axios";

const API_URL = "http://localhost:5180/api/auth";

const registerUser = async (userData) => {
  return axios.post(`${API_URL}/register`, userData);
};

const loginUser = async (credentials) => {
  try {
    const response = await axios.post(`${API_URL}/login`, credentials);
    localStorage.setItem("token", response.data.token);
    return response.data;
  } catch (error) {
    console.error(
      "Login Error:",
      error.response?.data?.message || "Login failed"
    );
    throw new Error(error.response?.data?.message || "Login failed");
  }
};

export default { registerUser, loginUser };
