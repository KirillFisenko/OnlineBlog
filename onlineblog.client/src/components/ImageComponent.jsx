import React from 'react';

const ImageComponent = ({ base64String }) => {
    if (base64String === null) {
        return <img alt="Image" />;
    }
    // Вы должны заключить строку в обратные кавычки для использования интерполяции строк в JSX
    const imgUrl = `data:image/jpeg;base64,${base64String}`;
    return <img src={imgUrl} alt="Image" />;
};

export default ImageComponent;
