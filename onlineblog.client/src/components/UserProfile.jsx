import React, { useState, useEffect } from 'react';
import { getUser } from '../services/userService';
import ImageComponent from './ImageComponent';
import UserProfileCreation from './UserProfileCreation';

const UserProfile = () => {
    const [user, setUser] = useState({        
        firstName: '',
        lastName: '',
        email: '',
        description: '',
        photo: null,
        subsCount: 0   
    });

    useEffect(() => {
        const fetchUser = async () => {
            const data = await getUser();
            setUser(data);
        };

        fetchUser();
    }, []);

    const updateUser = (newuser) => {
        
    }
    return (
        // отображения страницы профиля
        <div>
            <h2>User Profile</h2>            
            <p><strong>First Name:</strong> {user.firstName}</p>
            <p><strong>Last Name:</strong> {user.lastName}</p>
            <p><strong>Email:</strong> {user.email}</p>
            <p><strong>SubsCount:</strong> {user.subsCount}</p>
            <p><strong>Description:</strong> {user.description}</p>            
            <ImageComponent byteArray={user.photo} />
            <ModalButton modalContent = {<UserProfileCreation user = {user} />} title = {'Редактирование профиля'} />
        </div>
    );
};

export default UserProfile;
