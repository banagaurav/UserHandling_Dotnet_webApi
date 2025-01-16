import { BrowserRouter as Router, Routes, Route } from "react-router-dom"; // Import Router, Routes, and Route
import SignUp from "./pages/SignUp.jsx";
import Login from "./pages/Login.jsx";
import "./App.css";

const App = () => {
  return (
    <Router>
      {/* Wrap everything in Router */}
      <Routes>
        {/* Define the routes here */}
        <Route path="/" element={<Login />} /> {/* Default route */}
        <Route path="/signup" element={<SignUp />} />
        <Route path="/login" element={<Login />} />
      </Routes>
    </Router>
  );
};

export default App;
