import React, { useState } from 'react';
import ImageComponent from './ImageComponent';
import ImageUploader from './ImageUploader';

const UserProfileCreation = ({ user, setAction }) => {
    const [userFirstName, setFirstName] = useState();
    const [userLastName, setLastName] = useState();
    const [userEmail, setUserEmail] = useState();
    const [userDescription, setUserDescription] = useState();
    const [userPhoto, setUserPhoto] = useState();

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
        <div>
            <h2>User Profile</h2>
            <input type='text' onChange={e => setFirstName(e.target.value)} />
            <input type='text' onChange={e => setLastName(e.target.value)} />
            <input type='text' onChange={e => setUserEmail(e.target.value)} />
            <textarea onChange={e => setUserDescription(e.target.value)} />
            <ImageUploader byteImageAction={(bytes) => setUserPhoto(bytes)} />
            <ImageComponent byteArray={userPhoto} />
            <button onClick={endCreate}/>
        </div>
    );
};

export default UserProfileCreation;
