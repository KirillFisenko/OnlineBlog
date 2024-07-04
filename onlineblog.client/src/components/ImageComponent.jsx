import React from 'react';
import "../ImageComponent.css";

const ImageComponent = ({ base64String }) => {
  if (base64String === null || base64String === "") {
    return <img className="image" src="src\default_logo_user.jpg" alt="default_logo_user.jpg" />;
  } else {
    const imageUrl = `data:image/jpeg;base64,${base64String}`;
    return <img className="image" src={imageUrl} alt="image" />;
  }
};

export default ImageComponent;
