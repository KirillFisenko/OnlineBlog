import React from 'react';

const ImageUploader = ({ byteImageAction }) => {
  const handleFileChange = (event) => {
    const file = event.target.files[0];

    if (file) {
      const reader = new FileReader();
      const imageUrl = URL.createObjectURL(file);
      reader.onload = (e) => {        
        const byteArray = new Uint8Array(e.target.result);        
        byteImageAction(imageUrl, byteArray);
      };

      reader.readAsArrayBuffer(file);
    }
  };

  return (
    <div>
      <input className="mb-4" type="file" accept="image/*" onChange={handleFileChange} />
    </div>
  );
};

export default ImageUploader;
