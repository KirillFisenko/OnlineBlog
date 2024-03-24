import React from 'react';

const ImageComponent = ({ base64String }) => {
  
  if (base64String === null) return <div></div>;

  // Создание Data URL
  const imageUrl = `data:image/jpeg;base64,${base64String}`;

  return <img style={{width: '100%'}} src={imageUrl} alt="Image" />;
};

export default ImageComponent;
