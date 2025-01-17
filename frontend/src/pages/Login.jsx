import React, { useState } from "react";
import { useNavigate } from "react-router-dom";

const Login = ({ onLogin }) => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();

    // Dummy authentication (Replace with API call)
    if (username === "admin" && password === "password") {
      localStorage.setItem("token", "dummy-jwt-token"); // Store token (if using authentication)
      onLogin(); // Update authentication state
      navigate("/home"); // Redirect to Home Page
    } else {
      alert("Invalid username or password");
    }
  };

  return (
    <div style={{ textAlign: "center", padding: "50px" }}>
      <h1>Login Page</h1>
      <form onSubmit={handleLogin}>
        <input
          type="text"
          placeholder="Username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          required
        />
        <br />
        <br />
        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />
        <br />
        <br />
        <button type="submit">Login</button>
      </form>
    </div>
  );
};

export default Login;
