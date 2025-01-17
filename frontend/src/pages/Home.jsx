import React from "react";
import { useNavigate } from "react-router-dom";

const Home = () => {
  const navigate = useNavigate();

  // Logout function (Optional)
  const handleLogout = () => {
    localStorage.removeItem("token"); // Clear token (if using authentication)
    navigate("/"); // Redirect to login
  };

  return (
    <div style={{ textAlign: "center", padding: "50px" }}>
      <h1>Welcome to Home Page</h1>
      <p>This is the homepage after login.</p>
      <button onClick={handleLogout}>Logout</button>
    </div>
  );
};

export default Home;
