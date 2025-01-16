import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import userService from "../services/UserService";
import "../styles/SignUp.css";

const SignUp = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [email, setEmail] = useState("");
  const [role, setRole] = useState("User");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    const newUser = { username, password, email, role };

    try {
      const response = await userService.createUser(newUser);
      if (response.status === 201) {
        navigate("/login");
      }
    } catch (error) {
      setError("Failed to create user");
    }
  };

  return (
    <div className="signup-container">
      <h2>Sign Up</h2>
      <form className="signup-form" onSubmit={handleSubmit}>
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
        <input
          type="email"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <select onChange={(e) => setRole(e.target.value)} value={role}>
          <option value="User">User</option>
          <option value="Admin">Admin</option>
        </select>
        {error && <p className="error-message">{error}</p>}
        <button type="submit" className="signup-button">
          Sign Up
        </button>
      </form>
      <p>
        Already have an account?{" "}
        <button onClick={() => navigate("/login")} className="link-button">
          Login
        </button>
      </p>
    </div>
  );
};

export default SignUp;
