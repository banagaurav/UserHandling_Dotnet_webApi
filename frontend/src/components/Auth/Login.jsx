import React, { useState } from "react";
import { useNavigate } from "react-router-dom"; // Import useNavigate

const Login = ({ setIsAuthenticated }) => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate(); // Initialize the navigate function

  const handleLogin = async (e) => {
    e.preventDefault();
    // Replace with your actual authentication logic
    console.log("Logging in with:", { username, password });

    // Simulating a successful login response:
    if (username === "test" && password === "password") {
      // Save user info or token in localStorage (can be replaced with actual user data)
      localStorage.setItem("user", JSON.stringify({ username }));
      setIsAuthenticated(true); // Update the authentication state
    } else {
      alert("Invalid credentials");
    }
  };

  const handleRegisterRedirect = () => {
    // Use navigate to go to the register page
    navigate("/register"); // Change this to the path where your Register component is located
  };

  return (
    <div>
      <form onSubmit={handleLogin}>
        <h2>Login</h2>
        <div>
          <label>Username:</label>
          <input
            type="text"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            required
          />
        </div>
        <div>
          <label>Password:</label>
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>
        <button type="submit">Login</button>
      </form>
      <button onClick={handleRegisterRedirect}>Go to Register</button>
    </div>
  );
};

export default Login;
