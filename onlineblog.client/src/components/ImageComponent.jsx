import React from 'react';

const ImageComponent = ({ base64String }) => {
    if (base64String === null) {
        return <div />;
    }
    // Вы должны заключить строку в обратные кавычки для использования интерполяции строк в JSX
    const imageUrl = `data:image/jpeg;base64,${base64String}`;
    return <img src={imageUrl} alt="Image"  style={{width: '100%'}} />;
};

export default ImageComponent;
