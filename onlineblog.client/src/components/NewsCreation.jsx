import React, { useState } from 'react';
import ImageComponent from './ImageComponent';
import ImageUploader from './ImageUploader';

const NewsCreation = ({ id, oldText, oldImage, setAction }) => {
    const [text, setText] = useState(oldText);
    const [image, setImage] = useState(oldImage);
    const [imageStr, setImageStr] = useState('');

    const endCreate = () => {
        const newNews = {
            id: id,
            text: text,
            image: image
        };

        setAction(newNews);
    }
    const imageView = imageStr.length > 0 ? <img src={imageStr} alt="Image" /> : <ImageComponent base64String={oldImage} />;

    return (
        <div>
            <p>{text}</p>
            <textarea type='text' value={text} onChange={e => setText(e.target.value)} />
            {imageView}
            <ImageUploader byteImageAction={(str, bytes) => { setImage(bytes); setImageStr(str); }} />
            <button onClick={endCreate}>Ok</button>
        </div>
    );
}

export default NewsCreation;