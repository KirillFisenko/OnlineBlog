import { useParams } from 'react-router-dom';
import React, { useState, useEffect } from 'react';
import UserView from './UserView';
import { getPublicUser } from '../services/userService';

const UserPublicView = () => {
    const [user, setUser] = useState({
        id: 0,
        firstName: '',
        lastName: '',
        email: '',
        description: '',
        photo: ''
    });

    const param = useParams();
    const userId = param.userId;

    useEffect(() => {
        const fetchUser = async () => {
            const data = await getPublicUser(userId);
            setUser(data);
        };

        fetchUser();
    }, []);

    return (
        <div>
            {<UserView user={user} />}
        </div>
    );
}

export default UserPublicView;