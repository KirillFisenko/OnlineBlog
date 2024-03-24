import React, { useEffect, useState } from 'react';
import { exitFromProfile, getUser, updateUser } from '../services/usersService';

import ModalButton from './ModalButton';
import UserProfileCreation from './UserProfileCreation';
import UserView from './UserView';

const UserProfile = () => {
  const [user, setUser] = useState({
      id: 0,
      firstName: '',
      lastName: '',
      email: '',
      description: '',
      photo: '',
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
    <div>
    <div style={
      {
        display: 'flex',
        justifyContent: 'flex-end'
        }}>
        <ModalButton 
          btnName = {'Edit'}
          modalContent = {<UserProfileCreation user = {user} setAction={updateUserView}/>} 
          title = {'Редактирование профиля'}/>
        <button className="btn btn-secondary" onClick={() => exitFromProfile()}>Exit</button>
    </div>
    <UserView user={user} isProfile={true}/>    
    </div>
  );
};

export default UserProfile;