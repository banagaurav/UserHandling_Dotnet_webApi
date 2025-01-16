import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import pdfService from "../services/pdfService";
import "../styles/Home.css";

const Home = () => {
  const [pdfs, setPdfs] = useState([]);
  const [error, setError] = useState("");
  const [username, setUsername] = useState("");
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);
  const [selectedFile, setSelectedFile] = useState(null);
  const [isUploading, setIsUploading] = useState(false);
  const navigate = useNavigate();

  // Handle user sign out
  const handleSignOut = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("username");
    navigate("/login");
  };

  // Toggle the user info dropdown
  const toggleDropdown = () => {
    setIsDropdownOpen(!isDropdownOpen);
  };

  // Fetch all PDFs when component loads
  useEffect(() => {
    const fetchPdfs = async () => {
      try {
        const data = await pdfService.getAllPdfs();
        setPdfs(data);
      } catch (err) {
        console.error("Error fetching PDFs:", err);
        setError("Failed to load PDFs.");
      }
    };

    const storedUsername = localStorage.getItem("username");
    if (storedUsername) {
      setUsername(storedUsername);
    }

    fetchPdfs();
  }, []);

  // Handle file selection
  const handleFileChange = (event) => {
    const file = event.target.files[0];
    if (file) {
      setSelectedFile(file);
    }
  };

  // Handle PDF upload
  const handleUpload = async () => {
    if (!selectedFile) {
      alert("Please select a file first.");
      return;
    }

    const formData = new FormData();
    formData.append("file", selectedFile);
    formData.append("title", selectedFile.name); // Use the file name as the title
    formData.append("description", "Uploaded via React app");

    const token = localStorage.getItem("token");
    if (!token) {
      alert("You must be logged in to upload a PDF.");
      return;
    }

    setIsUploading(true); // Disable button during upload

    try {
      await pdfService.uploadPdf(formData, token);
      alert("PDF uploaded successfully");
      setSelectedFile(null); // Clear selected file after successful upload
      window.location.reload(); // Optionally, fetch new PDFs without reloading the page
    } catch (err) {
      setError("Failed to upload PDF.");
    } finally {
      setIsUploading(false); // Re-enable button
    }
  };

  return (
    <div>
      {/* User Information and Sign Out */}
      <div className="user-info">
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

      {/* PDF Upload Section */}
      <div className="upload-section">
        <input
          type="file"
          accept="application/pdf"
          onChange={handleFileChange}
        />
        {selectedFile && (
          <div>
            <h3>Selected File: {selectedFile.name}</h3>
            <p>Size: {Math.round(selectedFile.size / 1024)} KB</p>
          </div>
        )}
        <button onClick={handleUpload} disabled={isUploading}>
          {isUploading ? "Uploading..." : "Upload PDF"}
        </button>
      </div>

      {/* Display PDF Cards */}
      <div style={{ display: "flex", flexWrap: "wrap", gap: "1rem" }}>
        {pdfs.map((pdf) => (
          <div
            key={pdf.pdfId || pdf.title} // Use pdfId or pdf.title if pdfId is missing
            className="pdf-card"
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
