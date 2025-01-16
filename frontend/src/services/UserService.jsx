import axios from "axios";

const API_URL = "http://localhost:5180/api/User"; // Your API base URL

// Service to create a new user
const createUser = async (userData) => {
  return axios.post(`${API_URL}/create`, userData);
};

// Service to log in a user
const loginUser = async (credentials) => {
  return axios.post(`${API_URL}/login`, credentials);
};

// src/services/UserService.jsx
const userService = {
  registerUser: async (userData) => {
    const response = await fetch("/api/users/register", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(userData),
    });

    if (!response.ok) {
      throw new Error("Failed to register user");
    }

    return response.json();
  },
};

export default {
  createUser,
  loginUser,
};
