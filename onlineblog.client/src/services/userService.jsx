import { sendRequestWithToken } from "./commonService";
import {ACCOUNT_URL} from "./commonService";

export async function getUser(){
    var user = await sendRequestWithToken(ACCOUNT_URL, 'GET')
    return user;
}

export async function updateUser(user) {    
    user.photo = user.photo.toString()
    var newUser = await sendRequestWithToken(ACCOUNT_URL, 'PATCH')
    window.location.href = 'profile';
    return newUser;
}