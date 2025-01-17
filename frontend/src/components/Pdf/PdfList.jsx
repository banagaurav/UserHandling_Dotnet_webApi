import React from "react";

const PdfList = ({ pdfs }) => {
  return (
    <div>
      {pdfs.length > 0 ? (
        pdfs.map((pdf) => (
          <div key={pdf.id}>
            <h3>{pdf.fileName}</h3>
            <div>
              <h4>Users:</h4>
              <ul>
                {pdf.users.map((user) => (
                  <li key={user.id}>
                    {user.fullName} - {user.role}
                  </li>
                ))}
              </ul>
            </div>
            <div>
              <h4>Subjects:</h4>
              <ul>
                {pdf.subjects.map((subject) => (
                  <li key={subject.id}>
                    {subject.name} ({subject.code}) - {subject.creditHours}{" "}
                    Credit Hours
                  </li>
                ))}
              </ul>
            </div>
          </div>
        ))
      ) : (
        <p>No PDFs found.</p>
      )}
    </div>
  );
};

export default PdfList;
