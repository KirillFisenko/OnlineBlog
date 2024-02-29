import React, { useState } from 'react';
import ImageComponent from './ImageComponent';
import ImageUploader from './ImageUploader';
import './UserProfileCreation.css';

const UserProfileCreation = ({ user, setAction }) => {
    const [userFirstName, setFirstName] = useState(user.firstName);
    const [userLastName, setLastName] = useState(user.lastName);
    const [userEmail, setUserEmail] = useState(user.email);    
    const [userDescription, setUserDescription] = useState(user.description);
    const [userPhoto, setUserPhoto] = useState(user.photo);
    const [userPhotoStr, setUserPhotoStr] = useState('');

    const endCreate = () => {
        // if (userPassword.length == 0) return;
        const newUser = {
            id: user.id,
            firstName: userFirstName,
            lastName: userLastName,
            email: userEmail,           
            description: userDescription,            
            photo: userPhoto
        }
        setAction(newUser);
    }

    const image = userPhotoStr.length > 0 ? <img src={userPhotoStr} alt="Image" /> : <ImageComponent base64String={user.photo} />;

    return (
        // карточка редактирования профиля
        <div class='user-profile-container'>
            <h2>User Profile</h2>
            <p>Имя</p>
            <input type='text' value={userFirstName} onChange={e => setFirstName(e.target.value)} />
            <p>Фамилия</p>
            <input type='text' value={userLastName} onChange={e => setLastName(e.target.value)} />
            <p>Email</p>
            <input type='text' value={userEmail} onChange={e => setUserEmail(e.target.value)} />            
            <p>Описание</p>
            <textarea value={userDescription} onChange={e => setUserDescription(e.target.value)} />
            <p>Фото</p>
            {image}
            <ImageUploader byteImageAction={(str, bytes) => { setUserPhoto(bytes); setUserPhotoStr(str); }} />
            <button onClick={endCreate}>Ok</button>
        </div>
    );
};

export default UserProfileCreation;
