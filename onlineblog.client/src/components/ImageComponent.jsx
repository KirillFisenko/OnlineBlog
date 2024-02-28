import React from 'react';

const ImageComponent = ({ base64String }) => {
  // Преобразование массива байтов в base64-строку
  const base64String = btoa(
    new Uint8Array(byteArray).reduce((data, byte) => data + String.fromCharCode(byte), '')
  );

  return (
    <div>
      <h2>Image Component</h2>
      {base64String && <img src={`data:image/png;base64,${base64String}`} alt="Image" />}
    </div>
  );
};

export default ImageComponent;
