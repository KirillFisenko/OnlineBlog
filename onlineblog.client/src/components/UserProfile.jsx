import React, { useState, useEffect } from 'react';
import { getUser, updateUser } from '../services/userService';
import ImageComponent from './ImageComponent';
import UserProfileCreation from './UserProfileCreation';
import ModalButton from './ModalButton';
import './UserProfile.css';

const UserProfile = () => {
    const [user, setUser] = useState({
        id: '',
        firstName: '',
        lastName: '',
        email: '',
        description: '',
        photo: ''        
    });

    useEffect(() => {
        const fetchUser = async () => {
            const data = await getUser();
            setUser(data);
        };

        fetchUser();
    }, []);

    const updateUserView = (newUser) => {
        setUser(newUser);
        updateUser(newUser);
    }
    return (
        // отображения страницы профиля
        <div className="user-profile-container">
            <h2>User Profile</h2>
            <p><strong>First Name:</strong> {user.firstName}</p>
            <p><strong>Last Name:</strong> {user.lastName}</p>
            <p><strong>Email:</strong> {user.email}</p>            
            <p><strong>Description:</strong> {user.description}</p>
            <ImageComponent base64String={user.photo} />
            <ModalButton modalContent={<UserProfileCreation user={user} setAction={updateUserView} />} title={'Редактирование профиля'} />
        </div>

    );
};

export default UserProfile;
