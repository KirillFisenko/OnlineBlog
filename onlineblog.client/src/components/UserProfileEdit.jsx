import React, { useState } from 'react';
import ImageComponent from './ImageComponent';
import ImageUploader from './ImageUploader';

const UserProfileEdit = ({ user, setAction }) => {
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
        <div>            
            <h1 class="h3 mb-3 fw-normal">Профиль пользователя</h1>
            <div class="mb-4 form-floating">
                <input type="email" class="form-control" id="floatingInput" placeholder="Кирилл" wfd-id="id0" defaultValue={userFirstName} onChange={e => setFirstName(e.target.value)} />
                <label>Имя</label>
            </div>
            <div class="mb-4 form-floating">
                <input type="email" class="form-control" id="floatingInput" placeholder="Кирилл" wfd-id="id0" defaultValue={userLastName} onChange={e => setLastName(e.target.value)} />
                <label>Фамилия</label>
            </div>
            <div class="mb-4 form-floating">
                <input type="email" class="form-control" id="floatingInput" placeholder="name@example.com" wfd-id="id0" defaultValue={userEmail} onChange={e => setUserEmail(e.target.value)} />
                <label>Email</label>
            </div>
            <div class="mb-4 form-floating">
                <input type="password" class="form-control" id="floatingPassword" placeholder="Password" wfd-id="id1" defaultValue={userPassword} onChange={e => setUserPassword(e.target.value)} />
                <label>Пароль</label>
            </div>
            <div class="mb-4 form-floating">
                <textarea type="email" class="form-control" id="floatingInput" placeholder="Кирилл" wfd-id="id0" defaultValue={userDescription} onChange={e => setUserDescription(e.target.value)} />
                <label>О себе</label>
            </div>
            {image}
            <ImageUploader byteImageAction={(str, bytes) => { setUserPhoto(bytes); setUserPhotoStr(str) }} />
            <button class="btn btn-primary w-100 py-2" onClick={endCreate}>Ок</button>
        </div>
    );
};

export default UserProfileEdit;