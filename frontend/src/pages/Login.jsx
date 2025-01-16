import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import userService from "../services/UserService";
import "../styles/Login.css";

const Login = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await userService.loginUser({ username, password });
      if (response.status === 200) {
        const token = response.data.token;
        localStorage.setItem("token", token); // Store token in localStorage

        // Optionally, you can store the username for the user info in your top-right section
        localStorage.setItem("username", username);

        navigate("/home");
      }
    } catch (error) {
      setError("Invalid credentials");
    }
  };

  return (
    <div className="login-container">
      <h2>Login</h2>
      <form className="login-form" onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="Username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        {error && <p className="error-message">{error}</p>}
        <button type="submit" className="login-button">
          Login
        </button>
      </form>
      <p>
        Don't have an account?{" "}
        <button onClick={() => navigate("/signup")} className="link-button">
          Sign Up
        </button>
      </p>
    </div>
  );
};

export default Login;
