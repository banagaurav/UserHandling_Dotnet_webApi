import axios from "axios";

const API_URL = "http://localhost:5087/api/"; // Your backend URL

export const getAllPdfs = async () => {
  try {
    const response = await axios.get(`${API_URL}PDFs/all-with-User`);
    return response.data;
  } catch (error) {
    console.error("Error fetching PDFs:", error);
    throw error;
  }
};

export const uploadPdf = async (formData) => {
  try {
    const response = await axios.post(`${API_URL}PDFs/upload`, formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
    return response.data;
  } catch (error) {
    console.error("Error uploading PDF:", error);
    throw error;
  }
};
