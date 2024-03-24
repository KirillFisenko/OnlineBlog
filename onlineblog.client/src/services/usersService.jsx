import { ACCOUNT_URL, LOGIN_URL, PROFILE_URL, USERS_URL, clearStore, sendRequestWithToken } from "./commonService";

export async function getUser(){
    const user = await sendRequestWithToken(ACCOUNT_URL, 'GET');
    return user; 
}

export async function getPublicUser(userId){
    const user = await sendRequestWithToken(`${USERS_URL}/${userId}`, 'GET');
    return user; 
}

export async function updateUser(user) {
    user.photo = user.photo.toString()
    const newUser = await sendRequestWithToken(ACCOUNT_URL, 'PATCH', user);
    window.location.href = PROFILE_URL;
    return newUser;
}

export async function createUser(user) {
    user.photo = user.photo.toString()
    const newUser = await sendRequestWithToken(ACCOUNT_URL, 'POST', user, false);
    window.location.href = LOGIN_URL;
    return newUser;
}

export function exitFromProfile(){
    const userAnswer = window.confirm("Are you sure?");
    if (userAnswer) {
        clearStore();
        window.location.href = LOGIN_URL;
    }
}

export async function getUsersByName(userName){
    const users = await sendRequestWithToken(USERS_URL + `/all/${userName}`, 'GET');
    return users;
}

export async function subscribeToUser(userId){
    const users = await sendRequestWithToken(USERS_URL + `/subs/${userId}`, 'POST');
    return users;
}