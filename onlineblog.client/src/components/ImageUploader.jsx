import React from 'react';

const ImageUploader = ({ byteImageAction }) => {
    const handleFileChange = (event) => {
        const file = event.target.files[0];

        if (file) {
            const reader = new FileReader();

            reader.onload = (e) => {
                const fileContentString = e.target.result;
                const byteArray = new Uint8Array(e.target.result);
                byteImageAction(fileContentString, byteArray);
            };

            reader.readAsArrayBuffer(file);
        }
    };

    return (
        <div>
        <input type= "file" accept = "image/*" onChange = { handleFileChange } />
            </div>
    );
};

export default ImageUploader;
