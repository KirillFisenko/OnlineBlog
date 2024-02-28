import React, { useState } from 'react';
import ImageComponent from './ImageComponent';
import ImageUploader from './ImageUploader';

const UserProfileCreation = ({ user, setAction }) => {
    const [userFirstName, setFirstName] = useState(user.firstName);
    const [userLastName, setLastName] = useState(user.lastName);
    const [userEmail, setUserEmail] = useState(user.email);
    const [userDescription, setUserDescription] = useState(user.description);
    const [userPhoto, setUserPhoto] = useState(user.photo);
    const [userPhotoStr, setUserPhotoStr] = useState('');

    const endCreate = () => {
        const newUser = {
            firstName: userFirstName,
            lastName: userLastName,
            email: userEmail,
            description: userDescription,
            photo: userPhoto
        }
        setAction(newUser);
    }

    const image = userPhotoStr.length > 0 ? <img src={`data:image/png;base64,${base64String}`} alt="Image" /> : <ImageComponent base64String={user.photo} />

    return (
        // карточка редактирования профиля
        <div style={{display: 'flex', flexDirection: 'colimn'} }>
            <h2>User Profile</h2>
            <p>Имя</p>
            <input type='text' defaultValue={userFirstName} onChange={e => setFirstName(e.target.value)} />
            <p>Фамилия</p>
            <input type='text' defaultValue={userLastName} onChange={e => setLastName(e.target.value)}/> 
            <p>Email</p>
            <input type='text' defaultValue={userEmail}  onChange={e => setUserEmail(e.target.value)} />
            <p>Описание</p>
            <textarea defaultValue={userDescription} onChange={e => setUserDescription(e.target.value)} />
            <p>Фото</p>
            {image}
            <ImageUploader byteImageAction={(str, bytes) => { setUserPhoto(bytes), setUserPhotoStr(str) }} />
            
            <button onClick={endCreate}>Ok</button>
        </div>
    );
};

export default UserProfileCreation;
