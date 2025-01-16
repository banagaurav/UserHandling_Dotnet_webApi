import axios from "axios";

const API_BASE_URL = "http://localhost:5180/api/Pdf"; // Adjust the port if necessary

const getAllPdfs = async () => {
  try {
    const response = await axios.get(`${API_BASE_URL}/all`);
    return response.data; // Returns the list of PDFs
  } catch (error) {
    console.error("Error fetching PDFs:", error);
    throw error;
  }
};

// Refactor uploadPdf to use axios
const uploadPdf = async (formData, token) => {
  try {
    const response = await axios.post(
      "http://localhost:5180/api/Pdf/upload",
      formData,
      {
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "multipart/form-data",
        },
      }
    );
    return response.data;
  } catch (err) {
    console.error("Error uploading PDF:", err);
    throw err;
  }
};

export default { getAllPdfs, uploadPdf };
