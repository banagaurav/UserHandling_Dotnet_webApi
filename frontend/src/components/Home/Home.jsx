import React, { useEffect, useState } from "react";
import { getAllPdfs } from "../../api/api";
import PdfList from "../Pdf/PdfList";

const Home = () => {
  const [pdfs, setPdfs] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchPdfs = async () => {
      try {
        const data = await getAllPdfs();
        setPdfs(data);
      } catch (error) {
        console.error("Error loading PDFs:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchPdfs();
  }, []);

  return (
    <div>
      <h1>All PDFs</h1>
      {loading ? <p>Loading...</p> : <PdfList pdfs={pdfs} />}
    </div>
  );
};

export default Home;
