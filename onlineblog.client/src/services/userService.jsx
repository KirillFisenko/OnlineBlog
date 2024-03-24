import { ACCOUNT_URL, LOGIN_URL, PROFILE_URL, USERS_URL, sendRequestWithToken, clearStore } from "C:/Users/justi/source/repos/OnlineBlog/onlineblog.client/src/services/commonService";

export async function getUser() {
    var user = await sendRequestWithToken(ACCOUNT_URL, 'GET')
    return user;
}

export async function getPublicUser(userId) {
    var user = await sendRequestWithToken(`${USERS_URL}/${userId}`, 'GET')
    return user;
}

export async function updateUser(user) {
    user.photo = user.photo.toString()
    var newUser = await sendRequestWithToken(ACCOUNT_URL, 'PATCH', user);
    window.location.href = PROFILE_URL;
    return newUser;
}

export async function createUser(user) {
    user.photo = user.photo.toString()
    var newUser = await sendRequestWithToken(ACCOUNT_URL, 'POST', user, false);
    window.location.href = LOGIN_URL;
    return newUser;
}

export async function getUserByName(userName) {
    const users = await sendRequestWithToken(USERS_URL + `/all/${userName}`, 'GET')
    return users;
}

export async function exitFromProfile() {
    const userAnswer = window.confirm("Вы уверены, что хотите выйти?")
    if (userAnswer) {
        clearStore();
        window.location.href = LOGIN_URL;
    }
}