import { useState } from "react"
import ImageComponent from "./ImageComponent";
import ImageUploader from "./ImageUploader";

const NewsCreation = ({id, oldtext, oldImage, setAction}) => {
    const [text, setText] = useState(oldtext);
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

    const imageView = imageStr.length > 0 ? 
        <img src={imageStr} alt="Image" /> 
        : <ImageComponent base64String={oldImage}/>;

        return (
            <div style={{display: 'flex', flexDirection: 'column'}}>
                <textarea defaultValue={text} onChange={e => setText(e.target.value)}/>
                {imageView}
                <ImageUploader byteImageAction={(str, bytes) => {setImage(bytes); setImageStr(str)} }/>
                <button className="btn btn-primary w-100 py-2" onClick={endCreate}>Ok</button>
            </div>
        )
}

export default NewsCreation;