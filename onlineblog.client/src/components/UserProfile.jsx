import React, { useEffect, useState } from 'react';
import { exitFromProfile, getUser, updateUser } from '../services/usersService';
import ModalButton from './ModalButton';
import UserView from './UserView';
import UserProfileEdit from '/src/components/UserProfileEdit.jsx';

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
          btnName = {'Редактировать'}
          modalContent = {<UserProfileEdit user = {user} setAction={updateUserView}/>}           />
        <button className="btn btn-sm btn-secondary" onClick={() => exitFromProfile()}>Выйти</button>
    </div>
    <UserView user={user} isProfile={true}/>    
    </div>
  );
};

export default UserProfile;