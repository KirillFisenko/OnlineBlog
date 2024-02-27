import React, { useState } from 'react';
import ImageComponent from './ImageComponent';
import ImageUploader from './ImageUploader';

const UserProfileCreation = ({ user, setAction }) => {
    const [userFirstName, setFirstName] = useState(user.firstName);
    const [userLastName, setLastName] = useState(user.lastName);
    const [userEmail, setUserEmail] = useState(user.email);
    const [userDescription, setUserDescription] = useState(user.description);
    const [userPhoto, setUserPhoto] = useState(user.photo);

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
            <ImageUploader byteImageAction={(bytes) => setUserPhoto(bytes)} />
            <ImageComponent byteArray={userPhoto} />
            <button onClick={endCreate}>Ok</button>
        </div>
    );
};

export default UserProfileCreation;
