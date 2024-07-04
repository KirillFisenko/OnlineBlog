import React, { useState } from 'react';
import ImageComponent from './ImageComponent';
import ImageUploader from './ImageUploader';
import "../ImageComponent.css";

const UserProfileCreation = ({ user, setAction }) => {
    const [userFirstName, setFirstName] = useState(user.firstName);
    const [userLastName, setLastName] = useState(user.lastName);
    const [userEmail, setUserEmail] = useState(user.email);
    const [userPassword, setUserPassword] = useState();
    const [userDescription, setUserDescription] = useState(user.description);
    const [userPhoto, setUserPhoto] = useState(user.photo);
    const [userPhotoStr, setUserPhotoStr] = useState('');
    const [errorMessage, setErrorMessage] = useState("");

    const endCreate = () => {
        if (userFirstName === ''
            || userLastName === ''
            || userEmail === ''
            || userPassword === undefined
            || userDescription === '') {
            setErrorMessage("Все поля должны быть заполнены");
        }
        else {
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
        
    }

    const image = userPhotoStr.length > 0 ? <img className="image" src={userPhotoStr} alt="Image" /> : <ImageComponent base64String={user.photo} />;

    return (
        <div className="login-container">
            <img className="mb-4 rounded" src="src\orig.jpg" alt="" width="122" height="107" />
            <h1 className="h3 mb-3 fw-normal">Профиль пользователя</h1>
            <div className="mb-4 form-floating">
                <input type="text" className="form-control" defaultValue={userFirstName} onChange={e => setFirstName(e.target.value)} />
                <label>Имя</label>
            </div>
            <div className="mb-4 form-floating">
                <input type="text" className="form-control" defaultValue={userLastName} onChange={e => setLastName(e.target.value)} />
                <label>Фамилия</label>
            </div>
            <div className="mb-4 form-floating">
                <input type="email" className="form-control" defaultValue={userEmail} onChange={e => setUserEmail(e.target.value)} />
                <label>Email</label>
            </div>
            <div className="mb-4 form-floating">
                <input type="password" className="form-control" defaultValue={userPassword} onChange={e => setUserPassword(e.target.value)} />
                <label>Пароль</label>
            </div>
            <div className="mb-4 form-floating">
                <textarea type="text" className="form-control" defaultValue={userDescription} onChange={e => setUserDescription(e.target.value)} />
                <label>О себе</label>
            </div>
            <div>{image}</div>
            <ImageUploader byteImageAction={(str, bytes) => { setUserPhoto(bytes); setUserPhotoStr(str) }} />
            {errorMessage && <div className="text-danger mb-2">{errorMessage}</div>}
            <button className="btn btn-primary w-100 py-2" onClick={endCreate}>Зарегистрироваться</button>
        </div>
    );
};

export default UserProfileCreation;