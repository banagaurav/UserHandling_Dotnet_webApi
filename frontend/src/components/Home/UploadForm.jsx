import React, { useState, useEffect } from "react";
import { uploadPdf } from "../../api/api";

const UploadForm = () => {
  const [file, setFile] = useState(null);
  const [subjectId, setSubjectId] = useState("");
  const [academicProgramId, setAcademicProgramId] = useState("");
  const [facultyId, setFacultyId] = useState("");
  const [universityId, setUniversityId] = useState("");
  const [subjects, setSubjects] = useState([]);
  const [academicPrograms, setAcademicPrograms] = useState([]);
  const [faculties, setFaculties] = useState([]);
  const [universities, setUniversities] = useState([]);

  // Fetch data for Subject, AcademicProgram, Faculty, and University
  useEffect(() => {
    const fetchData = async () => {
      // Fetch universities
      const universityResponse = await fetch("/api/universities");
      const universitiesData = await universityResponse.json();
      setUniversities(universitiesData);

      // Fetch faculties
      const facultyResponse = await fetch("/api/faculties");
      const facultiesData = await facultyResponse.json();
      setFaculties(facultiesData);

      // Fetch academic programs
      const academicProgramResponse = await fetch("/api/academicprograms");
      const academicProgramsData = await academicProgramResponse.json();
      setAcademicPrograms(academicProgramsData);

      // Fetch subjects
      const subjectResponse = await fetch("/api/subjects");
      const subjectsData = await subjectResponse.json();
      setSubjects(subjectsData);
    };
    fetchData();
  }, []);

  const handleFileChange = (e) => {
    setFile(e.target.files[0]);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    const formData = new FormData();
    formData.append("PdfFile", file);
    formData.append("SubjectId", subjectId);
    formData.append("AcademicProgramId", academicProgramId);
    formData.append("FacultyId", facultyId);
    formData.append("UniversityId", universityId);

    try {
      const response = await uploadPdf(formData);
      alert("File uploaded successfully");
    } catch (error) {
      console.error("Error uploading PDF:", error);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>Upload PDF</h2>

      <div>
        <label>University:</label>
        <select
          value={universityId}
          onChange={(e) => setUniversityId(e.target.value)}
        >
          <option value="">Select University</option>
          {universities.map((university) => (
            <option key={university.id} value={university.id}>
              {university.name}
            </option>
          ))}
        </select>
      </div>

      <div>
        <label>Faculty:</label>
        <select
          value={facultyId}
          onChange={(e) => setFacultyId(e.target.value)}
        >
          <option value="">Select Faculty</option>
          {faculties.map((faculty) => (
            <option key={faculty.id} value={faculty.id}>
              {faculty.name}
            </option>
          ))}
        </select>
      </div>

      <div>
        <label>Academic Program:</label>
        <select
          value={academicProgramId}
          onChange={(e) => setAcademicProgramId(e.target.value)}
        >
          <option value="">Select Academic Program</option>
          {academicPrograms.map((program) => (
            <option key={program.id} value={program.id}>
              {program.name}
            </option>
          ))}
        </select>
      </div>

      <div>
        <label>Subject:</label>
        <select
          value={subjectId}
          onChange={(e) => setSubjectId(e.target.value)}
        >
          <option value="">Select Subject</option>
          {subjects.map((subject) => (
            <option key={subject.id} value={subject.id}>
              {subject.name}
            </option>
          ))}
        </select>
      </div>

      <div>
        <input type="file" onChange={handleFileChange} required />
      </div>

      <button type="submit">Upload</button>
    </form>
  );
};

export default UploadForm;
