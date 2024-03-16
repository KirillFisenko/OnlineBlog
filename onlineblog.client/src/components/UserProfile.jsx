import React, { useState, useEffect } from 'react';
import { exitFromProfile, getUser, updateUser } from '../services/userService';
import UserProfileCreation from './UserProfileCreation';
import ModalButton from './ModalButton';
import './UserProfile.css';
import UserView from './UserView';

const UserProfile = () => {
    const [user, setUser] = useState({
        id: 0,
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
        <div>
            <div>
                <ModalButton modalContent={<UserProfileCreation user={user} setAction={updateUserView} />} title={'Редактирование профиля'} />
                <button type="button" className="btn btn-secondary" onClick={exitFromProfile}>Выход</button>
            </div>
            <UserView user={user} />
        </div >
    );
};

export default UserProfile;
