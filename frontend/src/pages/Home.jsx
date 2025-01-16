import React, { useEffect, useState } from "react";
import pdfService from "../services/pdfService";

const Home = () => {
  const [pdfs, setPdfs] = useState([]);
  const [error, setError] = useState("");

  useEffect(() => {
    const fetchPdfs = async () => {
      try {
        const data = await pdfService.getAllPdfs();
        setPdfs(data);
      } catch (err) {
        setError("Failed to load PDFs.");
      }
    };

    fetchPdfs();
  }, []);

  return (
    <div>
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
