import React, { useState, useEffect } from "react";
import {
  BrowserRouter as Router,
  Route,
  Routes,
  Navigate,
} from "react-router-dom";
import Login from "./components/Auth/Login";
import Register from "./components/Auth/Register";
import Home from "./components/Home/Home";
import UploadForm from "./components/Home/UploadForm";

const App = () => {
  // Track if the user is authenticated
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  // Simulate checking authentication status (could be replaced with API or context)
  useEffect(() => {
    const user = localStorage.getItem("user");
    if (user) {
      setIsAuthenticated(true);
    }
  }, []);

  return (
    <Router>
      <Routes>
        {/* If not authenticated, redirect to login */}
        <Route
          path="/"
          element={isAuthenticated ? <Home /> : <Navigate to="/login" />}
        />
        <Route
          path="/login"
          element={<Login setIsAuthenticated={setIsAuthenticated} />}
        />
        <Route path="/register" element={<Register />} />
        <Route
          path="/upload"
          element={isAuthenticated ? <UploadForm /> : <Navigate to="/login" />}
        />
      </Routes>
    </Router>
  );
};

export default App;
