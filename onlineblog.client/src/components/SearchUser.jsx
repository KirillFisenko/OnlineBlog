import React, { useState } from 'react';
import { getUserByName } from '../services/userService';
import ImageComponent from './ImageComponent';
import { isUserOnline, LOGIN_URL } from "C:/Users/justi/source/repos/OnlineBlog/onlineblog.client/src/services/commonService";

const SearchUser = () => {
    const [users, setUsers] = useState([]);

    if (!isUserOnline()) window.location.href = LOGIN_URL;

    const getUsers = async (userName) => {
        try {
            const allUsers = await getUserByName(userName);
            setUsers(allUsers);
        }
        catch {
            return;
        }
    }

    return (
        <div>
            <input type="text" onChange={e => getUsers(e.target.value)} />
            {users != undefined ?
                users.map(x => <ShortUserView user={x} />)
                :
                <p></p>}
        </div>
    )
}
export default SearchUser;

const ShortUserView = ({ user }) => {

    const userClick = (userId) => {
        window.location.href = `/all/${userId}`;
    }

    return (
        <div onClick={() => userClick(userId)}>
            <div>
                <ImageComponent base64String={user.photo} />
            </div>
            <div>
                <p>{user.name}</p>
                <p>{user.description}</p>
            </div>
        </div>
    )
}