import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom"; // Import useNavigate
import pdfService from "../services/pdfService";
import "../styles/Home.css";

const Home = () => {
  const [pdfs, setPdfs] = useState([]);
  const [error, setError] = useState("");
  const [username, setUsername] = useState("");
  const [isDropdownOpen, setIsDropdownOpen] = useState(false); // Dropdown state
  const navigate = useNavigate(); // Use navigate hook

  // Function to handle sign out
  const handleSignOut = () => {
    localStorage.removeItem("token"); // Clear the token
    localStorage.removeItem("username"); // Clear the username
    navigate("/login"); // Redirect to login page
  };

  // Function to toggle dropdown visibility
  const toggleDropdown = () => {
    setIsDropdownOpen(!isDropdownOpen);
  };

  // Fetch PDFs on component mount
  useEffect(() => {
    const fetchPdfs = async () => {
      try {
        const data = await pdfService.getAllPdfs();
        setPdfs(data);
      } catch (err) {
        setError("Failed to load PDFs.");
      }
    };

    // Get the username from localStorage
    const storedUsername = localStorage.getItem("username");
    if (storedUsername) {
      setUsername(storedUsername);
    }

    fetchPdfs();
  }, []);

  return (
    <div>
      <div className="user-info">
        {/* Username click toggles the dropdown */}
        <span onClick={toggleDropdown} style={{ cursor: "pointer" }}>
          {username}
        </span>
        {isDropdownOpen && (
          <div className="dropdown-menu">
            <button onClick={handleSignOut}>Sign Out</button>
          </div>
        )}
      </div>
      <h1>All PDFs</h1>
      {error && <p style={{ color: "red" }}>{error}</p>}
      <div style={{ display: "flex", flexWrap: "wrap", gap: "1rem" }}>
        {pdfs.map((pdf) => (
          <div
            key={pdf.title}
            style={{
              border: "1px solid #ccc",
              borderRadius: "8px",
              padding: "1rem",
              width: "200px",
            }}
          >
            <h3>{pdf.title}</h3>
            <p>{pdf.description}</p>
            <small>
              <strong>Authors:</strong>{" "}
              {pdf.authors && pdf.authors.length > 0
                ? pdf.authors.join(", ")
                : "Unknown"}
            </small>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Home;
