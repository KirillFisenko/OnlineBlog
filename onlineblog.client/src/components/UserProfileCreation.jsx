import React, { useState } from 'react';
import ImageComponent from './ImageComponent';
import ImageUploader from './ImageUploader';

const UserProfileCreation = ({ user, setAction }) => {
    const [userFirstName, setFirstName] = useState(user.firstName);
    const [userLastName, setLastName] = useState(user.lastName);
    const [userEmail, setUserEmail] = useState(user.email);
    const [userPassword, setUserPassword] = useState();
    const [userDescription, setUserDescription] = useState(user.description);
    const [userPhoto, setUserPhoto] = useState(user.photo);
    const [userPhotoStr, setUserPhotoStr] = useState('');

    const endCreate = () => {
        if (userPassword.length === 0) return;

        const newUser = {
            id: user.id,
            firstName: userFirstName,
            lastName: userLastName,
            email: userEmail,
            description: userDescription,
            password: userPassword,
            photo: userPhoto
        }
        setAction(newUser);
    }

    const image = userPhotoStr.length > 0 ? <img src={userPhotoStr} alt="Image" /> : <ImageComponent base64String={user.photo} />;

    return (
        <div style={{ display: 'flex', flexDirection: 'column' }}>
            <h2>User Profile</h2>
            <p>Name</p>
            <input type='text' defaultValue={userFirstName} onChange={e => setFirstName(e.target.value)} />
            <p>Name</p>
            <input type='text' defaultValue={userLastName} onChange={e => setLastName(e.target.value)} />
            <p>Email</p>
            <input type='text' defaultValue={userEmail} onChange={e => setUserEmail(e.target.value)} />
            <p>Password</p>
            <input type='text' defaultValue={userPassword} onChange={e => setUserPassword(e.target.value)} />
            <p>Description</p>
            <textarea defaultValue={userDescription} onChange={e => setUserDescription(e.target.value)} />
            {image}
            <ImageUploader byteImageAction={(str, bytes) => { setUserPhoto(bytes); setUserPhotoStr(str) }} />
            <button onClick={endCreate}>Ok</button>
        </div>
    );
};

export default UserProfileCreation;