import { ACCOUNT_URL, PROFILE_URL, sendRequestWithToken } from "./commonService";

export async function getUser() {
    var user = await sendRequestWithToken(ACCOUNT_URL, 'GET')
    return user;
}

export async function updateUser(user) {
    if (user.photo !== null) {
        user.photo = user.photo.toString()
    }
    var newUser = await sendRequestWithToken(ACCOUNT_URL, 'PATCH', user);
    window.location.href = PROFILE_URL;
    return newUser;
}
